using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BackPropagationPoints
{
    public partial class Form1 : Form
    {
        private const int maxX = 600;
        private const int maxY = 600;

        Graphics graphics;
        bool done = false;
        List<Point> points = new List<Point>();
        int numberOfOutputs = 0;
        NeuralNetwork neuralNetwork;

        Color[] colorAreas = new Color[]
        {
            Color.FromArgb(0, 204, 255),
            Color.FromArgb(204, 255, 153),
            Color.FromArgb(255, 204, 153),
            Color.FromArgb(223, 255, 128)
        };

        Color[] colorPoints = new Color[]
        {
             Color.FromArgb(0, 0, 179),
            Color.FromArgb(0, 204, 102),
            Color.FromArgb(204, 122, 0),
            Color.FromArgb(134, 179, 0)

        };

        Color[] colors = new Color[]
       {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Firebrick,
            Color.Orange,
            Color.DarkViolet,
            Color.Cyan,
            Color.Turquoise,
            Color.Yellow,
            Color.Magenta
       };

        public Form1()
        {
            InitializeComponent();

            graphics = panel1.CreateGraphics();
        }

        #region Draws
        private void DrawAxis()
        {
            Pen pen = new Pen(Color.Black, 1.5f);

            graphics.DrawLine(pen, panel1.Width / 2, 0, panel1.Width / 2, panel1.Height);
            graphics.DrawLine(pen, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);
        }

        private void DrawPoint(double x, double y, Color c, float radius = 3.6f)
        {
            double screenX = x + maxX / 2 - radius / 2;
            double screenY = maxY / 2 - y - radius / 2;

            graphics.FillEllipse(new SolidBrush(c), (float)screenX, (float)screenY, radius, radius);
        }

        #endregion

        #region buttons 
        private void button_load_Click(object sender, System.EventArgs e)
        {

        }

        private void button_train_Click(object sender, System.EventArgs e)
        {

        }

        private void button_start_Click(object sender, System.EventArgs e)
        {

        }
        #endregion
    }
}
