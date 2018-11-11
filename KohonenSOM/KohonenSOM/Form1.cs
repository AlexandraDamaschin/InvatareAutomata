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
        public Neuroni[,] neuronis = new Neuroni[10, 10];
        int min = -300, max = 300;

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
                    y = random.Next(-200, 200);
                    gauss = Math.Exp((-((my[i] - y) * (my[i] - y) * 1.0) / (2.0 * ty[i] * ty[i])));
                    step = random.Next(0, 100000);
                    step = step / 100000;
                } while (gauss < step);
                points.Add(new Point(x, y));
                constant++;
            }
        }

        private void InitializeNeuroni()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    neuronis[i, j].x = -180 + i * 40; //why???
                    neuronis[i, j].y = 180 - j * 40; //why???
                }
            }
        }

        private void DrawMap()
        {
            throw new NotImplementedException();
        }

        private void button_Points_Click(object sender, EventArgs e)
        {
            DrawPoints();
            InitializeNeuroni();
            DrawMap();
        }

        #endregion

        #region Kohonen
        private void button_Kohonen_Click(object sender, EventArgs e)
        {
            Kohonen();
        }

        private void Kohonen()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
