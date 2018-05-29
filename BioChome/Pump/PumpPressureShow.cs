using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Pump
{
    public partial class PumpPressureShow : UserControl
    {
        public PumpPressureShow()
        {
            InitializeComponent();
        }

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

            internal Color curvColor;
            internal bool isFreshEn;
        };
        public static picRuler CurvRuler;


        private static Thread th_Manager;
        private static Thread th_tick1;
        private static Thread th_tick2;
        private static Thread th_curv;
        private static Thread th_ruler;
        public static Queue<double> curvQueue;
        public static double[] pressureVal;
        public static int maxPixelCnt;
        public static int nowPixelCnt;

        private void PumpPressureShow_Load(object sender, EventArgs e)
        {
            //instance = this;

            CurvRuler.isFreshEn = true;
            CurvRuler.curvX_Max = CurvRuler.x_Max;
            CurvRuler.curvX_Min = CurvRuler.x_Min;
            CurvRuler.curvY_Max = CurvRuler.y_Max;
            CurvRuler.curvY_Min = CurvRuler.y_Min;


            //th_Manager = new Thread(curvManager);
            //th_Manager.Start();
            curvQueue = new Queue<double>();

            if (th_curv == null)
            {
                th_curv = new Thread(curvpaint);
                th_curv.Start();
            }
        }

        private void PumpPressureShow_Layout(object sender, LayoutEventArgs e)
        {
            //Graphics len = CreateGraphics();
            //SizeF size = len.MeasureString("10000", new Font("宋体", 9));
            //CurvArea.Left = Convert.ToInt32(size.Width) + 10;
            //CurvArea.Top = Convert.ToInt32(size.Height / 2);
            //CurvArea.Width = this.Width - CurvArea.Left - Convert.ToInt32(size.Width / 2);
            //CurvArea.Height = this.Height - CurvArea.Top - Convert.ToInt32(size.Height) - 5;
            CurvArea.Left = 0;
            CurvArea.Top = 3;
            CurvArea.Width = PressureCurvPic.Width - CurvArea.Left;
            CurvArea.Height = PressureCurvPic.Height - CurvArea.Top-3;
        }


        protected void curvpaint()
        {
            Bitmap curv_bmp;
            try
            {
                curv_bmp = new Bitmap(CurvArea.Width, CurvArea.Height);
            }
            catch
            {
                curv_bmp = new Bitmap(100, 100);
            }
            Graphics curv_pen = Graphics.FromImage(curv_bmp);

            Thread.Sleep(100);
            while (true)
            {
                Thread.Sleep(40);
                try
                {
                    Point startPoint = new Point();
                    Point endPoint = new Point();

                    CurvArea.Invoke(new EventHandler(delegate
                    {
                        if (curv_bmp.Width != CurvArea.Width || curv_bmp.Height != CurvArea.Height)
                        {
                            //curv_bmp.Dispose();
                            try
                            {
                                curv_bmp = new Bitmap(CurvArea.Width, CurvArea.Height);
                            }
                            catch
                            {
                                curv_bmp = new Bitmap(100, 100);
                            }
                            curv_pen = Graphics.FromImage(curv_bmp);
                        }
                        curv_pen.Clear(PressureCurvPic.BackColor);
                    }));

                    maxPixelCnt = CurvArea.Width;

                    for (int pixIndex = 0; pixIndex < nowPixelCnt; ++pixIndex)
                    {
                        if (pixIndex == 0)
                        {
                            startPoint.Y = Convert.ToInt32(CurvArea.Height-1 - (CurvArea.Height-2) * (pressureVal[pixIndex] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                        }
                        else
                        {
                            startPoint.X = pixIndex;
                            endPoint.X = startPoint.X;
                            endPoint.Y = Convert.ToInt32(CurvArea.Height-1 - (CurvArea.Height-2) * (pressureVal[pixIndex] - CurvRuler.curvY_Min) / (CurvRuler.curvY_Max - CurvRuler.curvY_Min));
                            //curv_pen.DrawLine(new Pen(CurvRuler.curvColor, 1),
                            //        startPoint.X - 1, startPoint.Y,
                            //        endPoint.X, endPoint.Y);
                            curv_pen.DrawLine(new Pen(Color.Blue, 1),
                                    startPoint.X - 1, startPoint.Y,
                                    endPoint.X, endPoint.Y);
                            startPoint.Y = endPoint.Y;
                        }
                    }

                    CurvArea.Invoke(new EventHandler(delegate
                    {
                        CurvArea.Image = curv_bmp;
                    }));
                }
                catch (Exception ex)
                {
                    //th_curv.Abort();
                    continue;
                }
            }
        }


        public void SetPressureVal(double val)
        {
            pressureVal = new double[maxPixelCnt];
            if (curvQueue.Count < maxPixelCnt) {
                curvQueue.Enqueue(val);
                nowPixelCnt = curvQueue.Count;
                int i = 0;
                foreach (double x in curvQueue) pressureVal[i++] = x;
                //for (i = 0; i < nowPixelCnt; i++)
                //    pressureVal[i] = 5;// 10 * Math.Sin(i / 3.1415926)+10;
            }
            else
            {
                curvQueue.Dequeue();
                curvQueue.Enqueue(val);
                nowPixelCnt = curvQueue.Count;
                int i = 0;
                foreach (double x in curvQueue) pressureVal[i++] = x;
                //for (i = 0; i < nowPixelCnt; i++)
                //    pressureVal[i] = 5;//10 * Math.Sin(i / 3.1415926)+10;
            }
        }
        public void ClearPressureVal()
        {
            curvQueue.Clear();
        }

        public void SetCurv_yMax(double yMax)
        {
            CurvRuler.curvY_Max = yMax;
        }

        public void PressureThreadDispose()
        {
            if (th_curv != null && th_curv.IsAlive)
                th_curv.Abort();
        }


    }
}
