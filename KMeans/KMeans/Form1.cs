using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KMeans
{
    public partial class Form1 : Form
    {

        private System.IO.StreamReader streamReader;
        private List<DataPoint> points;
        Random random;
        //random colors
        Color[] colors = {
                Color.Blue, Color.Red, Color.Yellow, Color.Magenta, Color.Green,
                Color.Pink, Color.Plum,Color.Orange, Color.Maroon, Color.Turquoise};
        int noOfCenters;
        int[] cX, cY;
        int min = -300;
        int max = 300;

        public Form1()
        {
            InitializeComponent();
            init();
            points = new List<DataPoint>();
            streamReader = new System.IO.StreamReader("xy.txt");
            drawAxis();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            drawAxis();
            points = new List<DataPoint>();
            groupPoints();
        }

        private void init()
        {
            random = new Random();
            noOfCenters = random.Next(2, 10); //random zones [2,10]
            //random x and y for centroid
            cX = new int[noOfCenters];
            cY = new int[noOfCenters];

            for (int i = 0; i < noOfCenters; i++)
            {
                cX[i] = random.Next(min, max);
                cY[i] = random.Next(min, max);
                //set label text to see random x and y 
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

        #region draw
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
            SolidBrush brush = new SolidBrush(Color.Black);
            // y axis
            Point pointY = new Point(300, 0);
            Rectangle yAxis = new Rectangle(pointY, new Size(1, 600));
            graphics.FillRectangle(brush, yAxis);

            Point pointX = new Point(0, 300);
            Rectangle xAxis = new Rectangle(pointX, new Size(600, 1));
            graphics.FillRectangle(brush, xAxis);

            graphics.Dispose();
        }
        #endregion

        public void groupPoints()
        {
            string line;
            int minDistance, distance, parent = 0;
            Point point;

            //draw all random centers
            for (int i = 0; i < noOfCenters; i++)
            {
                int newX = CalculateNewX(cX[i]);
                int newY = CalculateNewY(cY[i]);
                drawCenter(newX, newY, colors[i]);
            }

            while ((line = streamReader.ReadLine()) != null)
            {
                minDistance = 1000;
                point = getPoint(line);
                //for each point calculate distance
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

        #region get
        private Point getCenter(int center)
        {
            Point pointCenter = new Point();
            int sumX = 0, sumY = 0, nr = 0;

            //calculate center for each point 
            foreach (DataPoint point in points)
            {
                if (point.Center == center)
                {
                    nr++;
                    sumX += point.X;
                    sumY += point.Y;
                }
            }

            if (nr != 0)
            {
                pointCenter.X = sumX / nr;
                pointCenter.Y = sumY / nr;
            }
            return pointCenter;
        }

        //get point from xy file
        private Point getPoint(string line)
        {
            string x, y;
            Point point = new Point();
            int separator = line.IndexOf(" ");
            if (separator > 0)
            {
                x = line.Substring(0, separator);
                y = line.Substring(separator + 1);
                point = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
            }
            return point;
        }

        private double getDistance(int x1, int y1, int x2, int y2)
        {
            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distance;
        }
        #endregion

        private void button_try_again_Click(object sender, EventArgs e)
        {
            init();
            points = new List<DataPoint>();
            streamReader = new System.IO.StreamReader("xy.txt");
            drawAxis();
            points = new List<DataPoint>();
            groupPoints();
        }

        private void button_clear_picture_box_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            streamReader.Close();
        }

    }
}
