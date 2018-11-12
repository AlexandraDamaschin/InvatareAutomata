using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace KohonenSOM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Point> points = new List<Point>();
        public struct Neuroni
        {
            public double x;
            public double y;
        };
        public Neuroni[,] neuronis = new Neuroni[10, 10];
        int min = -300, max = 300;
        int matriceCoord = 10;

        #region Points
        private void DrawPoint(int x, int y)
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox1.Handle);
            Pen pen = new Pen(Color.Black, 2);
            graphics.DrawLine(pen, 300, 0, 300, 600);
            graphics.DrawLine(pen, 0, 300, 600, 300);

            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Point point = new Point(x, y);
            Size size = new Size(1, 1);
            Rectangle rectangle = new Rectangle(point, size);
            graphics.FillRectangle(solidBrush, rectangle);
            graphics.Dispose();
        }

        private void DrawPoints()
        {
            Random random = new Random();
            int constant = 0;
            double step, gauss;
            int x, y, i;
            int[] mx = { -100, 100, -50 };
            int[] my = { 100, 0, -100 };
            int[] tx = { 20, 10, 10 };
            int[] ty = { 10, 5, 30 };

            while (constant < 3000) //draw only 3 points
            {
                i = random.Next(0, 3);
                do
                {
                    x = random.Next(min, max);
                    gauss = Math.Exp((-((mx[i] - x) * (mx[i] - x) * 1.0) / (2.0 * tx[i] * tx[i])));
                    step = random.Next(0, 100000);
                    step = step / 100000;
                } while (gauss < step);
                do
                {
                    y = random.Next(min, max);
                    gauss = Math.Exp((-((my[i] - y) * (my[i] - y) * 1.0) / (2.0 * ty[i] * ty[i])));
                    step = random.Next(0, 100000);
                    step = step / 100000;
                } while (gauss < step);
                points.Add(new Point(x, y));
                constant++;
            }
        }

        private void InitializeNeurons()
        {
            for (int i = 0; i < matriceCoord; i++)
            {
                for (int j = 0; j < matriceCoord; j++)
                {
                    neuronis[i, j].x = -280 + i * 40; //why???
                    neuronis[i, j].y = 280 - j * 40; //why???
                }
            }
        }

        private void DrawMap()
        {
            pictureBox1.Invalidate();
            pictureBox1.Refresh();

            Graphics graphics = Graphics.FromHwnd(pictureBox1.Handle);
            Pen pen = new Pen(Color.Red, 1);

            //draw points with new coordonates
            for (int i = 0; i < points.Count; i++)
            {
                int newX = CalculateNewX(points[i].X);
                int newY = CalculateNewY(points[i].Y);
                DrawPoint(newX, newY);
            }

            for (int i = 0; i < matriceCoord; i++)
            {
                for (int j = 0; j < matriceCoord; j++)
                {
                    if (i < 9 && j < 9)
                    {
                        graphics.DrawLine(pen,
                            (float)neuronis[i, j].x + 300,
                            300 - (float)neuronis[i, j].y,
                            (float)neuronis[i, j + 1].x + 300,
                            300 - (float)neuronis[i, j + 1].y);

                        graphics.DrawLine(pen,
                            (float)neuronis[i, j].x + 300,
                            300 - (float)neuronis[i, j].y,
                            (float)neuronis[i + 1, j].x + 300,
                            300 - (float)neuronis[i + 1, j].y);
                    }
                    else if (i == 9 && j < 9)
                    {
                        graphics.DrawLine(pen,
                              (float)neuronis[i, j].x + 300,
                              300 - (float)neuronis[i, j].y,
                              (float)neuronis[i, j + 1].x + 300,
                              300 - (float)neuronis[i, j + 1].y);
                    }
                    else if (i < 9 && j == 9)
                    {
                        graphics.DrawLine(pen,
                            (float)neuronis[i, j].x + 300,
                            300 - (float)neuronis[i, j].y,
                            (float)neuronis[i + 1, j].x + 300,
                            300 - (float)neuronis[i + 1, j].y);
                    }
                } 
            }
            graphics.Dispose();
        }

        private void button_Points_Click(object sender, EventArgs e)
        {
            DrawPoints();
            InitializeNeurons();
            DrawMap();
        }
        #endregion

        #region Calculate new points
        private int CalculateNewX(int x)
        {
            int newX = x + 300;
            return newX;
        }

        private int CalculateNewY(int y)
        {
            int newY = 300 - y;
            return newY;
        }
        #endregion


        #region Kohonen
        private void button_Kohonen_Click(object sender, EventArgs e)
        {
            Kohonen();
        }

        private void Kohonen()
        {
            int N = 10;
            int t = 1;
            double distance, V;
            double alfa = 0.7 * Math.Exp((-1.0 * t) / N);
            int winnerI = 0, winnerJ = 0;
            int VIup, VIdown, VIright, VIleft;

            InitializeNeurons();

            while (alfa > 0.001)
            {
                alfa = 0.7 * Math.Exp((-1.0 * t) / N);
                V = 7 * Math.Exp((-1.0 * t) / N);
                t++;
                DrawMap();
                Thread.Sleep(500);
                for (int pct = 0; pct < points.Count; pct++)
                {
                    double minimum = 99999;
                    for (int i = 0; i < matriceCoord; i++)
                    {
                        for (int j = 0; j < matriceCoord; j++)
                        {
                            distance = Math.Sqrt(((points[pct].X - neuronis[i, j].x) * (points[pct].X - neuronis[i, j].x)) +
                                ((points[pct].Y - neuronis[i, j].y) * (points[pct].Y - neuronis[i, j].y)));
                            if (distance < min)
                            {
                                minimum = distance;
                                winnerI = i;
                                winnerJ = j;
                            }
                        }
                    }

                    VIup = winnerI - (int)V;
                    VIdown = winnerI + (int)V;
                    VIleft = winnerJ - (int)V;
                    VIright = winnerJ + (int)V;

                    if (VIup < 0)
                    {
                        VIup = 0;
                    }
                    if (VIdown > 9)
                    {
                        VIdown = 9;
                    }
                    if (VIleft < 0)
                    {
                        VIleft = 0;
                    }
                    if (VIright > 9)
                    {
                        VIright = 9;
                    }

                    for (int i = VIup; i <= VIdown; i++)
                    {
                        for (int j = VIleft; j <= VIright; j++)
                        {
                            neuronis[i, j].x = neuronis[i, j].x + (alfa * (points[pct].X - neuronis[i, j].x));
                            neuronis[i, j].y = neuronis[i, j].y + (alfa * (points[pct].Y - neuronis[i, j].y));
                        }
                    }
                }
            }
            label1.Text = "End";
        }
        #endregion
    }
}
