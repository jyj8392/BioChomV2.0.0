using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using NPOI.SS.UserModel;

namespace Report
{
    public class Report
    {
        public static void SaveAsDOC(string path)
        {

        }

        public static void SaveAsXLS(string path, DataTable dt, double vps)
        {
            HSSFWorkbook wk;
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                wk = new HSSFWorkbook(fs);
            }
            ISheet sheet2 = wk.GetSheetAt(1);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                sheet2.CreateRow(i).CreateCell(0, CellType.NUMERIC).SetCellValue((Convert.ToDouble(dt.Rows[i]["ID"]) - 1) / vps / 60);
                sheet2.GetRow(i).CreateCell(1, CellType.NUMERIC).SetCellValue(Convert.ToDouble(dt.Rows[i]["Curv0"]));
                if (Convert.ToInt32(dt.Rows[0]["UVType"]) > 1) sheet2.GetRow(i).CreateCell(2, CellType.NUMERIC).SetCellValue(Convert.ToDouble(dt.Rows[i]["Curv1"]));
                if (Convert.ToInt32(dt.Rows[0]["UVType"]) > 2) sheet2.GetRow(i).CreateCell(3,CellType.NUMERIC).SetCellValue(Convert.ToDouble(dt.Rows[i]["Curv2"]));
            }
            sheet2.GetRow(0).CreateCell(4, CellType.STRING).SetCellValue("波长：" + dt.Rows[0]["Curv0WaveLength"].ToString());
            if (Convert.ToInt32(dt.Rows[0]["UVType"]) > 1) sheet2.GetRow(1).CreateCell(4, CellType.STRING).SetCellValue("波长：" + dt.Rows[0]["Curv1WaveLength"].ToString());
            if (Convert.ToInt32(dt.Rows[0]["UVType"]) > 2) sheet2.GetRow(2).CreateCell(4, CellType.STRING).SetCellValue("波长：" + dt.Rows[0]["Curv2WaveLength"].ToString());
            ISheet sheet1 = wk.GetSheetAt(0);
            sheet1.GetRow(0).CreateCell(1, CellType.STRING).SetCellValue(dt.Rows[0]["Time"].ToString());
            sheet1.GetRow(1).CreateCell(1, CellType.STRING).SetCellValue(dt.Rows[dt.Rows.Count - 1]["Time"].ToString());
            using (FileStream ftm = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                wk.Write(ftm);
                ftm.Close();
            }
        }

        public static void SaveAsPDF(string path)
        {

        }

    }
}
