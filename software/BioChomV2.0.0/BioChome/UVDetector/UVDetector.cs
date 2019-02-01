using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Threading;
//using System.IO;
using System.Diagnostics;

namespace UVDetector
{   
    public partial class UV
    {
        public String COMString = String.Empty;

        public struct UVPara {
            public int id;
            public double[] data;
            public double vps;
            public double aufs;
            public bool ISLampDeuteriumOpen;
            public bool ISLampTungstenOpen;
            public int waveLengthNow;
            public int waveLength1;
            public int waveLength2;
            public int waveLength3;
            public int uvWaveLengthCnt;
        };
        public static UVPara t_UVPara;

        public struct peakInfo
        {
            internal double thresholdStart_Set;
            internal double thresholdStop_Set;
            internal double slopeStart_Set;
            internal double slopeStop_Set;
            internal double peakWidth_Set;
            internal double peakIntegral_Set;

            internal Boolean isPeak;
            internal double peakIntegral;

            public int collectorButtleNo;
        };
        public static peakInfo CurvPeak;

        public static bool uvDataTableReadLock = false;

        /************************************************************************/



        /************************************************************************/
        //private string[] csvStr;
        private static OleDbConnection conn;
        public static DataTable uvValue_DataTable;
        public static DataRow[] uvValueRow;
        //private static DataRow[] readMDB;
        //private static DataRow[] writeMDB;
        private static Thread th_writeTable;
        private static Thread th_writeDataBase;
        public static int newRowCnt = 0;
        //private static int newRowStartIndex = 0;

        public static int newDataCnt = 0;

        public static void UVStartCollect()
        {            
            string exePath = System.Environment.CurrentDirectory;
            System.IO.File.Copy("CurvTemplate.mdb", "Curv.mdb", true);
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Curv.mdb");
            conn.Open();
            string sql = "select * from Curv";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            uvValue_DataTable = new DataTable();
            da.Fill(uvValue_DataTable);

            uvValue_DataTable.Columns.Add("isCollectorBottle", typeof(int));
            uvValue_DataTable.Columns.Add("New", typeof(bool));
            uvValue_DataTable.RowChanged += new DataRowChangeEventHandler(Row_Changed);

            uvDataTableReadLock = true;
            CurvShow.StartCollector();

            t_UVPara.data = new double[3];
            CurvPeak.collectorButtleNo = 0;

            th_writeTable = new Thread(WriteDataTable);
            th_writeTable.Start();
            th_writeDataBase = new Thread(WriteDataBase);
            th_writeDataBase.Start();

            curvQueue = new Queue<double>[3];
        }

        public static void UVPauseCollect(bool ISRun)
        {
            if (ISRun)
            {
                CurvShow.StartCollector();
                //th_writeTable = new Thread(WriteDataTable);
                //th_writeTable.Start();
                //th_writeDataBase = new Thread(WriteDataBase);
                //th_writeDataBase.Start();
            }
            else
            {
                //th_writeTable.Abort();
                //th_writeDataBase.Abort();
                CurvShow.StopCollector();
            }
        }

        public static void UVStopCollect()
        {
            uvValue_DataTable.RowChanged -= new DataRowChangeEventHandler(Row_Changed);
            if (th_writeTable != null && th_writeTable.IsAlive) th_writeTable.Abort();
            if (th_writeDataBase != null && th_writeDataBase.IsAlive) th_writeDataBase.Abort();
            CurvShow.StopCollector();
            //conn.Close();
            //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Curv.mdb");
            //conn.Open();
            //string sql = "select * from Curv";
            //OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            //uvValue_DataTable = new DataTable();
            //da.Fill(uvValue_DataTable);
            DataRow drs = uvValue_DataTable.Rows[0];
            drs["VPS"] = t_UVPara.vps;
            drs["xUnit"] = CurvShow.CurvRuler.curvX_unit;
            drs["yUnit"] = CurvShow.CurvRuler.curvY_unit;
            drs["Curv0WaveLength"] = t_UVPara.waveLength1;
            drs["Curv1WaveLength"] = t_UVPara.waveLength2;
            drs["Curv2WaveLength"] = t_UVPara.waveLength3;
            drs["UVType"] = t_UVPara.uvWaveLengthCnt;
            //OleDbCommand OleCmd = new OleDbCommand(
            //    "insert into Curv ([VPS], [xUnit], [yUnit], [Curv0WaveLength], [Curv1WaveLength], [Curv2WaveLength]) values('" + drs["VPS"] + "','" + drs["xUnit"] + "','" + drs["yUnit"] + "','" + drs["Curv0WaveLength"] + "','" + drs["Curv1WaveLength"] + "','" + drs["Curv2WaveLength"] + "')", conn);
            OleDbCommand OleCmd = new OleDbCommand(
                "update Curv set VPS='" + t_UVPara.vps
                + "',xUnit='" + CurvShow.CurvRuler.curvX_unit 
                + "',yUnit='" + CurvShow.CurvRuler.curvY_unit
                + "',Curv0WaveLength='" + t_UVPara.waveLength1.ToString()
                + "',Curv1WaveLength='" + t_UVPara.waveLength2.ToString()
                + "',Curv2WaveLength='" + t_UVPara.waveLength3.ToString()
                + "',UVType='" + drs["UVType"]
                + "' where ID=1", conn);
            OleCmd.ExecuteNonQuery();
            OleCmd = new OleDbCommand(
                "update Curv set Curv0WaveLength='" + System.Drawing.ColorTranslator.ToHtml(CurvShow.CurvRuler.curv0Color)
                + "',Curv1WaveLength='" + System.Drawing.ColorTranslator.ToHtml(CurvShow.CurvRuler.curv1Color)
                + "',Curv2WaveLength='" + System.Drawing.ColorTranslator.ToHtml(CurvShow.CurvRuler.curv2Color)
                + "' where ID=2", conn);
            OleCmd.ExecuteNonQuery();
        }
        public static void UVDispose()
        {
            if (th_writeTable != null && th_writeTable.IsAlive) th_writeTable.Abort();
            if (th_writeDataBase != null && th_writeDataBase.IsAlive) th_writeDataBase.Abort();
            CurvShow.DisposeCollector();
        }

        protected static void WriteDataTable()
        {
            double[] peakValue = new double[4];
            int index = 0;
            //dt = new DataTable();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
                    //UVDetector.UV.t_UVPara.uvWaveLengthCnt = 2;
                    //UVDetector.UV.t_UVPara.waveLength1 = 254;
                    //UVDetector.UV.t_UVPara.waveLength2 = 275;
                    //UVDetector.UV.t_UVPara.waveLength3 = 320;
            while (true)
            {
                Thread.Sleep(10);
                if (newDataCnt > 0)
                {
                    uvDataTableReadLock = true;
                    DataRow drs = uvValue_DataTable.NewRow();
                    drs["Time"] = DateTime.Now;
                    //if (t_UVPara.data[0] < 18) t_UVPara.data[0]++; else t_UVPara.data[0] = 0;
                    //if (t_UVPara.data[1] < 18) t_UVPara.data[1]++; else t_UVPara.data[1] = 0;
                    //if (t_UVPara.data[2] < 18) t_UVPara.data[2]++; else t_UVPara.data[2] = 0;
                    //t_UVPara.data[1]++;
                    //t_UVPara.data[2]++;
                    //drs["Curv0"] = 100 * Math.Sin(t_UVPara.data[0] / 3.1415926) + 50;
                    //drs["Curv1"] = 100 * Math.Sin(t_UVPara.data[1] / 3.1415926) + 0;
                    //drs["Curv2"] = 100 * Math.Sin(t_UVPara.data[2] / 3.1415926) - 50;

                    drs["Curv0"] = t_UVPara.data[0];
                    drs["Curv1"] = t_UVPara.data[1];
                    drs["Curv2"] = t_UVPara.data[2];
                    //drs["isPeak"] = false;
                    drs["New"] = true;

                    SetPeakQueue(Convert.ToDouble(drs["Curv0"]), Convert.ToDouble(drs["Curv1"]), Convert.ToDouble(drs["Curv2"]));

                    drs["isCollectorBottle"] = CurvPeak.collectorButtleNo;
                    //peakValue[0] = peakValue[1];
                    //peakValue[1] = peakValue[2];
                    //peakValue[2] = peakValue[3];
                    //peakValue[3] = Convert.ToDouble(drs["Curv0"]);
                    //if (uvValue_DataTable.Rows.Count > 4)
                    //{ 
                    //    if (ISPeakStart(peakValue[0], peakValue[1], peakValue[2], peakValue[3]) || CurvPeak.isPeak)
                    //    {
                    //        CurvPeak.isPeak = true;
                    //        drs["isPeak"] = true;
                    //    }
                    //    if (ISPeakStop(peakValue[0], peakValue[1], peakValue[2], peakValue[3]) && CurvPeak.isPeak)
                    //    {
                    //        CurvPeak.isPeak = false;
                    //        drs["isPeak"] = false;
                    //    }
                    //}

                    uvValue_DataTable.Rows.Add(drs);
                    newRowCnt++;
                    newDataCnt--;

                }
                else
                {
                    //uvDataTableReadLock = false;

                }
            }

            //sw.Stop();
            //TimeSpan ts2 = sw.Elapsed;
            //Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
            //OleDbCommand OleCmd = new OleDbCommand("insert into Curv ([Time], [Data]) values('" + drs["Time"] + "','" + drs["Data"] + "')", conn);
            //OleCmd.ExecuteNonQuery();

            //Thread writeMDB = new Thread(new ParameterizedThreadStart(SaveDataBase));
            //writeMDB.Start(drs);
        }

        private static void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            uvDataTableReadLock = false;
            //uvValueRow = UVDetector.UV.uvValue_DataTable.Select();
            //CurvShow.CurvRuler.valX_Max = uvValue_DataTable.Rows.Count / UVDetector.UV.t_UVPara.vps / 60;
            //CurvShow.CurvRuler.valY_Max = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Compute("Max(Data)", "true"));
            //CurvShow.CurvRuler.valY_Min = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Compute("Min(Data)", "true"));
        }

        protected static void WriteDataBase()
        {
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    if (newRowCnt > 0)
                    {
                        DataRow drs = uvValue_DataTable.Rows[uvValue_DataTable.Rows.Count - newRowCnt];
                        if (Convert.ToBoolean(drs["New"]))
                        {
                            drs["New"] = false;
                            OleDbCommand OleCmd;
                            switch (t_UVPara.uvWaveLengthCnt)
                            {
                                case 1:
                                    OleCmd = new OleDbCommand("insert into Curv ([Time], [Curv0]) values('" + drs["Time"] + "','" + drs["Curv0"] + "')", conn);
                                    OleCmd.ExecuteNonQuery();
                                    break;
                                case 2:
                                    OleCmd = new OleDbCommand("insert into Curv ([Time], [Curv0], [Curv1]) values('" + drs["Time"] + "','" + drs["Curv0"] + "','" + drs["Curv1"] + "')", conn);
                                    OleCmd.ExecuteNonQuery();
                                    break;
                                case 3:
                                    OleCmd = new OleDbCommand("insert into Curv ([Time], [Curv0], [Curv1], [Curv2]) values('" + drs["Time"] + "','" + drs["Curv0"] + "','" + drs["Curv1"] + "','" + drs["Curv2"] + "')", conn);
                                    OleCmd.ExecuteNonQuery();
                                    break;
                            }
                        }
                        newRowCnt--;
                    }
                }
                catch
                {

                }
            }
        }


        /**************************峰判定********************************/
        public static Queue<double>[] curvQueue;
        private static void SetPeakQueue(double dat0, double dat1, double dat2)
        {
            if (curvQueue[0] == null) curvQueue[0] = new Queue<double>();
            if (curvQueue[1] == null) curvQueue[1] = new Queue<double>();
            if (curvQueue[2] == null) curvQueue[2] = new Queue<double>();
            if (curvQueue[0].Count < 4) curvQueue[0].Enqueue(dat0);
            else
            {
                curvQueue[0].Dequeue();
                curvQueue[0].Enqueue(dat0);
            }
            if (curvQueue[1].Count < 4) curvQueue[1].Enqueue(dat1);
            else
            {
                curvQueue[1].Dequeue();
                curvQueue[1].Enqueue(dat1);
            }
            if (curvQueue[2].Count < 4) curvQueue[2].Enqueue(dat2);
            else
            {
                curvQueue[2].Dequeue();
                curvQueue[2].Enqueue(dat2);
            }
        }


        public static bool ISPeakStart(Queue<double> curv)
        {
            double[] dat = new double[4];
            int i = 0;
            foreach (double x in curv) dat[i++] = x;

            if (dat[3] - dat[2] > CurvPeak.slopeStart_Set &&
                dat[2]- dat[1] > CurvPeak.slopeStart_Set &&
                dat[1] - dat[0] > CurvPeak.slopeStart_Set)
            {
                return true;
            }
            return false;
        }
        public static bool ISPeakStop(double dat1, double dat2, double dat3, double dat4)
        {
            if (dat4 - dat3 > CurvPeak.slopeStart_Set &&
                dat3 - dat2 < CurvPeak.slopeStart_Set &&
                dat2 - dat1 < CurvPeak.slopeStart_Set)
            {
                return true;
            }
            return false;
        }
        /****************************************************************/


    }
}
