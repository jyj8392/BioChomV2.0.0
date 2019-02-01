using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace CurvAnalysis
{
    public class UV
    {
        public struct UVPara
        {
            public int id;
            public double data;
            public double vps;

            public int waveLength1;
            public int waveLength2;
            public int waveLength3;
            public int uvWaveLengthCnt;
        };

        public static UVPara t_UVPara;
        public static DataTable uvValue_DataTable;

        public static void OpenCurvFile(string path)
        {
            try {
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path);
                conn.Open();
                string sql = "select * from Curv";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                uvValue_DataTable = new DataTable();
                da.Fill(uvValue_DataTable);
                conn.Close();
            } catch (Exception e)
            {
                return;
            }

            try {
                CurvShow.CurvRuler.curv0Color = System.Drawing.ColorTranslator.FromHtml(uvValue_DataTable.Rows[1]["Curv0WaveLength"].ToString());
                CurvShow.CurvRuler.curv1Color = System.Drawing.ColorTranslator.FromHtml(uvValue_DataTable.Rows[1]["Curv1WaveLength"].ToString());
                CurvShow.CurvRuler.curv2Color = System.Drawing.ColorTranslator.FromHtml(uvValue_DataTable.Rows[1]["Curv2WaveLength"].ToString());
                t_UVPara.waveLength1 = Convert.ToInt32(uvValue_DataTable.Rows[0]["Curv0WaveLength"]);
                t_UVPara.waveLength2 = Convert.ToInt32(uvValue_DataTable.Rows[0]["Curv1WaveLength"]);
                t_UVPara.waveLength3 = Convert.ToInt32(uvValue_DataTable.Rows[0]["Curv2WaveLength"]);
                t_UVPara.uvWaveLengthCnt = Convert.ToInt32(uvValue_DataTable.Rows[0]["UVType"]);
                t_UVPara.vps = Convert.ToDouble(uvValue_DataTable.Rows[0]["VPS"]);

                CurvShow.CurvRuler.x_Max = GetCurvRuler_xMax(uvValue_DataTable);
                CurvShow.CurvRuler.curvX_unit = uvValue_DataTable.Rows[0]["xUnit"].ToString();
                CurvShow.CurvRuler.curvX_Max = CurvShow.CurvRuler.x_Max;
                CurvShow.CurvRuler.curvX_Min = CurvShow.CurvRuler.x_Min;

                CurvShow.CurvRuler.x_Min = 0;
                CurvShow.CurvRuler.y_Max = GetCurvRuler_yMax(uvValue_DataTable);
                CurvShow.CurvRuler.y_Min = GetCurvRuler_yMin(uvValue_DataTable);
                CurvShow.CurvRuler.curvY_unit = uvValue_DataTable.Rows[0]["yUnit"].ToString();
                CurvShow.CurvRuler.curvY_Max = CurvShow.CurvRuler.y_Max;
                CurvShow.CurvRuler.curvY_Min = CurvShow.CurvRuler.y_Min;
            }
            catch
            {
                MessageBox.Show("图谱格式错误", "打开图谱", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        public static void UVDispose()
        {
            CurvShow.DisposeCollector();
        }

        //public static double GetUVPara_vps(DataTable uvdt)
        //{
        //    double vps = 1;
        //    DataRow[] uvrw = uvdt.Select("Time = '" + (Convert.ToDateTime(uvdt.Rows[0]["Time"]).AddSeconds(2)).ToString() + "'");
        //    switch (uvrw.Length)
        //    {
        //        case 20:
        //        case 10:
        //        case 5:
        //            vps = uvrw.Length;
        //            break;
        //    }
        //    if (uvrw.Length == 1)
        //    {
        //        DateTime t1 = Convert.ToDateTime(uvdt.Rows[1]["Time"]);
        //        DateTime t0 = Convert.ToDateTime(uvdt.Rows[0]["Time"]);
        //        TimeSpan ts = t1 - t0;
        //        switch (ts.Seconds)
        //        {
        //            case 1:
        //                vps = 1;
        //                break;
        //            case 2:
        //                vps = 2;
        //                break;
        //            case 5:
        //                vps = 5;
        //                break;
        //            case 10:
        //                vps = 10;
        //                break;
        //        }
        //    }
        //    return vps;
        //}

        public static double GetCurvRuler_xMax(DataTable uvdt)
        {
            //DateTime t1 = Convert.ToDateTime(uvdt.Rows[uvdt.Rows.Count-1]["Time"]);
            //DateTime t0 = Convert.ToDateTime(uvdt.Rows[0]["Time"]);
            //TimeSpan ts = t1 - t0;

            //return ts.TotalMinutes;
            return (double)(uvdt.Rows.Count) / t_UVPara.vps / 60;
        }
        public static double GetCurvRuler_yMax(DataTable uvdt)
        {
            double max0 = 0, max1 = 0, max2 = 0;
            max0 = Convert.ToDouble(uvdt.Compute("Max(Curv0)", "true"));
            if (t_UVPara.uvWaveLengthCnt > 1) max1 = Convert.ToDouble(uvdt.Compute("Max(Curv1)", "true"));
            if (t_UVPara.uvWaveLengthCnt > 2) max2 = Convert.ToDouble(uvdt.Compute("Max(Curv2)", "true"));

            if (t_UVPara.uvWaveLengthCnt == 1) return max0;
            if (t_UVPara.uvWaveLengthCnt == 2) return System.Math.Max(max0, max1);
            if (t_UVPara.uvWaveLengthCnt == 3) return System.Math.Max(System.Math.Max(max0, max1), max2);
            return 100;
        }
        public static double GetCurvRuler_yMin(DataTable uvdt)
        {
            double min0 = 0, min1 = 0, min2 = 0;
            min0 = Convert.ToDouble(uvdt.Compute("Min(Curv0)", "true"));
            if (t_UVPara.uvWaveLengthCnt > 1) min1 = Convert.ToDouble(uvdt.Compute("Min(Curv1)", "true"));
            if (t_UVPara.uvWaveLengthCnt > 2) min2 = Convert.ToDouble(uvdt.Compute("Min(Curv2)", "true"));

            if (t_UVPara.uvWaveLengthCnt == 1) return min0;
            if (t_UVPara.uvWaveLengthCnt == 2) return System.Math.Min(min0, min1);
            if (t_UVPara.uvWaveLengthCnt == 3) return System.Math.Min(System.Math.Min(min0, min1), min2);
            return -100;
        }
    }
}
