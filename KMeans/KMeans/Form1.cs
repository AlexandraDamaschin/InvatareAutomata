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

    }
}
