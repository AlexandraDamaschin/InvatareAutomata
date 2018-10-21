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
            streamReader = new System.IO.StreamReader("newCoordonates.txt");
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
            int newX = x + 300;
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
            Graphics oX_oY;
            oX_oY = this.CreateGraphics();

            //create oX from 0 to 600
            oX_oY.DrawLine(Pens.Black, 0, 300, 600, 300);

            //create oY from 0 to 600
            oX_oY.DrawLine(Pens.Black, 300, 0, 300, 600);
        }
        #endregion

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

        #region get
        private Point getCenter(int center)
        {
            Point pointCenter = new Point();
            int sumX = 0, sumY = 0, nr = 0;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            streamReader.Close();
        }
    }
}
