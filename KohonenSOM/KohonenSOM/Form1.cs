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
            throw new NotImplementedException();
        }

        private void DrawMap()
        {
            throw new NotImplementedException();
        }

        private void InitializeNeuroni()
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
