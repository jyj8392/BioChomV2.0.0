using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace UVDetector
{
    public partial class CurvShow : UserControl
    {
        private CurvShow instance;

        public CurvShow()
        {
            InitializeComponent();
        }


        private struct picMove
        {
            internal int currentX;
            internal int currentY;
            internal bool isMoving;
            internal PictureBox anchor;
        };
        private static picMove CurvMove;

        public struct picRuler
        {
            internal double dX;
            internal double dY;
            internal double y_Max;
            internal double y_Min;
            internal double x_Max;
            internal double x_Min;
            internal double curvY_Max;
            internal double curvY_Min;
            internal double curvX_Max;
            internal double curvX_Min;
            internal double valY_Max;
            internal double valY_Min;
            internal double valX_Max;
            internal double valX_Min;
            internal double rate;

            internal string curvX_unit;
            internal string curvY_unit;

            internal Color curv0Color;
            internal Color curv1Color;
            internal Color curv2Color;

            internal bool isFreshEn;
        };
        public static picRuler CurvRuler;

        public static bool[] buttleSelected;
        public static int buttleSelectedNo;

        private static Thread th_Manager;
        private static Thread th_tick1;
        private static Thread th_tick2;
        private static Thread th_curv;
        private static Thread th_ruler;

        private void CurvShow_Load(object sender, EventArgs e)
        {
            instance = this;

            CurvMove.currentX = 0;
            CurvMove.currentY = 0;
            CurvMove.isMoving = false;
            CurvRuler.isFreshEn = true;
            UVDetector.UV.uvValue_DataTable = null;
            
            UVDetector.UV.t_UVPara.vps = 10;
            CurvRuler.x_Max = 10;
            CurvRuler.x_Min = 0;
            CurvRuler.y_Max = 100;
            CurvRuler.y_Min = -100;
            CurvRuler.curvX_Max = CurvRuler.x_Max;
            CurvRuler.curvX_Min = CurvRuler.x_Min;
            CurvRuler.curvY_Max = CurvRuler.y_Max;
            CurvRuler.curvY_Min = CurvRuler.y_Min;
            CurvRuler.curvX_unit = "min";
            CurvRuler.curvY_unit = "mAu";

            buttleSelected = new bool[120];

            if (th_Manager == null)
            {
                th_Manager = new Thread(curvManager);
                th_Manager.Start();
            }
        }

        private void CollectState_Click(object sender, EventArgs e)
        {
            int index = 0;
            Point p = CollectState.PointToClient(Control.MousePosition);
            double curvInterval = 60000 * (CurvRuler.x_Max - CurvRuler.x_Min) / CurvArea.Width;
            if (CurvRuler.curvX_unit == "h") curvInterval = curvInterval * 60;

            switch (CurvRuler.curvX_unit)
            {
                case "min":
                    if (p.X * curvInterval + CurvRuler.x_Min * 60000 <= 0)
                        index = 0;
                    else
                        index = Convert.ToInt32(Math.Floor((p.X * curvInterval + CurvRuler.x_Min * 60000) / (1000 / UVDetector.UV.t_UVPara.vps)) + 1);
                    break;
                case "h":
                    if (p.X * curvInterval + CurvRuler.x_Min * 3600000 <= 0)
                        index = 0;
                    else
                        index = Convert.ToInt32(Math.Floor((p.X * curvInterval + CurvRuler.x_Min * 3600000) / (1000 / UVDetector.UV.t_UVPara.vps)) + 1);
                    break;
            }
            buttleSelectedNo = Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]);
            if (buttleSelectedNo != 0)
            {
                if (buttleSelected[buttleSelectedNo - 1])
                {
                    buttleSelected[buttleSelectedNo - 1] = false;
                } else
                {
                    buttleSelected[buttleSelectedNo - 1] = true;
                }              
            }

        }

        private void CurvArea_MouseMove(object sender, MouseEventArgs e)
        {
            //Point p = new Point();
            //if (e.Button == MouseButtons.Left)
            //{
            //    p = CurvMove.anchor.PointToClient(Control.MousePosition);
            //    if ((Math.Abs(CurvMove.currentX - p.X) > 1 || Math.Abs(CurvMove.currentY - p.Y) > 1))
            //    {
            //        CurvMove.isMoving = true;
            //        CurvRuler.isFreshEn = false;
            //    }
            //    else
            //    {
            //        CurvMove.isMoving = false;
            //    }
            //}

            if (CurvMove.isMoving)
            {
                Point p = CurvMove.anchor.PointToClient(Control.MousePosition);
                //CurvArea.Left = CurvArea.Left + p.X - CurvMove.currentX;
                //CurvArea.Top = CurvArea.Top + p.Y - CurvMove.currentY;
                CurvMove.anchor.Left = CurvMove.anchor.Left + p.X - CurvMove.currentX;
                CurvMove.anchor.Top = CurvMove.anchor.Top + p.Y - CurvMove.currentY;
                CurvRuler.dX = CurvMove.anchor.Left * (CurvRuler.curvX_Max - CurvRuler.curvX_Min) / CurvArea.Width;
                CurvRuler.dY = CurvMove.anchor.Top * (CurvRuler.curvY_Max - CurvRuler.curvY_Min) / CurvArea.Height;
                CurvRuler.x_Max = CurvRuler.curvX_Max - CurvRuler.dX;
                CurvRuler.x_Min = CurvRuler.curvX_Min - CurvRuler.dX;
                CurvRuler.y_Max = CurvRuler.curvY_Max + CurvRuler.dY;
                CurvRuler.y_Min = CurvRuler.curvY_Min + CurvRuler.dY;
            }
        }
        private void CurvArea_MouseEnter(object sender, EventArgs e)
        {
            CurvArea.Focus();
        }
        private void CurvArea_MouseLeave(object sender, EventArgs e)
        {
            CurvRulerPic.Focus();
        }
        private void CurvArea_MouseWheel(object sender, MouseEventArgs e)
        {
            double delta_xMax = (CurvRuler.x_Max - CurvRuler.x_Min) * (CurvArea.Width - e.X) / CurvArea.Width / 10;
            double delta_xMin = (CurvRuler.x_Max - CurvRuler.x_Min) * e.X / CurvArea.Width / 10;
            double delta_yMax = (CurvRuler.y_Max - CurvRuler.y_Min) * e.Y / CurvArea.Height / 10;
            double delta_yMin = (CurvRuler.y_Max - CurvRuler.y_Min) * (CurvArea.Height - e.Y) / CurvArea.Height / 10;

            if (e.Delta < 0)
            {
                CurvRuler.x_Max = CurvRuler.x_Max + delta_xMax;
                CurvRuler.x_Min = CurvRuler.x_Min - delta_xMin;
                CurvRuler.y_Max = CurvRuler.y_Max + delta_yMax;
                CurvRuler.y_Min = CurvRuler.y_Min - delta_yMin;
            }
            else
            {
                if (CurvRuler.x_Max - delta_xMax - (CurvRuler.x_Min + delta_xMin) >= 0.005 
                    && CurvRuler.y_Max - delta_yMax - (CurvRuler.y_Min + delta_yMin) >= 0.005)
                {
                    CurvRuler.x_Max = CurvRuler.x_Max - delta_xMax;
                    CurvRuler.x_Min = CurvRuler.x_Min + delta_xMin;
                    CurvRuler.y_Max = CurvRuler.y_Max - delta_yMax;
                    CurvRuler.y_Min = CurvRuler.y_Min + delta_yMin;
                }
            }
            CurvRuler.curvX_Max = CurvRuler.x_Max;
            CurvRuler.curvX_Min = CurvRuler.x_Min;
            CurvRuler.dX = 0;
            CurvMove.anchor.Left = 0;
            CurvRuler.isFreshEn = false;
            CurvRuler.curvY_Max = CurvRuler.y_Max;
            CurvRuler.curvY_Min = CurvRuler.y_Min;
            CurvRuler.dY = 0;
            CurvMove.anchor.Top = 0;
        }

        private void CurvArea_MouseUp(object sender, MouseEventArgs e)
        {
            CurvMove.isMoving = false;
        }

        private void CurvArea_MouseDown(object sender, MouseEventArgs e)
        {
            CurvMove.isMoving = true;
            CurvRuler.isFreshEn = false;
            Point p = CurvMove.anchor.PointToClient(Control.MousePosition);
            CurvMove.currentX = p.X;
            CurvMove.currentY = p.Y;
        }

        private void CurvShow_Paint(object sender, PaintEventArgs e)
        {
            Graphics len = CreateGraphics();
            SizeF size = len.MeasureString("10000", new Font("宋体", 9));
            CurvArea.Left = Convert.ToInt32(size.Width) + 10;
            CurvArea.Top = Convert.ToInt32(size.Height / 2);
            if (this.Width > 0 && this.Height > 0)
            {
                CurvArea.Width = this.Width - CurvArea.Left - Convert.ToInt32(size.Width / 2);
                CurvArea.Height = this.Height - CurvArea.Top - Convert.ToInt32(size.Height) - 5;
            }
        }

        private void CurvShow_Layout(object sender, LayoutEventArgs e)
        {
            Graphics len = CreateGraphics();
            SizeF size = len.MeasureString("10000", new Font("宋体", 9));
            CurvArea.Left = Convert.ToInt32(size.Width) + 10;
            CurvArea.Top = Convert.ToInt32(size.Height / 2);
            CurvArea.Width = this.Width - CurvArea.Left - Convert.ToInt32(size.Width/2);
            CurvArea.Height = this.Height - CurvArea.Top - Convert.ToInt32(size.Height)-5;

            CurvMove.anchor = new PictureBox();
            CurvMove.anchor.Left = 0;
            CurvMove.anchor.Top = 0;
            CurvMove.anchor.Width = 1;
            CurvMove.anchor.Height = 1;
            CurvMove.anchor.Visible = false;
            CurvMove.anchor.PointToClient(Control.MousePosition);

            CollectState.Left = CurvArea.Left;
            CollectState.Height = 10;
            CollectState.Top = CurvArea.Top + CurvArea.Height - CollectState.Height;
            CollectState.Width = CurvArea.Width;

        }

        private void Curv0Color_Set_Click(object sender, EventArgs e)
        {
            //ColorDialog curvColor = new ColorDialog();
            //curvColor.ShowDialog();
            //CurvRuler.curv0Color = curvColor.Color;
            //Curv0Color_Set.BackColor = curvColor.Color;
        }
        private void Curv1Color_Set_Click(object sender, EventArgs e)
        {
            //ColorDialog curvColor = new ColorDialog();
            //curvColor.ShowDialog();
            //CurvRuler.curv1Color = curvColor.Color;
            //Curv1Color_Set.BackColor = curvColor.Color;
        }
        private void Curv2Color_Set_Click(object sender, EventArgs e)
        {
            //ColorDialog curvColor = new ColorDialog();
            //curvColor.ShowDialog();
            //CurvRuler.curv2Color = curvColor.Color;
            //Curv2Color_Set.BackColor = curvColor.Color;
        }

        //private static Stopwatch sw;
        private static int timer1Cnt = 0;
        protected void timer1out()
        {
            int interval = 0;
            timer1Cnt = 0;
            while (true)
            {
                switch (UVDetector.UV.t_UVPara.vps.ToString())
                {
                    case "10":
                        interval = 85;
                        break;
                    case "5":
                        interval = 170;
                        break;
                    case "4":
                        interval = 200;
                        break;
                    case "3":
                        interval = 250;
                        break;
                    case "2":
                        interval = 400;
                        break;
                    case "1":
                        interval = 950;
                        break;
                    case "0.5":
                        interval = 1900;
                        break;
                    case "0.2":
                        interval = 4900;
                        break;
                    case "0.1":
                        interval = 9800;
                        break;
                }
                Thread.Sleep(interval);
                timer1Cnt++;
                //UVDetector.UV.t_UVPara.data = UVDetector.UV.t_UVPara.data + 1;
                if (UVDetector.UV.t_UVPara.vps > 1)
                {
                    if (timer1Cnt <= Convert.ToInt32(UVDetector.UV.t_UVPara.vps))
                        UVDetector.UV.newDataCnt++;
                }
                else
                {
                    if (timer1Cnt <= 1)
                        UVDetector.UV.newDataCnt++;
                }
            }
        }
        protected void timer2out()
        {
            int interval = 0;
            timer1Cnt = 0;
            while (true)
            {
                switch (UVDetector.UV.t_UVPara.vps.ToString())
                {
                    case "10":
                    case "5":
                    case "4":
                    case "3":
                    case "2":
                    case "1":
                        interval = 1000;
                        break;
                    case "0.5":
                        interval = 2000;
                        break;
                    case "0.2":
                        interval = 5000;
                        break;
                    case "0.1":
                        interval = 10000;
                        break;
                }
                Thread.Sleep(interval);
                //UVDetector.UV.t_UVPara.data = 0;
                //sw.Stop();
                //TimeSpan ts2 = sw.Elapsed;
                //UVDetector.UV.newRowCnt = 0;
                timer1Cnt = 0;
                //th_tick1.Abort();
                //th_tick2.Abort();
            }
        }

        protected void curvManager()
        {
            th_tick1 = new Thread(timer1out);
            //th_tick1.Start();
            th_tick2 = new Thread(timer2out);
            //th_tick2.Start();
            th_ruler = new Thread(rulerpaint);
            th_ruler.Start();
            th_curv = new Thread(curvpaint);
            //th_curv.Start();
            while (true)
            {
                Thread.Sleep(500);
                if (isStartCollector)
                {
                    if (th_tick1.IsAlive == false)
                    {
                        th_tick1 = new Thread(timer1out);
                        th_tick1.Start();
                    }
                    if (th_tick2.IsAlive == false)
                    {
                        th_tick2 = new Thread(timer2out);
                        th_tick2.Start();
                    }
                    if (th_ruler.IsAlive == false)
                    {
                        th_ruler = new Thread(rulerpaint);
                        th_ruler.Start();
                    }
                    if (th_curv.IsAlive == false)
                    {
                        th_curv = new Thread(curvpaint);
                        th_curv.Start();
                    }
                }
             }
         }

        protected void curvpaint()
        {
            Bitmap curv_bmp, collectButtleSelect_bmp;
            try
            {
                curv_bmp = new Bitmap(CurvArea.Width, CurvArea.Height);
                collectButtleSelect_bmp = new Bitmap(CurvArea.Width, CollectState.Height);
            }
            catch
            {
                curv_bmp = new Bitmap(100, 100);
                collectButtleSelect_bmp = new Bitmap(100, CollectState.Height);
            }
            Graphics curv_pen = Graphics.FromImage(curv_bmp);
            Graphics collectButtleSelect_pen = Graphics.FromImage(collectButtleSelect_bmp);

            Point startPoint_BaseLine = new Point();
            Point endPoint_BaseLine = new Point();
            int showButtleNo = 0;

            this.Invoke(new EventHandler(delegate
            {
                try {
                    Curv0Visible.Checked = true;
                    Curv0Visible.BackColor = CurvArea.BackColor;
                    Curv0Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength1;
                    Curv0Visible.BringToFront();
                    Curv0Color_Set.BringToFront();
                    if (UVDetector.UV.t_UVPara.uvWaveLengthCnt > 1)
                    {
                        Curv1Visible.Checked = true;
                        Curv1Visible.BackColor = CurvArea.BackColor;
                        Curv1Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength2;
                        Curv1Visible.BringToFront();
                        Curv1Color_Set.BringToFront();
                    }
                    if (UVDetector.UV.t_UVPara.uvWaveLengthCnt > 2)
                    {
                        Curv2Visible.Checked = true;
                        Curv2Visible.BackColor = CurvArea.BackColor;
                        Curv2Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength3;
                        Curv2Visible.BringToFront();
                        Curv2Color_Set.BringToFront();
                    }

                } catch
                {

                }
            }));

            Thread.Sleep(10);
            while (true)
            {
                if (CurvMove.isMoving)
                    Thread.Sleep(10);
                else
                    Thread.Sleep(40);
                try
                {
                    Point[] startPoint = new Point[3];
                    Point[] endPoint = new Point[3];


                    if (UVDetector.UV.uvDataTableReadLock) continue;
                    CurvRuler.valX_Max = UVDetector.UV.uvValue_DataTable.Rows.Count / UVDetector.UV.t_UVPara.vps / 60;
                    CurvArea.Invoke(new EventHandler(delegate
                    {
                        if (curv_bmp.Width != CurvArea.Width || curv_bmp.Height != CurvArea.Height)
                        {
                            //curv_bmp.Dispose();
                            try
                            {
                                curv_bmp = new Bitmap(CurvArea.Width, CurvArea.Height);
                                collectButtleSelect_bmp = new Bitmap(CurvArea.Width, CollectState.Height);
                            }
                            catch
                            {
                                curv_bmp = new Bitmap(100, 100);
                                collectButtleSelect_bmp = new Bitmap(100, CollectState.Height);
                            }
                            curv_pen = Graphics.FromImage(curv_bmp);
                            collectButtleSelect_pen = Graphics.FromImage(collectButtleSelect_bmp);
                        }
                        curv_pen.Clear(CurvRulerPic.BackColor);
                        collectButtleSelect_pen.Clear(CurvRulerPic.BackColor);
                    }));

                    double curvInterval = 60000 * (CurvRuler.x_Max - CurvRuler.x_Min) / CurvArea.Width;
                    if (CurvRuler.curvX_unit == "h") curvInterval = curvInterval * 60;
                    int zeroPixIndex;
                    if (CurvRuler.x_Min >= 0)
                        zeroPixIndex = 0;
                    else {
                        zeroPixIndex = Convert.ToInt32(-60000 * CurvRuler.x_Min / curvInterval);
                        if (CurvRuler.curvX_unit == "h") zeroPixIndex = Convert.ToInt32(-3600000 * CurvRuler.x_Min / curvInterval);
                    }
                    for (int pixIndex = zeroPixIndex; pixIndex <= CurvArea.Width; ++pixIndex)
                    {
                        int index = 0;
                        switch (CurvRuler.curvX_unit)
                        {
                            case "min":
                                if (pixIndex * curvInterval + CurvRuler.x_Min * 60000 <= 0)
                                    index = 0;
                                else
                                    index = Convert.ToInt32( Math.Floor((pixIndex * curvInterval + CurvRuler.x_Min * 60000) / (1000 / UVDetector.UV.t_UVPara.vps)) + 1);
                                break;
                            case "h":
                                if (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 <= 0)
                                    index = 0;
                                else
                                    index = Convert.ToInt32(Math.Floor((pixIndex * curvInterval + CurvRuler.x_Min * 3600000) / (1000 / UVDetector.UV.t_UVPara.vps)) + 1);
                                break;                                
                        } 

                        if (index < UVDetector.UV.uvValue_DataTable.Rows.Count-2)
                        {
                            double[] val = {0, 0, 0};
                            double[] uvValueLow = new double[3];
                            double[] uvValueHigh = new double[3];
                            if (pixIndex == zeroPixIndex)
                            {
                                if (index > 0)
                                {
                                    if (Curv0Visible.Checked)
                                    {
                                        uvValueLow[0] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv0"]);
                                        uvValueHigh[0] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv0"]);
                                        if (CurvRuler.curvX_unit == "min")
                                            val[0] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[0] - uvValueLow[0]) + uvValueLow[0];
                                        else if (CurvRuler.curvX_unit == "h")
                                            val[0] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[0] - uvValueLow[0]) + uvValueLow[0];
                                    }
                                    if (Curv1Visible.Checked)
                                    {
                                        uvValueLow[1] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv1"]);
                                        uvValueHigh[1] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv1"]);
                                        if (CurvRuler.curvX_unit == "min")
                                            val[1] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[1] - uvValueLow[1]) + uvValueLow[1];
                                        else if (CurvRuler.curvX_unit == "h")
                                            val[1] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[1] - uvValueLow[1]) + uvValueLow[1];
                                    }
                                    if (Curv2Visible.Checked)
                                    {
                                        uvValueLow[2] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv2"]);
                                        uvValueHigh[2] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv2"]);
                                        if (CurvRuler.curvX_unit == "min")
                                            val[2] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[2] - uvValueLow[2]) + uvValueLow[2];
                                        else if (CurvRuler.curvX_unit == "h")
                                            val[2] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[2] - uvValueLow[2]) + uvValueLow[2];
                                    }
                                }
                                else
                                {
                                    if (Curv0Visible.Checked) val[0] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv0"]);
                                    if (Curv1Visible.Checked) val[1] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv1"]);
                                    if (Curv2Visible.Checked) val[2] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv2"]);
                                }
                                if (CurvRuler.curvY_unit == "Au") { val[0] = val[0] / 1000; val[1] = val[1] / 1000; val[2] = val[2] / 1000;}
                                if (Curv0Visible.Checked) startPoint[0].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[0] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                                if (Curv1Visible.Checked) startPoint[1].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[1] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                                if (Curv2Visible.Checked) startPoint[2].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[2] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));

                                if (Curv0Visible.Checked) CurvShow.CurvRuler.valY_Max = uvValueHigh[0];
                                if (Curv1Visible.Checked) CurvShow.CurvRuler.valY_Max = uvValueHigh[1];
                                if (Curv2Visible.Checked) CurvShow.CurvRuler.valY_Max = uvValueHigh[2];
                                if (Curv0Visible.Checked) CurvShow.CurvRuler.valY_Min = uvValueLow[0];
                                if (Curv1Visible.Checked) CurvShow.CurvRuler.valY_Min = uvValueLow[1];
                                if (Curv2Visible.Checked) CurvShow.CurvRuler.valY_Min = uvValueLow[2];
                            }
                            else if (index > 0)
                            {
                                if (Curv0Visible.Checked)
                                {
                                    uvValueLow[0] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv0"]);
                                    uvValueHigh[0] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv0"]);
                                    if (CurvRuler.curvX_unit == "min")
                                        val[0] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[0] - uvValueLow[0]) + uvValueLow[0];
                                    else if (CurvRuler.curvX_unit == "h")
                                        val[0] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[0] - uvValueLow[0]) + uvValueLow[0];
                                }
                                if (Curv1Visible.Checked)
                                {
                                    uvValueLow[1] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv1"]);
                                    uvValueHigh[1] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv1"]);
                                    if (CurvRuler.curvX_unit == "min")
                                        val[1] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[1] - uvValueLow[1]) + uvValueLow[1];
                                    else if (CurvRuler.curvX_unit == "h")
                                        val[1] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[1] - uvValueLow[1]) + uvValueLow[1];
                                }
                                if (Curv2Visible.Checked)
                                {
                                    uvValueLow[2] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index - 1]["Curv2"]);
                                    uvValueHigh[2] = Convert.ToDouble(UVDetector.UV.uvValue_DataTable.Rows[index]["Curv2"]);
                                    if (CurvRuler.curvX_unit == "min")
                                        val[2] = (pixIndex * curvInterval + CurvRuler.x_Min * 60000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[2] - uvValueLow[2]) + uvValueLow[2];
                                    else if (CurvRuler.curvX_unit == "h")
                                        val[2] = (pixIndex * curvInterval + CurvRuler.x_Min * 3600000 - (index - 1) * (1000 / UVDetector.UV.t_UVPara.vps)) / (1000 / UVDetector.UV.t_UVPara.vps) * (uvValueHigh[2] - uvValueLow[2]) + uvValueLow[2];
                                }
                                if (CurvRuler.curvY_unit == "Au") { val[0] = val[0] / 1000; val[1] = val[1] / 1000; val[2] = val[2] / 1000; }
                                if (Curv0Visible.Checked) endPoint[0].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[0] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                                if (Curv1Visible.Checked) endPoint[1].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[1] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                                if (Curv2Visible.Checked) endPoint[2].Y = Convert.ToInt32(CurvArea.Height - CurvArea.Height * (val[2] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));

                                if (Curv0Visible.Checked)
                                {
                                    startPoint[0].X = pixIndex;
                                    endPoint[0].X = startPoint[0].X;
                                    curv_pen.DrawLine(new Pen(CurvRuler.curv0Color, 1),
                                            startPoint[0].X - 1, startPoint[0].Y + CurvMove.anchor.Top,
                                            endPoint[0].X, endPoint[0].Y + CurvMove.anchor.Top);
                                    startPoint[0].Y = endPoint[0].Y;
                                }

                                if (Curv1Visible.Checked)
                                {
                                    startPoint[1].X = pixIndex;
                                    endPoint[1].X = startPoint[1].X;
                                    curv_pen.DrawLine(new Pen(CurvRuler.curv1Color, 1),
                                            startPoint[1].X - 1, startPoint[1].Y + CurvMove.anchor.Top,
                                            endPoint[1].X, endPoint[1].Y + CurvMove.anchor.Top);
                                    startPoint[1].Y = endPoint[1].Y;
                                }

                                if (Curv2Visible.Checked)
                                {
                                    startPoint[2].X = pixIndex;
                                    endPoint[2].X = startPoint[2].X;
                                    curv_pen.DrawLine(new Pen(CurvRuler.curv2Color, 1),
                                            startPoint[2].X - 1, startPoint[2].Y + CurvMove.anchor.Top,
                                            endPoint[2].X, endPoint[2].Y + CurvMove.anchor.Top);
                                    startPoint[2].Y = endPoint[2].Y;
                                }

                                //判峰
                                if (Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index-1]["isCollectorBottle"]) < Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]) && showButtleNo < Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index+1]["isCollectorBottle"]))
                                {
                                    //curv_pen.DrawLine(new Pen(Color.LightGreen, 5),
                                    //        startPoint_BaseLine.X - 1, CurvArea.Height - 5,
                                    //        pixIndex, CurvArea.Height - 5);
                                    showButtleNo = Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]);
                                    collectButtleSelect_pen.DrawLine(new Pen(Color.LightGreen, buttleSelected[showButtleNo - 1]?6:4),
                                            startPoint_BaseLine.X - 1, 5,
                                            pixIndex, 5);
                                    startPoint_BaseLine.X = pixIndex;
                                    curv_pen.DrawLine(new Pen(Color.LightBlue, 1),
                                            startPoint_BaseLine.X, 0,
                                            startPoint_BaseLine.X, CurvArea.Height);
                                    curv_pen.DrawString(showButtleNo.ToString(),
                                            (new Font("宋体", 9)), (new SolidBrush(Color.Black)),
                                            startPoint_BaseLine.X + 1, 0);
                                }
                                if (Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index-1]["isCollectorBottle"]) == Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]) && showButtleNo == Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]))
                                {
                                    if (showButtleNo == 0)
                                    {
                                        startPoint_BaseLine.X = pixIndex;
                                        //curv_pen.DrawLine(new Pen(Color.LightGray, 5),
                                        //        startPoint_BaseLine.X - 1, CurvArea.Height - 5,
                                        //        startPoint_BaseLine.X, CurvArea.Height - 5);
                                        collectButtleSelect_pen.DrawLine(new Pen(Color.LightGray, 4),
                                                startPoint_BaseLine.X - 1, 5,
                                                startPoint_BaseLine.X, 5);
                                    }
                                    else
                                    {
                                        //curv_pen.DrawLine(new Pen(Color.LightGreen, 5),
                                        //        startPoint_BaseLine.X - 1, CurvArea.Height - 5,
                                        //        startPoint_BaseLine.X, CurvArea.Height - 5);
                                        collectButtleSelect_pen.DrawLine(new Pen(Color.LightGreen, buttleSelected[showButtleNo - 1] ? 6 : 4),
                                                startPoint_BaseLine.X - 1, 5,
                                                startPoint_BaseLine.X, 5);
                                    }
                                    startPoint_BaseLine.X++;
                                }
                                if (Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index]["isCollectorBottle"]) > Convert.ToInt32(UVDetector.UV.uvValue_DataTable.Rows[index+2]["isCollectorBottle"]) && showButtleNo != 0)
                                {
                                    endPoint_BaseLine.X = pixIndex;
                                    //curv_pen.DrawLine(new Pen(Color.LightGreen, 5),
                                    //        startPoint_BaseLine.X - 1, CurvArea.Height - 5,
                                    //        endPoint_BaseLine.X, CurvArea.Height - 5);
                                    collectButtleSelect_pen.DrawLine(new Pen(Color.LightGreen, buttleSelected[showButtleNo - 1] ? 6 : 4),
                                            startPoint_BaseLine.X - 1, 5,
                                            endPoint_BaseLine.X, 5);
                                    curv_pen.DrawLine(new Pen(Color.LightBlue, 1),
                                            endPoint_BaseLine.X, 0,
                                            endPoint_BaseLine.X, CurvArea.Height);
                                    showButtleNo = 0;
                                }
                                //获取Y轴最大最小值
                                if (Curv0Visible.Checked && CurvShow.CurvRuler.valY_Max < uvValueHigh[0]) CurvShow.CurvRuler.valY_Max = uvValueHigh[0];
                                if (Curv1Visible.Checked && CurvShow.CurvRuler.valY_Max < uvValueHigh[1]) CurvShow.CurvRuler.valY_Max = uvValueHigh[1];
                                if (Curv2Visible.Checked && CurvShow.CurvRuler.valY_Max < uvValueHigh[2]) CurvShow.CurvRuler.valY_Max = uvValueHigh[2];
                                if (Curv0Visible.Checked && CurvShow.CurvRuler.valY_Min > uvValueLow[0]) CurvShow.CurvRuler.valY_Min = uvValueLow[0];
                                if (Curv1Visible.Checked && CurvShow.CurvRuler.valY_Min > uvValueLow[1]) CurvShow.CurvRuler.valY_Min = uvValueLow[1];
                                if (Curv2Visible.Checked && CurvShow.CurvRuler.valY_Min > uvValueLow[2]) CurvShow.CurvRuler.valY_Min = uvValueLow[2];
                                //if (CurvShow.CurvRuler.valY_Max < System.Math.Max(System.Math.Max(uvValueHigh[0], uvValueHigh[1]), uvValueHigh[2]) ) CurvShow.CurvRuler.valY_Max = System.Math.Max(System.Math.Max(uvValueHigh[0], uvValueHigh[1]), uvValueHigh[2]);
                                //if (CurvShow.CurvRuler.valY_Min > System.Math.Min(System.Math.Min(uvValueLow[0], uvValueLow[1]), uvValueLow[2])) CurvShow.CurvRuler.valY_Min = System.Math.Min(System.Math.Min(uvValueLow[0], uvValueLow[1]), uvValueLow[2]);
                            }
                        }

                        if (CurvRuler.isFreshEn && 
                            (UVDetector.UV.uvValue_DataTable.Rows.Count / UVDetector.UV.t_UVPara.vps / (CurvRuler.curvX_unit == "h"?3600:60) 
                            >= CurvRuler.x_Max))
                        {

                            CurvRuler.x_Max = (CurvRuler.x_Max - CurvRuler.x_Min) * 0.2 + CurvRuler.x_Max;
                            CurvRuler.curvX_Max = CurvRuler.x_Max;
                            CurvRuler.x_Min = CurvRuler.curvX_Min;
                            CurvRuler.dX = 0;

                            CurvMove.anchor.Invoke(new EventHandler(delegate
                            {
                                CurvMove.anchor.Left = 0;
                            }));
                        }
                    }
                    startPoint_BaseLine.X = 0;
                    endPoint_BaseLine.X = 0;

                    CurvArea.Invoke(new EventHandler(delegate
                    {
                        CurvArea.Image = curv_bmp;
                    }));
                    CollectState.Invoke(new EventHandler(delegate
                    {
                        CollectState.Image = collectButtleSelect_bmp;
                    }));
                    showButtleNo = 0;

                    this.Invoke(new EventHandler(delegate
                    {
                        Curv0Visible.Left = CurvArea.Left + CurvArea.Width - Curv0Visible.Width - 20;
                        Curv0Visible.Top = CurvArea.Top + 20;
                        Curv0Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength1;
                        Curv0Color_Set.Left = Curv0Visible.Left - 40;
                        Curv0Color_Set.Top = Curv0Visible.Top + Curv0Visible.Height / 2 - 2;
                        Curv0Color_Set.BackColor = CurvRuler.curv0Color;
                        Curv0Visible.Visible = true;
                        Curv0Color_Set.Visible = true;
                        if (UVDetector.UV.t_UVPara.uvWaveLengthCnt > 1)
                        {
                            Curv1Visible.Left = Curv0Visible.Left;
                            Curv1Visible.Top = Curv0Visible.Top + Curv0Visible.Height + 2;
                            Curv1Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength2;
                            Curv1Color_Set.Left = Curv1Visible.Left - 40;
                            Curv1Color_Set.Top = Curv1Visible.Top + Curv1Visible.Height / 2 - 2;
                            Curv1Color_Set.BackColor = CurvRuler.curv1Color;
                            Curv1Visible.Visible = true;
                            Curv1Color_Set.Visible = true;
                        }
                        if (UVDetector.UV.t_UVPara.uvWaveLengthCnt > 2)
                        {
                            Curv2Visible.Left = Curv0Visible.Left;
                            Curv2Visible.Top = Curv1Visible.Top + Curv1Visible.Height + 2;
                            Curv2Visible.Text = "波长：" + UVDetector.UV.t_UVPara.waveLength3;
                            Curv2Color_Set.Left = Curv2Visible.Left - 40;
                            Curv2Color_Set.Top = Curv2Visible.Top + Curv2Visible.Height / 2 - 2;
                            Curv2Color_Set.BackColor = CurvRuler.curv2Color;
                            Curv2Visible.Visible = true;
                            Curv2Color_Set.Visible = true;
                        }
                    }));
                }
                catch (Exception ex)
                {
                    //log_tmp = "curvpaint():" + ex.Message;
                    th_curv.Abort();
                }
            }
        }

        protected void rulerpaint()
        {
            Bitmap ruler_bmp;
            try
            {
                ruler_bmp = new Bitmap(CurvRulerPic.Width, CurvRulerPic.Height);
            }
            catch
            {
                ruler_bmp = new Bitmap(100, 100);
            }
            Graphics ruler_pen = Graphics.FromImage(ruler_bmp);


            Label yRulerLabel = new Label();
            Label xRulerLabel = new Label();
            this.Invoke(new EventHandler(delegate
            {
                try
                {
                    yRulerLabel.AutoSize = true;
                    yRulerLabel.BackColor = CurvArea.BackColor;
                    yRulerLabel.Text = "mAu";
                    yRulerLabel.Visible = true;
                    this.Controls.Add(yRulerLabel);
                    yRulerLabel.BringToFront();

                    xRulerLabel.AutoSize = true;
                    xRulerLabel.BackColor = CurvArea.BackColor;
                    xRulerLabel.Text = "min";
                    xRulerLabel.Visible = true;
                    this.Controls.Add(xRulerLabel);
                    xRulerLabel.BringToFront();
                }catch
                {

                }
            }));
            while (true)
            {
                if (CurvMove.isMoving)
                    Thread.Sleep(10);
                else
                    Thread.Sleep(40);
                try
                {
                    Point startPoint = new Point();
                    Point endPoint = new Point();
                    CurvRulerPic.Invoke(new EventHandler(delegate
                    {
                        if (ruler_bmp.Width != CurvRulerPic.Width || ruler_bmp.Height != CurvRulerPic.Height)
                        {
                            //ruler_bmp.Dispose();
                            try
                            {
                                ruler_bmp = new Bitmap(CurvRulerPic.Width, CurvRulerPic.Height);
                            }
                            catch
                            {
                                ruler_bmp = new Bitmap(100, 100);
                            }
                            ruler_pen = Graphics.FromImage(ruler_bmp);
                        }
                        ruler_pen.Clear(CurvRulerPic.BackColor);
                    }));
                    startPoint.X = CurvArea.Left + 1;
                    startPoint.Y = CurvArea.Top + CurvArea.Height;
                    for (int i = 1; startPoint.X < CurvRulerPic.Width; ++i)
                    {
                        double rulerInterval = GetRulerInterval(CurvRuler.x_Max, CurvRuler.x_Min) * CurvRuler.rate;
                        double rulerNowValue = Math.Round(GetRulerStart(CurvRuler.x_Min, rulerInterval) + (i - 1) * rulerInterval, 3);

                        startPoint.X = Convert.ToInt32((rulerNowValue - CurvRuler.x_Min) / (CurvRuler.x_Max - CurvRuler.x_Min) * CurvArea.Width + CurvArea.Left-1);
                        if (startPoint.X > CurvRulerPic.Width) break;

                        Graphics len = CreateGraphics();
                        SizeF size = len.MeasureString(rulerNowValue.ToString(), new Font("宋体", 9));
                        ruler_pen.DrawString(rulerNowValue.ToString(),
                                (new Font("宋体", 9)), (new SolidBrush(Color.Black)),
                                startPoint.X - size.Width / 2, startPoint.Y);
                    }
                    startPoint.X = CurvArea.Left - 2;
                    for (int i = 1; startPoint.Y > CurvArea.Top; ++i)
                    {
                        double rulerInterval = GetRulerInterval(CurvRuler.y_Max, CurvRuler.y_Min) * CurvRuler.rate;
                        double rulerNowValue = Math.Round(GetRulerStart(CurvRuler.y_Min, rulerInterval) + (i - 1) * rulerInterval, 3);
                        startPoint.Y = CurvArea.Top + CurvArea.Height - Convert.ToInt32((rulerNowValue - CurvRuler.y_Min) / (CurvRuler.y_Max - CurvRuler.y_Min) * CurvArea.Height);

                        if (startPoint.Y < CurvArea.Top - 2)
                            break;
                        Graphics len = CreateGraphics();
                        SizeF size = len.MeasureString(rulerNowValue.ToString(), new Font("宋体", 9));
                        ruler_pen.DrawString(rulerNowValue.ToString(),
                                (new Font("宋体", 9)), (new SolidBrush(Color.Black)),
                                startPoint.X - size.Width - 7, startPoint.Y - size.Height / 2);

                        endPoint.X = startPoint.X - 7;
                        endPoint.Y = startPoint.Y;
                        ruler_pen.DrawLine(new Pen(Color.LightGray, 1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
                    }
                    ruler_pen.DrawLine(new Pen(Color.LightGray, 1), CurvArea.Left-1, 0, CurvArea.Left-1, CurvArea.Top + CurvArea.Height-1);
                    ruler_pen.DrawLine(new Pen(Color.LightGray, 1), CurvArea.Left-1, CurvArea.Top + CurvArea.Height, CurvRulerPic.Left + CurvRulerPic.Width, CurvArea.Top + CurvArea.Height);
                    
                    CurvRulerPic.Invoke(new EventHandler(delegate
                    {
                        CurvRulerPic.Image = ruler_bmp;
                    }));

                    this.Invoke(new EventHandler(delegate
                    {
                        yRulerLabel.Left = CurvArea.Left + 10;
                        yRulerLabel.Top = CurvArea.Top;
                        yRulerLabel.Text = CurvRuler.curvY_unit;

                        xRulerLabel.Left = CurvArea.Left + CurvArea.Width - 15;
                        xRulerLabel.Top = CurvArea.Top + CurvArea.Height - 20;
                        xRulerLabel.Text = CurvRuler.curvX_unit;
                    }));
                }
                catch (Exception ex)
                {
                    //log_tmp = "rulerpaint():" + ex.Message;
                    th_ruler.Abort();
                }
            }
        }



        private int GetRulerInterval(double max, double min)
        {
            int[] rulerInterval = { 1, 2, 5 };
            int i = 0;

            double test_rate = 0.001;

            while ((max - min) / (test_rate * rulerInterval[i]) < 3 ||
                (max - min) / (test_rate * rulerInterval[i]) > 10)
            {
                i++;
                if (i > 2)
                {
                    i = 0;
                    test_rate = test_rate * 10;
                }
            }

            CurvRuler.rate = test_rate;
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



        /****************************接口函数****************************/
        private static string log_tmp;
        public static string GetLog()
        {
            return log_tmp;
        }
        public static void ClearLog()
        {
            log_tmp = "";
        }

        public static int GetWaveLengthCnt()
        {
            return UVDetector.UV.t_UVPara.uvWaveLengthCnt;
        }
        public static int GetWaveLength1()
        {
            return UVDetector.UV.t_UVPara.waveLength1;
        }
        public static int GetWaveLength2()
        {
            return UVDetector.UV.t_UVPara.waveLength2;
        }
        public static int GetWaveLength3()
        {
            return UVDetector.UV.t_UVPara.waveLength3;
        }

        public static void SetCurv_vps(double vps)
        {
            UVDetector.UV.t_UVPara.vps = vps;
        }
        public static void SetCurv_Color(Color curv0Color, Color curv1Color, Color curv2Color)
        {
            CurvRuler.curv0Color = curv0Color;
            CurvRuler.curv1Color = curv1Color;
            CurvRuler.curv2Color = curv2Color;
        }
        public static void SetCurv_xMax(double xMax)
        {
            if (xMax - CurvRuler.x_Min < 0.005) return;
            if (xMax < CurvRuler.valX_Max) CurvRuler.isFreshEn = false;
            else CurvRuler.isFreshEn = true;

            CurvRuler.x_Max = xMax;
            CurvRuler.curvX_Max = CurvRuler.x_Max;
            CurvRuler.dX = 0;
            CurvMove.anchor.Left = 0;
        }
        public static double GetCurv_xMax()
        {
            return CurvRuler.x_Max;
        }
        public static void SetCurv_xMin(double xMin)
        {
            if (CurvRuler.x_Max - xMin < 0.005) return;
            CurvRuler.x_Min = xMin;
            CurvRuler.curvX_Min = CurvRuler.x_Min;
            CurvRuler.dX = 0;
            CurvMove.anchor.Left = 0;
        }
        public static double GetCurv_xMin()
        {
            return CurvRuler.x_Min;
        }
        public static void SetCurv_xUnit(string xUnit)
        {
            switch (xUnit)
            {
                case "min":
                    if (CurvRuler.curvX_unit == "h")
                    {
                        CurvRuler.x_Max = CurvRuler.x_Max * 60;
                        CurvRuler.x_Min = CurvRuler.x_Min * 60;
                        CurvRuler.curvX_Max = CurvRuler.x_Max;
                        CurvRuler.curvX_Min = CurvRuler.x_Min;
                    }
                    break;
                case "h":
                    if (CurvRuler.curvX_unit == "min")
                    {
                        if (CurvRuler.x_Max/60 - CurvRuler.x_Min/60 < 0.005) return;
                        CurvRuler.x_Max = CurvRuler.x_Max / 60;
                        CurvRuler.x_Min = CurvRuler.x_Min / 60;
                        CurvRuler.curvX_Max = CurvRuler.x_Max;
                        CurvRuler.curvX_Min = CurvRuler.x_Min;
                    }
                    break;
            }
            CurvRuler.curvX_unit = xUnit;
        }
        public static string GetCurv_xUnit()
        {
            return CurvRuler.curvX_unit;
        }
        public static void SetCurv_yMax(double yMax)
        {
            if (yMax - CurvRuler.y_Min < 0.005) return;
            CurvRuler.y_Max = yMax;
            CurvRuler.curvY_Max = CurvRuler.y_Max;
            CurvRuler.dY = 0;
            CurvMove.anchor.Top = 0;
        }
        public static double GetCurv_yMax()
        {
            return CurvRuler.y_Max;
        }
        public static void SetCurv_yMin(double yMin)
        {
            if (CurvRuler.y_Max - yMin < 0.005) return;
            CurvRuler.y_Min = yMin;
            CurvRuler.curvY_Min = CurvRuler.y_Min;
            CurvRuler.dY = 0;
            CurvMove.anchor.Top = 0;
        }
        public static double GetCurv_yMin()
        {
            return CurvRuler.y_Min;
        }
        public static void SetCurv_yUnit(string yUnit)
        {
            switch (yUnit)
            {
                case "mAu":
                    if (CurvRuler.curvY_unit == "Au")
                    {
                        CurvRuler.y_Max = CurvRuler.y_Max * 1000;
                        CurvRuler.y_Min = CurvRuler.y_Min * 1000;
                        CurvRuler.curvY_Max = CurvRuler.y_Max;
                        CurvRuler.curvY_Min = CurvRuler.y_Min;
                    }
                    break;
                case "Au":
                    if (CurvRuler.curvY_unit == "mAu")
                    {
                        if (CurvRuler.y_Max/1000 - CurvRuler.y_Min/1000 < 0.005) return;
                        CurvRuler.y_Max = CurvRuler.y_Max / 1000;
                        CurvRuler.y_Min = CurvRuler.y_Min / 1000;
                        CurvRuler.curvY_Max = CurvRuler.y_Max;
                        CurvRuler.curvY_Min = CurvRuler.y_Min;
                    }
                    break;
            }
            CurvRuler.curvY_unit = yUnit;
        }
        public static string GetCurv_yUnit()
        {
            return CurvRuler.curvY_unit;
        }
        public static void SetCurvXtoMax()
        {
            if (CurvRuler.valX_Max - CurvRuler.valX_Min < 0.005) return;
            switch (CurvRuler.curvX_unit)
            {
                case "min":
                    CurvRuler.x_Max = CurvRuler.valX_Max * 1.1;
                    break;
                case "h":
                    CurvRuler.x_Max = CurvRuler.valX_Max * 1.1 / 60;
                    break;
            }
            CurvRuler.x_Min = 0;
            CurvRuler.curvX_Max = CurvRuler.x_Max;
            CurvRuler.curvX_Min = 0;
            CurvRuler.dX = 0;
            CurvMove.anchor.Left = 0;
            CurvRuler.isFreshEn = true;
        }
        public static void SetCurvYtoMax()
        {
            if (CurvRuler.valY_Max - CurvRuler.valY_Min < 0.005) return;
            switch (CurvRuler.curvY_unit)
            {
                case "mAu":
                    CurvRuler.y_Max = CurvRuler.valY_Max + (CurvRuler.valY_Max - CurvRuler.valY_Min) * 0.1;
                    CurvRuler.y_Min = CurvRuler.valY_Min - (CurvRuler.valY_Max - CurvRuler.valY_Min) * 0.1;
                    break;
                case "Au":
                    CurvRuler.y_Max = (CurvRuler.valY_Max + (CurvRuler.valY_Max - CurvRuler.valY_Min) * 0.1) / 1000;
                    CurvRuler.y_Min = (CurvRuler.valY_Min - (CurvRuler.valY_Max - CurvRuler.valY_Min) * 0.1) / 1000;
                    break;
            }
            CurvRuler.curvY_Max = CurvRuler.y_Max;
            CurvRuler.curvY_Min = CurvRuler.y_Min;
            CurvRuler.dY = 0;
            CurvMove.anchor.Top = 0;
        }

        public static double GetUVValue(string unit, int channel)
        {
            switch (unit)
            {
                case "mAu":
                    return UVDetector.UV.t_UVPara.data[channel];
                case "Au":
                    return UVDetector.UV.t_UVPara.data[channel] / 1000;
            }
            return 0;
        }
        public static double GetUVTime(string unit)
        {
            try {
                switch (unit)
                {
                    case "min":
                        //return ((TimeSpan)(DateTime.Now - Convert.ToDateTime(UVDetector.UV.uvValue_DataTable.Rows[0]["Time"])) - UVDetector.UV.accPauseTime).TotalMinutes;
                        return (double)(UVDetector.UV.uvValue_DataTable.Rows.Count) / UVDetector.UV.t_UVPara.vps / 60;
                    case "h":
                        return (double)(UVDetector.UV.uvValue_DataTable.Rows.Count) / UVDetector.UV.t_UVPara.vps / 3600;
                        //return ((TimeSpan)(DateTime.Now - Convert.ToDateTime(UVDetector.UV.uvValue_DataTable.Rows[0]["Time"])) - UVDetector.UV.accPauseTime).TotalHours;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public static bool GetStartSlop(Queue<double> curv, double slopThreshold)
        {
            double[] dat = new double[4];
            int i = 0;
            foreach (double x in curv) dat[i++] = x;

            if (dat[3] - dat[2] > slopThreshold &&
                dat[2] - dat[1] > slopThreshold &&
                dat[1] - dat[0] < slopThreshold)
            {
                return true;
            }
            return false;
        }
        public static bool GetStopSlop(Queue<double> curv, double slopThreshold)
        {
            double[] dat = new double[4];
            int i = 0;
            foreach (double x in curv) dat[i++] = x;

            if (dat[0] - dat[1] > slopThreshold &&
                dat[1] - dat[2] < slopThreshold &&
                dat[2] - dat[3] < slopThreshold)
            {
                return true;
            }
            return false;
        }


        private static bool isStartCollector = false;
        public static void StartCollector()
        {
            isStartCollector = true;
        }
        public static void StopCollector()
        {
            isStartCollector = false;
            if (th_tick1 != null && th_tick1.IsAlive) th_tick1.Abort();
            if (th_tick2 != null && th_tick2.IsAlive) th_tick2.Abort();
            //th_curv.Abort();
        }
        public static void DisposeCollector()
        {
            isStartCollector = false;
            if (th_curv != null && th_curv.IsAlive) th_curv.Abort();
            if (th_ruler != null && th_ruler.IsAlive) th_ruler.Abort();
            if (th_Manager != null && th_Manager.IsAlive) th_Manager.Abort();
            if (th_tick1 != null && th_tick1.IsAlive) th_tick1.Abort();
            if(th_tick2 != null && th_tick2.IsAlive) th_tick2.Abort();
        }
        public static string GetUVFileName()
        {
            return  UVDetector.UV.uvValue_DataTable.Rows[0]["Time"].ToString().Replace("/", "_").Replace(":", "_")
                + " - " +
            UVDetector.UV.uvValue_DataTable.Rows[UVDetector.UV.uvValue_DataTable.Rows.Count - 1]["Time"].ToString().Replace("/", "_").Replace(":", "_");
        }


        /****************************************************************/
        public static int GetButtleSelected()
        {
            return buttleSelectedNo;
        }
        public static void ClearButtleSelected()
        {
            buttleSelectedNo = 0;
        }

    }
}
