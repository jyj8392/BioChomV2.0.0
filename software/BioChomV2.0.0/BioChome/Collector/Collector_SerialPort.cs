using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Collector
{
    public class Collector
    {
        public struct CollectorPara
        {
            public string id;
            public string ser;
            public bool isRun;
            public string softVer;
            public int flowadj;

            public double flowTotal;
            public double flowPercent;
            public int pressure;

            public int currentButtleNo;
            public bool ISRun;
        };
        public static CollectorPara t_CollectorPara;

        public struct commu
        {
            public SerialPort collectorPort;

            public string STX;
            public string ID;
            public string AI;
            public string PFC;
            public string VALUE;
            public string CRC;
            public string ETX;
        }
        public commu t_SerialPortCommu;

        public void SetCollectorSerialPort(SerialPort port, string ID)
        {
            t_SerialPortCommu = new commu();
            t_SerialPortCommu.collectorPort = port;
            t_SerialPortCommu.ID = ID;

        }
        public void ClearCollectorSerialPort()
        {
            revStr = "";
            t_SerialPortCommu = new commu();
        }

        public string revStr = "";
        public bool InitialCollector()
        {
            revStr = "";
            revStr = CollectorSerialPortSendData(0, 4, " ", "\n");
            if (revStr == "" || GetCRC(revStr.Substring(0, 12)) != revStr.Substring(12, 3)) return false;
            if (!GetCollectorState(revStr)) return false;

            return true;
        }
        public void CollectorInstanceDispose()
        {
            try
            {
                t_CollectorPara = new CollectorPara();
            }
            catch
            {
                return;
            }
        }



        private string CollectorSerialPortSendData(int AI, int PFC, int VALUE, string stopStr)
        {
            if (t_SerialPortCommu.collectorPort == null || !t_SerialPortCommu.collectorPort.IsOpen) return "";

            t_SerialPortCommu.STX = "!";
            //t_SerialPortCommu.ID = string.Format("{0:00}", ID);
            t_SerialPortCommu.AI = string.Format("{0:0}", AI);
            t_SerialPortCommu.PFC = string.Format("{0:00}", PFC);
            t_SerialPortCommu.VALUE = string.Format("{0,6}", VALUE);
            string sendStr = t_SerialPortCommu.STX + t_SerialPortCommu.ID + t_SerialPortCommu.AI + t_SerialPortCommu.PFC + t_SerialPortCommu.VALUE;
            t_SerialPortCommu.CRC = GetCRC(sendStr);
            t_SerialPortCommu.ETX = "\n";
            sendStr = sendStr + t_SerialPortCommu.CRC + t_SerialPortCommu.ETX;
            t_SerialPortCommu.collectorPort.Write(sendStr);
            return WaitReceive(t_SerialPortCommu.collectorPort, sendStr, stopStr);
        }
        private string CollectorSerialPortSendData(int AI, int PFC, string VALUE, string stopStr)
        {
            if (t_SerialPortCommu.collectorPort == null || !t_SerialPortCommu.collectorPort.IsOpen) return "";

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
            t_SerialPortCommu.collectorPort.Write(sendStr);
            return WaitReceive(t_SerialPortCommu.collectorPort, sendStr, stopStr);
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

        private bool GetCollectorState(string s)
        {
            switch (s.Substring(6, 1))
            {
                case "0":
                    t_CollectorPara.isRun = false;
                    break;
                case "1":
                    t_CollectorPara.isRun = true;
                    break;
                default:
                    return false;
            }
            int no = Convert.ToInt32(s.Substring(7, 5));
            if (no >= 1 && no <= 120)
                t_CollectorPara.currentButtleNo = no;
            else
                return false;
            return true;
        }

        public void GotoButtle(int no)
        {
            CollectorSerialPortSendData(0, 10, no, "#");
        }

        public void StartCollect()
        {
            CollectorSerialPortSendData(0, 15, " ", "#");
        }

        public void StopCollect()
        {
            CollectorSerialPortSendData(0, 16, " ", "#");
        }

    }
}
