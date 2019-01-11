using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawAxis();
        }

        #region buttons 
        private void button_load_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string line;
                StreamReader reader = new StreamReader(openFile.FileName, true);

                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(new char[] { ',', ' ' });

                    Point point = new Point();
                    point.X = Convert.ToDouble(data[0]);
                    point.Y = Convert.ToDouble(data[1]);
                    point.Area = Convert.ToInt32(data[2]);
                    point.Color = colorPoints[point.Area - 1];

                    DrawPoint(point.X, point.Y, point.Color);
                    points.Add(point);

                    if (numberOfOutputs < point.Area)
                    {
                        numberOfOutputs = point.Area;
                    }
                }
                reader.Close();
                neuralNetwork = new NeuralNetwork(2, 5, numberOfOutputs);
            }
            Console.WriteLine(numberOfOutputs);
            done = false;
        }

        private void button_train_Click(object sender, System.EventArgs e)
        {
            Thread thread = new Thread(Run);
            thread.Start();
        }

        private void button_start_Click(object sender, System.EventArgs e)
        {

        }
        #endregion

        private void Run()
        {
            double prag = Math.Pow(10, -2);
            double E = 100;
            int epoca = 0;

            while (E > prag && epoca < 1000)
            {
                E = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    Point point = points[i];
                    double[] p = new double[] { point.X / (maxX / 2), point.Y / (maxY / 2) };
                    neuralNetwork.Forward(p);

                    double target = (double)point.Area / (numberOfOutputs + 1);

                    neuralNetwork.Backward(target);

                    for (int j = 0; j < neuralNetwork.Layers[neuralNetwork.Layers.Count - 1].Neurons.Count; j++)
                    {
                        E += Math.Pow(neuralNetwork.Layers[neuralNetwork.Layers.Count - 1].Neurons[j].Output - target, 2);
                    }
                }
                Console.WriteLine(E);
                epoca++;
            }
            MessageBox.Show("DONE!\n Epoci =" + epoca);
        }
    }
}
