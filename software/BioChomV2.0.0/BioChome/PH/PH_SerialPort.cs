using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace PH
{
    public class PH
    {
        public struct PHPara
        {
            public string id;
            public string ser;


            public double conductance;
            public double ph;
            public double temperature;
        };
        public PHPara t_PHInfo;

        public struct commu
        {
            public SerialPort phPort;

            public string STX;
            public string ID;
            public string AI;
            public string PFC;
            public string VALUE;
            public string CRC;
            public string ETX;
        }
        public commu t_SerialPortCommu;



        public void SetPHSerialPort(SerialPort port, string ID)
        {
            t_SerialPortCommu = new commu();
            t_SerialPortCommu.phPort = port;
            t_SerialPortCommu.ID = ID;

        }
        public void ClearPHSerialPort()
        {
            revStr = "";
            t_SerialPortCommu = new commu();
        }

        public string revStr = "";
        public Thread th_recvFromPHSerialPort;
        public bool InitialPH(string phType)
        {
            if (phType == "PH/C Monitor")
            {
                th_recvFromPHSerialPort = new Thread(ReadPHValue);
                th_recvFromPHSerialPort.Start();
            }
            else if (phType == "")
            {

            }

            return true;
        }
        public void PHInstanceDispose()
        {
            try
            {
                t_PHInfo = new PHPara();
                if (th_recvFromPHSerialPort != null && th_recvFromPHSerialPort.IsAlive)
                    th_recvFromPHSerialPort.Abort();
            }
            catch
            {
                return;
            }
        }


        private void ReadPHValue()
        {
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    t_SerialPortCommu.phPort.ReadTo("A");
                    revStr = "A" + t_SerialPortCommu.phPort.ReadTo("Y");
                }
                catch
                {
                    revStr = "";
                    continue;
                }
                t_PHInfo.conductance = Convert.ToDouble(revStr.Substring(3, 6));
                t_PHInfo.ph = Convert.ToDouble(revStr.Substring(10, 4)) / 10;
                t_PHInfo.temperature = Convert.ToDouble(revStr.Substring(15, 4)) / 10;
            }
        }


    }
}
