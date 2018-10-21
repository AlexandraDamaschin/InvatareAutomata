using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMeans
{
    public partial class Form1 : Form
    {

        private System.IO.StreamReader streamReader;
        private List<DataPoint> points;
        Random random;
        Color[] colors = { Color.Blue, Color.Red, Color.Yellow, Color.Magenta, Color.Green };
        int noOfCenters;
        int[] cX;
        int[] cY;
        int min = -300;
        int max = 300;

        public Form1()
        {
            InitializeComponent();
            init();
            points = new List<DataPoint>();
            streamReader = new System.IO.StreamReader("xy.txt");
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            drawAxis();
            points = new List<DataPoint>();
            groupPoints();

        }

        private void init()
        {
            random = new Random(DateTime.Now.Millisecond);
            noOfCenters = random.Next(2, 10); //random zones [2,10]
            //random x and y for centroid
            cX = new int[noOfCenters];
            cY = new int[noOfCenters];

            for (int i = 0; i < noOfCenters; i++)
            {
                cX[i] = random.Next(min, max);
                cY[i] = random.Next(min, max);
                //set label text to see current x and y 
                label.Text = cX[i] + " " + cY[i];
                int newX = CalculateNewX(cX[i]);
                int newY = CalculateNewY(cY[i]);
                //draw center
                drawCenter(newX, newY, colors[i]);
            }
        }

        #region calculate new x and y
        public int CalculateNewX(int x)
        {
            int newX = 300 + x;
            return newX;
        }

        public int CalculateNewY(int y)
        {
            int newY = 300 - y;
            return newY;
        }
        #endregion

        public void drawPoint(int x, int y, Color color)
        {
            //FromHwnd= method is used to create a Graphics object from the specified handler of a window
            Graphics graphics = Graphics.FromHwnd(pictureBox.Handle);
            SolidBrush solidBrush = new SolidBrush(color);
            Point point = new Point(x, y);
            Rectangle rectangle = new Rectangle(point, new Size(1, 1));
            graphics.FillRectangle(solidBrush, rectangle);
            graphics.Dispose();
        }

        public void drawCenter(int x, int y, Color color)
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox.Handle);
            SolidBrush solidBrush = new SolidBrush(color);
            Point point = new Point(x - 3, y - 3);
            Pen pen = new Pen(color);
            Rectangle rectangle = new Rectangle(point, new Size(6, 6));
            graphics.DrawEllipse(pen, rectangle);
            graphics.Dispose();
        }

        public void drawAxis()
        {
            Graphics graphics = Graphics.FromHwnd(pictureBox.Handle);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            //y axis
            Point point = new Point(300, 0);
            Rectangle rectangleAxisY = new Rectangle(point, new Size(1, 600));
            graphics.FillRectangle(solidBrush, rectangleAxisY);

            //x axis
            point = new Point(0, 300);
            Rectangle rectangleAxisX = new Rectangle(point, new Size(600, 1));
            graphics.FillRectangle(solidBrush, rectangleAxisY);
            graphics.Dispose();
        }

        public void groupPoints()
        {
            string line;
            int minDistance, distance, parent = 0;
            Point point;

            for (int i = 0; i < noOfCenters; i++)
            {
                int newX = CalculateNewX(cX[i]);
                int newY = CalculateNewY(cY[i]);
                //draw center
                drawCenter(newX, newY, colors[i]);
            }

            while ((line = streamReader.ReadLine()) != null)
            {
                minDistance = 1000;
                point = getPoint(line);
                for (int i = 0; i < noOfCenters; i++)
                {
                    distance = (int)getDistance(point.X, point.Y, cX[i], cY[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        parent = i;
                    }
                }
                points.Add(new DataPoint(point.X, point.Y, parent));
                int newX = CalculateNewX(point.X);
                int newY = CalculateNewY(point.Y);
                //draw center
                drawCenter(newX, newY, colors[parent]);
            }
            for (int i = 0; i < noOfCenters; i++)
            {
                Point pointNewCenter = getCenter(i);
                if (pointNewCenter.X != 0)
                {
                    cX[i] = pointNewCenter.X;
                    cY[i] = pointNewCenter.Y;
                }
            }
        }

        private Point getPoint(string line)
        {
            int commaIndex = line.IndexOf(",");
            string x = line.Substring(0, commaIndex);
            string y = line.Substring(commaIndex + 1);

            Point point = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
            return point;
        }

        private double getDistance(int x1, int y1, int x2, int y2)
        {
            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distance;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            streamReader.Close();
        }
    }
}
