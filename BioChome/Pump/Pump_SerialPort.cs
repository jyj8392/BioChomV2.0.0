using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Pump
{
    public class Pump
    {
        public struct PumpPara
        {
            public string pumpType;
            public string no;
            public bool isRun;
            public string softVer;
            public int flowadj;

            public double flowTotal;
            public double flowPercent;
            public double setMaxPressure;
            public double setMinPressure;
            public double pressure;
        };
        public PumpPara t_PumpInfo;

        public struct commu
        {
            public SerialPort pumpPort;

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



        public void SetPumpSerialPort(SerialPort port, string ID)
        {
            t_SerialPortCommu = new commu();
            t_SerialPortCommu.pumpPort = port;
            t_SerialPortCommu.ID = ID;

        }
        public void ClearPumpSerialPort()
        {
            //if (t_SerialPortCommu.ID == "Tauto")
                    //t_SerialPortCommu.pumpPort.DataReceived -= new SerialDataReceivedEventHandler(TautoPumpPort_DataReceived);
            //else
                //t_SerialPortCommu.pumpPort.DataReceived -= new SerialDataReceivedEventHandler(pumpPort_DataReceived);
            revStr = "";
            t_SerialPortCommu = new commu();
        }

        public string revStr = "";
        private Thread th_recvFromPumpSerialPort;
        private Thread th_sendToPumpSerialPort;
        private Thread th_overTimeSerialPort;
        public bool InitialPump(char pumpType, string no)
        {
            if (th_overTimeSerialPort == null)
            {
                th_overTimeSerialPort = new Thread(OverTimer);
                th_overTimeSerialPort.Start();
                overtime_count = 0;
                start_trig_overtime1 = false;
            }

            t_PumpInfo = new PumpPara();
            t_PumpInfo.pumpType = pumpType.ToString();
            t_PumpInfo.no = no;

            t_PumpInfo.setMaxPressure = 10;
            if (t_SerialPortCommu.ID == "Tauto")
            {
                t_PumpInfo.setMaxPressure = 10;
                //t_SerialPortCommu.pumpPort.DataReceived += new SerialDataReceivedEventHandler(TautoPumpPort_DataReceived);
                //return true;
            }
            else
            {
                if (t_SerialPortCommu.ID == "10")
                    t_PumpInfo.setMaxPressure = 42;
                else if (t_SerialPortCommu.ID == "11")
                    t_PumpInfo.setMaxPressure = 25;
                else if (t_SerialPortCommu.ID == "20")
                    t_PumpInfo.setMaxPressure = 25;
                t_PumpInfo.setMinPressure = 0;

                if (!PumpSerialPortSendData(0, 18, 20)) return false;
                //if (!PumpSerialPortSendData(0, 13, 4200)) return false;
                if (!PumpSerialPortSendData(0, 14, 0)) return false;
                if (!PumpSerialPortSendData(1, 19, pumpType - 'A' + 1)) return false;
            }
            //t_SerialPortCommu.pumpPort.DataReceived += new SerialDataReceivedEventHandler(pumpPort_DataReceived);
            th_recvFromPumpSerialPort = new Thread(ReadPressure);
            th_recvFromPumpSerialPort.Start();
            th_sendToPumpSerialPort = new Thread(SetFlow);
            th_sendToPumpSerialPort.Start();


            return true;
        }

        public void PumpInstanceDispose()
        {
            try
            {
                t_PumpInfo = new PumpPara();
                if (th_recvFromPumpSerialPort != null && th_recvFromPumpSerialPort.IsAlive)
                    th_recvFromPumpSerialPort.Abort();
                if (th_sendToPumpSerialPort != null && th_sendToPumpSerialPort.IsAlive)
                    th_sendToPumpSerialPort.Abort();
                if (th_overTimeSerialPort != null && th_overTimeSerialPort.IsAlive)
                    th_overTimeSerialPort.Abort();
            }
            catch
            {
                return;
            }
        }


        public bool SetPumpFlow(double flow)
        {
            int overTime = 0;
            if (t_SerialPortCommu.ID == "10")
            {
                if (flow == 10) flow = 9.999;
                while (overTime < 10 && !PumpSerialPortSendData(0, 10, Convert.ToInt32(flow * 1000))) { overTime++; Thread.Sleep(1); }
            }
            else if (t_SerialPortCommu.ID == "11")
            {
                while (overTime < 10 && !PumpSerialPortSendData(0, 10, Convert.ToInt32(flow * 100))) { overTime++; Thread.Sleep(1); }
            }
            else if (t_SerialPortCommu.ID == "20")
            {
                while (overTime < 10 && !PumpSerialPortSendData(0, 10, Convert.ToInt32(flow * 100))) { overTime++; Thread.Sleep(1); }
            }
            else if (t_SerialPortCommu.ID == "Tauto")
            {
                while (overTime < 10 && !TautoPumpSerialPortSendData(0, 'L', Convert.ToInt32(flow * 100))) { overTime++; Thread.Sleep(1); }
            }
            if (overTime >= 10) return false;
            return true;
        }

        public void SetPumpGradFlow(double flow)
        {
            if (flow < 0) return;
            isFreshFlow = true;
            freshFlow = flow;
        }

        public bool StartPump()
        {
            int overTime = 0;
            if (t_SerialPortCommu.ID == "Tauto")
            {
                while (overTime < 3 && !TautoPumpSerialPortSendData(0, 'R', 0)) { overTime++; Thread.Sleep(1); }
            }
            else {
                while (overTime < 3 && !PumpSerialPortSendData(0, 15, " ")) { overTime++; Thread.Sleep(1); }
            }
            if (overTime >= 3) return false;
            return true;
        }
        public bool StopPump()
        {
            int overTime = 0;
            if (t_SerialPortCommu.ID == "Tauto")
            {
                while (overTime < 3 && !TautoPumpSerialPortSendData(0, 'S', 0)) { overTime++; Thread.Sleep(1); }
            }
            else {
                while (overTime < 3 && !PumpSerialPortSendData(0, 16, " ")) { overTime++; Thread.Sleep(1); }
            }
            if (overTime >= 3) return false;
            return true;
        }

        public bool SetPumpMaxPressure(double pressure)
        {
            int overTime = 0;
            if (t_SerialPortCommu.ID == "Tauto")
            {
                while (overTime < 3 && !TautoPumpSerialPortSendData(0, 'P', Convert.ToInt32(pressure * 100))) { overTime++; Thread.Sleep(1); }
            }
            else {
                while (overTime < 3 && !PumpSerialPortSendData(0, 13, Convert.ToInt32(pressure * 100))) { overTime++; Thread.Sleep(1); }
            }
            if (overTime >= 3) return false;
            return true;
        }
        public bool SetPumpMinPressure(double pressure)
        {
            int overTime = 0;
            if (t_SerialPortCommu.ID == "Tauto")
            {
                ;//while (!TautoPumpSerialPortSendData(0, 'P', Convert.ToInt32(pressure * 100))) Thread.Sleep(1);
            }
            else {
                while (overTime < 3 && !PumpSerialPortSendData(0, 14, Convert.ToInt32(pressure * 100))) { overTime++; Thread.Sleep(1); }
            }
            if (overTime >= 3) return false;
            return true;
        }
        public double GetPumpPressure(string revData)
        {
            if (t_SerialPortCommu.ID == "Tauto")
            {
                string PFC = revData.Substring(3, 1);
                string VALUE = revData.Substring(4, 4);
                if (PFC != "N") return 0;
                return Convert.ToDouble(VALUE);
            }
            else
            {
                string STX = revData.Substring(0, 1);
                string ID = revData.Substring(1, 2);
                string AI = revData.Substring(3, 1);
                string PFC = revData.Substring(4, 2);
                string VALUE = revData.Substring(6, 6);
                string CRC = revData.Substring(12, 3);
                //string ETX = revData.Substring(15, 1);

                if (CRC != GetCRC(revData.Substring(0, 12))) return 0;
                if (STX != "!") return 0;
                if (ID != t_SerialPortCommu.ID) return 0;
                if (AI != "0") return 0;
                if (PFC != "90") return 0;
                //if (ETX != "\n") return 0;

                return Convert.ToDouble(VALUE) / 100.0;
            }
            return 0;
        }

        private bool PumpSerialPortSendData(int AI, int PFC, int VALUE)
        {
            if (t_SerialPortCommu.pumpPort == null || !t_SerialPortCommu.pumpPort.IsOpen) return false;
            t_SerialPortCommu.revLock = true;

            t_SerialPortCommu.STX = "!";
            //t_SerialPortCommu.ID = string.Format("{0:00}", ID);
            t_SerialPortCommu.AI = string.Format("{0:0}", AI);
            t_SerialPortCommu.PFC = string.Format("{0:00}", PFC);
            t_SerialPortCommu.VALUE = string.Format("{0:000000}", VALUE);
            string sendStr = t_SerialPortCommu.STX + t_SerialPortCommu.ID + t_SerialPortCommu.AI + t_SerialPortCommu.PFC + t_SerialPortCommu.VALUE;
            t_SerialPortCommu.CRC = GetCRC(sendStr);
            t_SerialPortCommu.ETX = "\n";
            sendStr = sendStr + t_SerialPortCommu.CRC + t_SerialPortCommu.ETX;
            t_SerialPortCommu.pumpPort.Write(sendStr);
            bool ret = WaitReceive(t_SerialPortCommu.pumpPort); //通讯失败
            t_SerialPortCommu.revLock = false;
            return ret;
        }
        private bool PumpSerialPortSendData(int AI, int PFC, string VALUE)
        {
            if (t_SerialPortCommu.pumpPort == null || !t_SerialPortCommu.pumpPort.IsOpen) return false;
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
            t_SerialPortCommu.pumpPort.Write(sendStr);
            bool ret = WaitReceive(t_SerialPortCommu.pumpPort); //通讯失败
            t_SerialPortCommu.revLock = false;
            return ret;
        }

        private bool TautoPumpSerialPortSendData(int AI, char PFC, int VALUE)
        {
            if (t_SerialPortCommu.pumpPort == null || !t_SerialPortCommu.pumpPort.IsOpen) return false;
            t_SerialPortCommu.revLock = true;

            t_SerialPortCommu.STX = "Q";
            //t_SerialPortCommu.ID = string.Format("{0:00}", ID);
            t_SerialPortCommu.AI = string.Format("{0:00}", AI);
            t_SerialPortCommu.PFC = PFC.ToString();
            t_SerialPortCommu.VALUE = string.Format("{0:0000}", VALUE);
            string sendStr = t_SerialPortCommu.STX + t_SerialPortCommu.AI + t_SerialPortCommu.PFC + t_SerialPortCommu.VALUE;
            //t_SerialPortCommu.CRC = GetCRC(sendStr);
            t_SerialPortCommu.ETX = "Y";
            sendStr = sendStr + t_SerialPortCommu.ETX;
            t_SerialPortCommu.pumpPort.Write(sendStr);
            bool ret = WaitTautoReceive(t_SerialPortCommu.pumpPort);
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

        /*******************************************************************************/
        private void pumpPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                revStr = ((SerialPort)sender).ReadTo("\n");
                //GetPumpPressure();
            }
            catch
            {
                revStr = "";
                return;
            }
        }

        private void TautoPumpPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                revStr = ((SerialPort)sender).ReadTo("Y");
            }
            catch
            {
                revStr = "";
                return;
            }
        }

        private void ReadPressure()
        {
            string pressureStr = string.Empty;

            while (true)
            {
                Thread.Sleep(100);
                if (t_SerialPortCommu.pumpPort != null && !t_SerialPortCommu.revLock)
                    //if (t_SerialPortCommu.pumpPort != null)
                {
                    try
                    {
                        if (t_SerialPortCommu.ID == "Tauto")
                        {
                            t_SerialPortCommu.pumpPort.ReadTo("Q");
                            pressureStr = "Q" + t_SerialPortCommu.pumpPort.ReadTo("Y");
                        }
                        else
                        {
                            t_SerialPortCommu.pumpPort.ReadTo("!");
                            pressureStr = "!" + t_SerialPortCommu.pumpPort.ReadTo("\n");
                        }
                        t_PumpInfo.pressure = GetPumpPressure(pressureStr);
                    }
                    catch
                    {
                        pressureStr = "";
                        continue;
                    }
                }
            }
        }

        private bool isFreshFlow;
        private double freshFlow;
        private void SetFlow()
        {
            isFreshFlow = false;
            while (true)
            {
                Thread.Sleep(10);
                if (isFreshFlow)
                {
                    //while (!TautoPumpSerialPortSendData(0, 'L', Convert.ToInt32(freshFlow * 100))) Thread.Sleep(1);
                    if (t_SerialPortCommu.ID == "10")
                        PumpSerialPortSendData(0, 10, Convert.ToInt32(freshFlow * 1000));
                    else if (t_SerialPortCommu.ID == "11")
                        PumpSerialPortSendData(0, 10, Convert.ToInt32(freshFlow * 100));
                    else if (t_SerialPortCommu.ID == "20")
                        PumpSerialPortSendData(0, 10, Convert.ToInt32(freshFlow * 100));
                    else if (t_SerialPortCommu.ID == "Tauto")
                        TautoPumpSerialPortSendData(0, 'L', Convert.ToInt32(freshFlow * 100));
                    isFreshFlow = false;

                }
            }
        }

        private bool WaitReceive(SerialPort port)
        {
            string s = string.Empty;

            try
            {
                //s = port.ReadTo("#");
                overtime_count = 0;
                start_trig_overtime1 = true;
                while (!(s.Contains("#")) && overtime_count < 25) {s += port.ReadExisting();}
                start_trig_overtime1 = false;
                if (overtime_count >= 25)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool WaitTautoReceive(SerialPort port)
        {
            string s = string.Empty;
            int timeOutCnt = 0;
            do
            {
                try
                {
                    s = port.ReadTo("#");
                    return true;
                }
                catch
                {
                    timeOutCnt++;
                }
            } while (timeOutCnt < 20);
            return false;
        }
        /*******************************************************************************/
        private int overtime_count;
        private bool start_trig_overtime1;
        private void OverTimer()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (start_trig_overtime1 == true)
                {
                    Thread.Sleep(10);
                    overtime_count++;
                    if (overtime_count >= 10000)
                        overtime_count = 0;
                }
            }
        }

    }



}
