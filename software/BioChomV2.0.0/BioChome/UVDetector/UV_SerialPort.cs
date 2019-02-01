using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace UVDetector
{
    partial class UV
    {
        public struct commu
        {
            public SerialPort uvPort;

            public string STX;
            public string ID;
            public string AI;
            public string PFC;
            public string VALUE;
            public string CRC;
            public string ETX;

            public bool revLock;
        }
        public commu t_SerialPortCommu;



        public void SetUVSerialPort(SerialPort port, string ID)
        {
            t_SerialPortCommu = new commu();
            t_SerialPortCommu.uvPort = port;
            t_SerialPortCommu.ID = ID;
        }

        public void ClearUVSerialPort()
        {
            revStr = "";
            t_SerialPortCommu = new commu();
        }

        public string revStr = "";
        public bool InitialUV()
        {
            if (UVSerialPortSendData(0, 18, 0, "#") == "#") return false;

            revStr = "";
            revStr = UVSerialPortSendData(0, 5, " ", "\n");
            if (revStr == "" || GetCRC(revStr.Substring(0, 12)) != revStr.Substring(12, 3)) return false;
            if (!GetUVVPS(revStr)) return false;
            if (!GetUVAUFS(revStr)) return false;

            revStr = "";
            revStr = UVSerialPortSendData(0, 4, " ", "\n");
            if (revStr == "" || GetCRC(revStr.Substring(0, 12)) != revStr.Substring(12, 3)) return false;
            if (!GetUVLampState(revStr)) return false;
            if (!GetUVWaveLength(revStr)) return false;

            revStr = "";
            revStr = UVSerialPortSendData(0, 9, " ", "\n");
            if (revStr == "" || GetCRC(revStr.Substring(0, 12)) != revStr.Substring(12, 3)) return false;
            if (!GetUVSetWaveLength(revStr)) return false;

            if (t_SerialPortCommu.ID == "52")
            {
                revStr = "";
                revStr = UVSerialPortSendData(1, 9, " ", "\n");
                if (revStr == "" || GetCRC(revStr.Substring(0, 12)) != revStr.Substring(12, 3)) return false;
                if (!GetUVSetWaveLength(revStr)) return false;
            }
            StartUV();
            return true;
        }

        private Thread th_recvFromUVSerialPort;
        public void StartUV()
        {
            UVSerialPortSendData(0, 18, 1, "#");
            //UVSerialPortSendData(0, 17, " ", "#");
            th_recvFromUVSerialPort = new Thread(ReadUVValue);
            th_recvFromUVSerialPort.Start();
        }
        public void StopUV()
        {
            UVInstanceDispose();
            UVSerialPortSendData(0, 18, 0, "#");
        }
        public void UVInstanceDispose()
        {
            try
            {
                t_UVPara = new UVPara();
                if (th_recvFromUVSerialPort != null && th_recvFromUVSerialPort.IsAlive)
                    th_recvFromUVSerialPort.Abort();
            }
            catch
            {
                return;
            }
        }


        public void ZeroUV()
        {
            UVSerialPortSendData(0, 17, " ", "#");
        }
        public bool UVLampOpen()
        {
            return UVSerialPortSendData(0, 14, " ", "#") == "#";
        }
        public bool UVLampClose()
        {
            return UVSerialPortSendData(0, 15, " ", "#") == "#";
        }
        public bool WaitLampOpening(SerialPort port)
        {
            t_SerialPortCommu.revLock = true;
            try
            {
                bool ret = (((char)port.ReadByte()).ToString() == "#");
                t_SerialPortCommu.revLock = false;
                return ret;
            }
            catch
            {
                t_SerialPortCommu.revLock = false;
                return false;
            }
        }



        private string UVSerialPortSendData(int AI, int PFC, int VALUE, string stopStr)
        {
            if (t_SerialPortCommu.uvPort == null || !t_SerialPortCommu.uvPort.IsOpen) return "";
            t_SerialPortCommu.revLock = true;

            t_SerialPortCommu.STX = "!";
            //t_SerialPortCommu.ID = string.Format("{0:00}", ID);
            t_SerialPortCommu.AI = string.Format("{0:0}", AI);
            t_SerialPortCommu.PFC = string.Format("{0:00}", PFC);
            t_SerialPortCommu.VALUE = string.Format("{0:D6}", VALUE);
            string sendStr = t_SerialPortCommu.STX + t_SerialPortCommu.ID + t_SerialPortCommu.AI + t_SerialPortCommu.PFC + t_SerialPortCommu.VALUE;
            t_SerialPortCommu.CRC = GetCRC(sendStr);
            t_SerialPortCommu.ETX = "\n";
            sendStr = sendStr + t_SerialPortCommu.CRC + t_SerialPortCommu.ETX;
            t_SerialPortCommu.uvPort.Write(sendStr);
            //if (!WaitReceive(t_SerialPortCommu.uvPort, sendStr)) return false; //通讯失败
            string ret = WaitReceive(t_SerialPortCommu.uvPort, sendStr, stopStr);
            t_SerialPortCommu.revLock = false;
            return ret;
        }
        private string UVSerialPortSendData(int AI, int PFC, string VALUE, string stopStr)
        {
            if (t_SerialPortCommu.uvPort == null || !t_SerialPortCommu.uvPort.IsOpen) return "";
            t_SerialPortCommu.revLock = true;

            t_SerialPortCommu.STX = "!";
            //t_SerialPortCommu.ID = string.Format("{0:00}", ID);
            t_SerialPortCommu.AI = string.Format("{0:0}", AI);
            t_SerialPortCommu.PFC = string.Format("{0:00}", PFC);
            if (VALUE == " ") t_SerialPortCommu.VALUE = "      ";
            else t_SerialPortCommu.VALUE = string.Format("{0,6}", VALUE);
            string sendStr = t_SerialPortCommu.STX + t_SerialPortCommu.ID + t_SerialPortCommu.AI + t_SerialPortCommu.PFC + t_SerialPortCommu.VALUE;
            t_SerialPortCommu.CRC = GetCRC(sendStr);
            t_SerialPortCommu.ETX = "\n";
            sendStr = sendStr + t_SerialPortCommu.CRC + t_SerialPortCommu.ETX;
            t_SerialPortCommu.uvPort.Write(sendStr);
            //if (!WaitReceive(t_SerialPortCommu.uvPort)) return false; //通讯失败
            string ret = WaitReceive(t_SerialPortCommu.uvPort, sendStr, stopStr);
            t_SerialPortCommu.revLock = false;
            return ret;
        }

        private string GetCRC(string s)
        {
            int sum = 0;
            byte[] b = System.Text.Encoding.Default.GetBytes(s);
            for (int i = 0; i < s.Length; ++i)
            {
                //sum = sum + Convert.ToInt32(s.Substring(i, 1));
                sum = sum + Convert.ToInt32(b[i]);
            }

            return string.Format("{0:000}", sum % 256);
        }

        private void ReadUVValue()
        {
            string uvValueStr = string.Empty;
            t_UVPara.data = new double[]{0, 0, 0};
            while (true)
            {
                Thread.Sleep(10);
                if (t_SerialPortCommu.uvPort != null && !t_SerialPortCommu.revLock)
                {
                    try
                    {
                        if (t_SerialPortCommu.uvPort != null) t_SerialPortCommu.uvPort.ReadTo("!");
                        if (t_SerialPortCommu.uvPort != null) uvValueStr = "!" + t_SerialPortCommu.uvPort.ReadTo("\n");
                        if (uvValueStr.Substring(4, 2) == "90" && GetCRC(uvValueStr.Substring(0, 12)) == uvValueStr.Substring(12, 3))
                        {
                            int value = Convert.ToInt32(uvValueStr.Substring(6, 6), 16);
                            if (value > 0x7FFFFF)
                                t_UVPara.data[Convert.ToInt32(uvValueStr.Substring(3, 1))] = -Convert.ToDouble(0xFFFFFF - value) / 1000000 * t_UVPara.aufs;
                            else
                                t_UVPara.data[Convert.ToInt32(uvValueStr.Substring(3, 1))] = Convert.ToDouble(value) / 1000000 * t_UVPara.aufs;
                        }
                    }
                    catch
                    {
                        uvValueStr = "";
                        continue;
                    }
                }
            }
        }

        private string WaitReceive(SerialPort port, string sendStr, string stopStr)
        {
            string s = string.Empty;
            int timeOutCnt = 0;
            try
            {
                do
                {
                    s = port.ReadTo(stopStr);
                    if (s == "" && stopStr == "#") return "#";
                    timeOutCnt++;
                } while (s.Substring(0, 1) != sendStr.Substring(0, 1) || s.Substring(4, 2) != sendStr.Substring(4, 2) && timeOutCnt < 10);
            }
            catch
            {
                return "";
            }
            if (timeOutCnt >= 10) return "";
            return s;
        }

        public void SetUVWaveLength(string waveLength1, string waveLength2, string waveLength3)
        {
            switch (t_SerialPortCommu.ID)
            {
                case "50":
                    UVSerialPortSendData(0, 10, waveLength1, "#");
                    break;
                case "51":
                    UVSerialPortSendData(0, 10, waveLength2 + waveLength1, "#");
                    break;
                case "52":
                    UVSerialPortSendData(0, 10, waveLength2 + waveLength1, "#");
                    UVSerialPortSendData(1, 10, waveLength3, "#");
                    break;
            }
        }

        public void SetUVVPS(int vps)
        {
            UVSerialPortSendData(0, 11, vps, "#");
        }

        private bool GetUVVPS(string s)
        {
            double[] vpsList = { 0.1, 0.2, 0.5, 1.0, 2.0, 3.0, 4.0, 5.0, 10.0 };
            int vpsListIndex = Convert.ToInt32(s.Substring(7, 2));
            if (vpsListIndex >= 0 && vpsListIndex <= vpsList.Length)
                t_UVPara.vps = vpsList[vpsListIndex];
            else
                return false;
            return true;
        }
        private bool GetUVAUFS(string s)
        {
            double[] aufsList = { 0.0001, 0.0002, 0.0005, 0.0010, 0.0020, 0.0050, 0.0100, 0.0200, 0.0500, 0.1000, 0.2000, 0.5000, 1.0000, 2.0000, 3.0000, 4.0000, 5.0000, 10.0000 };
            int aufsListIndex = Convert.ToInt32(s.Substring(10, 2));
            if (aufsListIndex >= 0 && aufsListIndex <= aufsList.Length)
                t_UVPara.aufs = aufsList[aufsListIndex];
            else
                return false;
            return true;
        }
        private bool GetUVLampState(string s)
        {
            switch (s.Substring(6, 2))
            {
                case "00":
                    t_UVPara.ISLampDeuteriumOpen = false;
                    break;
                case "01":
                    t_UVPara.ISLampDeuteriumOpen = true;
                    break;
                case "10":
                    t_UVPara.ISLampTungstenOpen = false;
                    break;
                case "11":
                    t_UVPara.ISLampTungstenOpen = true;
                    break;
                default:
                    return false;
            }
            return true;
        }
        private bool GetUVWaveLength(string s)
        {
            int waveLength = Convert.ToInt32(s.Substring(8, 4));
            if (waveLength > 190 && waveLength <= 700)
                t_UVPara.waveLengthNow = waveLength;
            else
                return false;
            return true;
        }
        private bool GetUVSetWaveLength(string s)
        {
            int waveLength1, waveLength2, waveLength3;
            switch (t_SerialPortCommu.ID)
            {
                case "50":
                    t_UVPara.uvWaveLengthCnt = 1;
                    waveLength1 = Convert.ToInt32(s.Substring(9, 3));
                    if (waveLength1 > 190 && waveLength1 <= 700)
                        t_UVPara.waveLength1 = waveLength1;
                    else
                        return false;
                    break;
                case "51":
                    t_UVPara.uvWaveLengthCnt = 2;
                    waveLength1 = Convert.ToInt32(s.Substring(9, 3));
                    if (waveLength1 > 190 && waveLength1 <= 700)
                        t_UVPara.waveLength1 = waveLength1;
                    else
                        return false;
                    waveLength2 = Convert.ToInt32(s.Substring(6, 3));
                    if (waveLength2 == 0 || waveLength2 > 190 && waveLength2 <= 700)
                        t_UVPara.waveLength2 = waveLength2;
                    else
                        return false;
                    break;
                case "52":
                    t_UVPara.uvWaveLengthCnt = 3;
                    switch (s.Substring(3, 1))
                    {
                        case "0":
                            waveLength1 = Convert.ToInt32(s.Substring(9, 3));
                            if (waveLength1 > 190 && waveLength1 <= 700)
                                t_UVPara.waveLength1 = waveLength1;
                            else
                                return false;
                            waveLength2 = Convert.ToInt32(s.Substring(6, 3));
                            if (waveLength2 == 0 || waveLength2 > 190 && waveLength2 <= 700)
                                t_UVPara.waveLength2 = waveLength2;
                            else
                                return false;
                            break;
                        case "1":
                            waveLength3 = Convert.ToInt32(s.Substring(6, 3));
                            if (waveLength3 == 0 || waveLength3 > 190 && waveLength3 <= 700)
                                t_UVPara.waveLength3 = waveLength3;
                            else
                                return false;
                            break;
                    }
                    break;
            }
            return true;
        }

    }
}
