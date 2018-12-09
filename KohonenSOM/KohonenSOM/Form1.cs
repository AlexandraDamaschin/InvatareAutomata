using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static int matrixSize = 10;
        public Neuroni[,] neuronis = new Neuroni[matrixSize, matrixSize];
        int min = -300, max = 300;
        int matriceCoord = 10;

        #region Points
        private void DrawPoint(float x, float y)
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox1.Handle);
            Pen pen = new Pen(Color.Black, 2);
            graphics.DrawLine(pen, 300, 0, 300, 600);
            graphics.DrawLine(pen, 0, 300, 600, 300);

            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Point point = new Point((int)x, (int)y);
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

        //inialize neurons and draw map
        private void InitializeNeurons()
        {
            for (int i = 0; i < matriceCoord; i++)
            {
                for (int j = 0; j < matriceCoord; j++)
                {
                    neuronis[i, j].x = -300 + i * 66;
                    neuronis[i, j].y = 300 - j * 66;
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
                float newX = CalculateNewX(points[i].X);
                float newY = CalculateNewY(points[i].Y);
                DrawPoint(newX, newY);
            }

            for (int i = 0; i < matriceCoord; i++)
            {
                for (int j = 0; j < matriceCoord; j++)
                {
                    int matrixSizeDecremented = matrixSize - 1;
                    if (i < matrixSizeDecremented && j < matrixSizeDecremented)
                    {
                        //i,j
                        float newXij = CalculateNewX((float)neuronis[i, j].x);
                        float newYij = CalculateNewY((float)neuronis[i, j].y);

                        //i,j+1
                        float newXij1 = CalculateNewX((float)neuronis[i, j + 1].x);
                        float newYij1 = CalculateNewY((float)neuronis[i, j + 1].y);

                        graphics.DrawLine(pen, newXij, newYij, newXij1, newYij1);

                        //i+1,j
                        float newXi1j = CalculateNewX((float)neuronis[i + 1, j].x);
                        float newYi1j = CalculateNewY((float)neuronis[i + 1, j].y);

                        graphics.DrawLine(pen, newXij, newYij, newXi1j, newYi1j);
                    }
                    else if (i == matrixSizeDecremented && j < matrixSizeDecremented)
                    {
                        //i,j
                        float newXij = CalculateNewX((float)neuronis[i, j].x);
                        float newYij = CalculateNewY((float)neuronis[i, j].y);

                        //i,j+1
                        float newXij1 = CalculateNewX((float)neuronis[i, j + 1].x);
                        float newYij1 = CalculateNewY((float)neuronis[i, j + 1].y);

                        graphics.DrawLine(pen, newXij, newYij, newXij1, newYij1);
                    }
                    else if (i < matrixSizeDecremented && j == matrixSizeDecremented)
                    {
                        //i,j
                        float newXij = CalculateNewX((float)neuronis[i, j].x);
                        float newYij = CalculateNewY((float)neuronis[i, j].y);

                        //i+1,j
                        float newXi1j = CalculateNewX((float)neuronis[i + 1, j].x);
                        float newYi1j = CalculateNewY((float)neuronis[i + 1, j].y);

                        graphics.DrawLine(pen, newXij, newYij, newXi1j, newYi1j);
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
        private float CalculateNewX(float x)
        {
            float newX = x + 300;
            return newX;
        }

        private float CalculateNewY(float y)
        {
            float newY = 300 - y;
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
            int N = 10; //number of steps in which we want the alg to learn
            int t = 0; //current step
            double distance, V;
            double alfa = 0.7 * Math.Exp((-1.0 * t) / N);
            int winnerI = 0, winnerJ = 0;
            int VIup, VIdown, VIright, VIleft;

            InitializeNeurons();

            while (alfa > 0.001)
            {
                V = 7 * Math.Exp((-1.0 * t) / N);
                t++;
                alfa = 0.7 * Math.Exp((-1.0 * t) / N);
                Console.WriteLine("Alfa & V:");
                Console.WriteLine("{0}, {1}", alfa, V);
                DrawMap();
                //Thread.Sleep(500);
                for (int pct = 0; pct < points.Count; pct++)
                {
                    double minimum = 99999;
                    for (int i = 0; i < matriceCoord; i++)
                    {
                        for (int j = 0; j < matriceCoord; j++)
                        {
                            distance = Math.Sqrt(((points[pct].X - neuronis[i, j].x) * (points[pct].X - neuronis[i, j].x)) +
                                ((points[pct].Y - neuronis[i, j].y) * (points[pct].Y - neuronis[i, j].y)));

                            if (distance < minimum)
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
                    if (VIdown > matrixSize)
                    {
                        VIdown = matrixSize;
                    }
                    if (VIleft < 0)
                    {
                        VIleft = 0;
                    }
                    if (VIright > matrixSize)
                    {
                        VIright = matrixSize;
                    }

                    int count = 0;
                    for (int i = VIup; i < VIdown; i++)
                    {
                        for (int j = VIleft; j < VIright; j++)
                        {
                            neuronis[i, j].x = neuronis[i, j].x + ((float)alfa * (points[pct].X - neuronis[i, j].x));
                            neuronis[i, j].y = neuronis[i, j].y + ((float)alfa * (points[pct].Y - neuronis[i, j].y));
                            count++;
                        }
                    }
                    label1.Text = "Epoch no: " + count;
                    //  Thread.Sleep(200);
                }
                //DrawMap();
            }
            label1.Text = "End";
        }
        #endregion
    }
}
