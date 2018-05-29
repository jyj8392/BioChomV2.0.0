using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Pump
{
    public partial class GradCurvShow : UserControl
    {
        public GradCurvShow()
        {
            InitializeComponent();
        }

        public struct picRuler
        {
            internal double y_Max;
            internal double y_Min;
            internal double x_Max;
            internal double x_Min;
            internal double rate;

            internal string curvX_unit;
            internal string curvY_unit;

            internal Color curvA_Color;
            internal Color curvB_Color;
            internal Color curvC_Color;
            internal Color curvD_Color;

            internal bool curvA_Visible;
            internal bool curvB_Visible;
            internal bool curvC_Visible;
            internal bool curvD_Visible;

            internal bool isFreshEn;
        };
        public static picRuler GradCurvRuler;

        public static DataTable GradCurvDataTable;

        private void GradCurvRulerPic_Paint(object sender, PaintEventArgs e)
        {
            Graphics ruler_pen = e.Graphics;
            ruler_pen.DrawLine(new Pen(Color.Black, 1), GradCurvArea.Left - 1, 0, GradCurvArea.Left - 1, GradCurvArea.Top + GradCurvArea.Height - 1);
            ruler_pen.DrawLine(new Pen(Color.Black, 1), GradCurvArea.Left - 1, GradCurvArea.Top + GradCurvArea.Height, GradCurvRulerPic.Left + GradCurvRulerPic.Width, GradCurvArea.Top + GradCurvArea.Height);
            try
            {
                Point startPoint = new Point();
                Point endPoint = new Point();
                if (GradCurvRuler.x_Max - GradCurvRuler.x_Min >= 0.005)
                {
                    startPoint.X = GradCurvArea.Left - 1;
                    startPoint.Y = GradCurvArea.Top + GradCurvArea.Height + 5;
                    for (int i = 1; startPoint.X < GradCurvRulerPic.Width; ++i)
                    {
                        double rulerInterval = GetRulerInterval(GradCurvRuler.x_Max, GradCurvRuler.x_Min) * GradCurvRuler.rate;
                        double rulerNowValue = Math.Round(GetRulerStart(GradCurvRuler.x_Min, rulerInterval) + (i - 1) * rulerInterval, 3);
                        startPoint.X = Convert.ToInt32((rulerNowValue - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * (GradCurvArea.Width) + GradCurvArea.Left - 1);
                        if (startPoint.X > GradCurvRulerPic.Width) break;

                        Graphics len = CreateGraphics();
                        SizeF size = len.MeasureString(rulerNowValue.ToString(), new Font("宋体", 9));
                        ruler_pen.DrawString(rulerNowValue.ToString(),
                                (new Font("宋体", 9)), (new SolidBrush(Color.Black)),
                                startPoint.X - size.Width / 2, startPoint.Y);
                        endPoint.X = startPoint.X;
                        endPoint.Y = startPoint.Y - 5;
                        ruler_pen.DrawLine(new Pen(Color.Black, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                    }
                }
                if (GradCurvRuler.y_Max - GradCurvRuler.y_Min >= 0.005)
                {
                    startPoint.X = GradCurvArea.Left - 1;
                    for (int i = 1; startPoint.Y > GradCurvArea.Top - 5; ++i)
                    {
                        double rulerInterval = GetRulerInterval(GradCurvRuler.y_Max, GradCurvRuler.y_Min) * GradCurvRuler.rate;
                        double rulerNowValue = Math.Round(GetRulerStart(GradCurvRuler.y_Min, rulerInterval) + (i - 1) * rulerInterval, 3);
                        startPoint.Y = GradCurvArea.Top + GradCurvArea.Height - Convert.ToInt32((rulerNowValue - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);

                        if (startPoint.Y < GradCurvArea.Top - 2)
                            break;
                        Graphics len = CreateGraphics();
                        SizeF size = len.MeasureString(rulerNowValue.ToString(), new Font("宋体", 9));
                        ruler_pen.DrawString(rulerNowValue.ToString(),
                                (new Font("宋体", 9)), (new SolidBrush(Color.Black)),
                                startPoint.X - size.Width - 0, startPoint.Y - size.Height / 2);

                        endPoint.X = startPoint.X - 3;
                        endPoint.Y = startPoint.Y;
                        ruler_pen.DrawLine(new Pen(Color.Black, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                    }
                }
            }
            catch
            {

            }
        }

        private void GradCurvArea_Paint(object sender, PaintEventArgs e)
        {
            if (GradCurvRuler.x_Max - GradCurvRuler.x_Min < 0.005) return;
            if (GradCurvRuler.y_Max - GradCurvRuler.y_Min < 0.005) return;
            try
            {
                Point startPoint = new Point();
                Point endPoint = new Point();
                Graphics curv_pen = e.Graphics;
                /*********A%*********/
                if (GradCurvRuler.curvA_Visible)
                {
                    startPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[0]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                    startPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[0]["FlowA"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                    for (int i = 0; i < GradCurvDataTable.Rows.Count; ++i)
                    {
                        endPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[i]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                        endPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[i]["FlowA"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                        curv_pen.DrawLine(new Pen(GradCurvRuler.curvA_Color, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                        startPoint.X = endPoint.X;
                        startPoint.Y = endPoint.Y;
                    }
                }
                /*********B%*********/
                if (GradCurvRuler.curvB_Visible)
                {
                    startPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[0]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                    startPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[0]["FlowB"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                    for (int i = 0; i < GradCurvDataTable.Rows.Count; ++i)
                    {
                        endPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[i]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                        endPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[i]["FlowB"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                        curv_pen.DrawLine(new Pen(GradCurvRuler.curvB_Color, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                        startPoint.X = endPoint.X;
                        startPoint.Y = endPoint.Y;
                    }
                }
                /*********C%*********/
                if (GradCurvRuler.curvC_Visible)
                {
                    startPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[0]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                    startPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[0]["FlowC"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                    for (int i = 0; i < GradCurvDataTable.Rows.Count; ++i)
                    {
                        endPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[i]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                        endPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[i]["FlowC"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                        curv_pen.DrawLine(new Pen(GradCurvRuler.curvC_Color, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                        startPoint.X = endPoint.X;
                        startPoint.Y = endPoint.Y;
                    }
                }
                /*********D%*********/
                if (GradCurvRuler.curvD_Visible)
                {
                    startPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[0]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                    startPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[0]["FlowD"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                    for (int i = 0; i < GradCurvDataTable.Rows.Count; ++i)
                    {
                        endPoint.X = Convert.ToInt32((Convert.ToDouble(GradCurvDataTable.Rows[i]["Time"]) - GradCurvRuler.x_Min) / (GradCurvRuler.x_Max - GradCurvRuler.x_Min) * GradCurvArea.Width);
                        endPoint.Y = Convert.ToInt32(GradCurvArea.Height - (Convert.ToDouble(GradCurvDataTable.Rows[i]["FlowD"]) - GradCurvRuler.y_Min) / (GradCurvRuler.y_Max - GradCurvRuler.y_Min) * GradCurvArea.Height);
                        curv_pen.DrawLine(new Pen(GradCurvRuler.curvD_Color, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                        startPoint.X = endPoint.X;
                        startPoint.Y = endPoint.Y;
                    }
                }
            }
            catch
            {

            }
        }

        private void GradCurvShow_Paint(object sender, PaintEventArgs e)
        {
            Graphics len = CreateGraphics();
            SizeF size = len.MeasureString("100", new Font("宋体", 9));
            GradCurvArea.Left = Convert.ToInt32(size.Width);
            GradCurvArea.Top = Convert.ToInt32(size.Height / 2);
            if (this.Width > 0 && this.Height > 0)
            {
                GradCurvArea.Width = GradCurvRulerPic.Width - GradCurvArea.Left - Convert.ToInt32(size.Width / 2);
                GradCurvArea.Height = GradCurvRulerPic.Height - GradCurvArea.Top - Convert.ToInt32(size.Height) - 5;
            }
        }

        private void refresher_Tick(object sender, EventArgs e)
        {
            if (GradCurvRuler.isFreshEn)
            {
                GradCurvRuler.isFreshEn = false;
                GradCurvRulerPic.Refresh();
                GradCurvArea.Refresh();
            }
        }

        /****************************************************************/
        private int GetRulerInterval(double max, double min)
        {
            int[] rulerInterval = { 1, 2, 5 };
            int i = 0;

            double test_rate = 0.001;

            while ((max - min) / (test_rate * rulerInterval[i]) < 2 ||
                (max - min) / (test_rate * rulerInterval[i]) > 7)
            {
                i++;
                if (i > 2)
                {
                    i = 0;
                    test_rate = test_rate * 10;
                }
            }

            GradCurvRuler.rate = test_rate;
            return rulerInterval[i];
        }
        private double GetRulerStart(double min, double interval)
        {
            double sub;
            sub = System.Math.Abs(min) - interval;
            while (sub >= 0)
            {
                sub = sub - interval;
            }
            sub = sub + interval;
            double ret;
            if (min > 0)
                ret = min - sub + interval;
            else
                ret = min + sub;

            return ret;
        }

        /****************************************************************/
        public static void SetGradCurv_Visible(string curvX, bool isVisible)
        {
            switch (curvX)
            {
                case "A":
                    GradCurvRuler.curvA_Visible = isVisible;
                    break;
                case "B":
                    GradCurvRuler.curvB_Visible = isVisible;
                    break;
                case "C":
                    GradCurvRuler.curvC_Visible = isVisible;
                    break;
                case "D":
                    GradCurvRuler.curvD_Visible = isVisible;
                    break;
            }
            GradCurvRuler.isFreshEn = true;
        }
        public static void SetGradCurv_Color(string curvX, Color curvColor)
        {
            switch (curvX)
            {
                case "A":
                    GradCurvRuler.curvA_Color = curvColor;
                    break;
                case "B":
                    GradCurvRuler.curvB_Color = curvColor;
                    break;
                case "C":
                    GradCurvRuler.curvC_Color = curvColor;
                    break;
                case "D":
                    GradCurvRuler.curvD_Color = curvColor;
                    break;
            }
            GradCurvRuler.isFreshEn = true;
        }
        public static void SetGradCurv_xMax(double xMax)
        {
            if (xMax - GradCurvRuler.x_Min < 0.005) return;
            GradCurvRuler.x_Max = xMax;
            GradCurvRuler.isFreshEn = true;
        }
        public static double GetGradCurv_xMax()
        {
            return GradCurvRuler.x_Max;
        }
        public static void SetGradCurv_xMin(double xMin)
        {
            if (GradCurvRuler.x_Max - xMin < 0.005) return;
            GradCurvRuler.x_Min = xMin;
            GradCurvRuler.isFreshEn = true;
        }
        public static double GetGradCurv_xMin()
        {
            return GradCurvRuler.x_Min;
        }
        public static void SetGradCurv_xUnit(string xUnit)
        {
            switch (xUnit)
            {
                case "min":
                    if (GradCurvRuler.curvX_unit == "h")
                    {
                        GradCurvRuler.x_Max = GradCurvRuler.x_Max * 60;
                        GradCurvRuler.x_Min = GradCurvRuler.x_Min * 60;
                    }
                    break;
                case "h":
                    if (GradCurvRuler.curvX_unit == "min")
                    {
                        if (GradCurvRuler.x_Max / 60 - GradCurvRuler.x_Min / 60 < 0.005) return;
                        GradCurvRuler.x_Max = GradCurvRuler.x_Max / 60;
                        GradCurvRuler.x_Min = GradCurvRuler.x_Min / 60;
                    }
                    break;
            }
            GradCurvRuler.curvX_unit = xUnit;
            GradCurvRuler.isFreshEn = true;
        }
        public static void SetGradCurv_yMax(double yMax)
        {
            if (yMax - GradCurvRuler.y_Min < 0.005) return;
            GradCurvRuler.y_Max = yMax;
            GradCurvRuler.isFreshEn = true;
        }
        public static double GetGradCurv_yMax()
        {
            return GradCurvRuler.y_Max;
        }
        public static void SetGradCurv_yMin(double yMin)
        {
            if (GradCurvRuler.y_Max - yMin < 0.005) return;
            GradCurvRuler.y_Min = yMin;
            GradCurvRuler.isFreshEn = true;
        }
        public static double GetGradCurv_yMin()
        {
            return GradCurvRuler.y_Min;
        }
        public static void SetGradCurv_yUnit(string yUnit)
        {
            switch (yUnit)
            {
                case "mAu":
                    if (GradCurvRuler.curvY_unit == "Au")
                    {
                        GradCurvRuler.y_Max = GradCurvRuler.y_Max * 1000;
                        GradCurvRuler.y_Min = GradCurvRuler.y_Min * 1000;
                    }
                    break;
                case "Au":
                    if (GradCurvRuler.curvY_unit == "mAu")
                    {
                        if (GradCurvRuler.y_Max / 1000 - GradCurvRuler.y_Min / 1000 < 0.005) return;
                        GradCurvRuler.y_Max = GradCurvRuler.y_Max / 1000;
                        GradCurvRuler.y_Min = GradCurvRuler.y_Min / 1000;
                    }
                    break;
            }
            GradCurvRuler.curvY_unit = yUnit;
            GradCurvRuler.isFreshEn = true;
        }


        public static void SetGradCurv_DataTable(DataTable pumpDataTable)
        {
            if (pumpDataTable == null)
            {
                GradCurvRuler.x_Max = 0;
                GradCurvRuler.x_Min = 0;
                GradCurvRuler.y_Max = 0;
                GradCurvRuler.y_Min = 0;
                GradCurvDataTable = null;
                GradCurvRuler.isFreshEn = true;
            }
            else {
                GradCurvDataTable = pumpDataTable.Copy();
                GradCurvRuler.isFreshEn = true;
            }
        }
        /****************************************************************/
    }
}
