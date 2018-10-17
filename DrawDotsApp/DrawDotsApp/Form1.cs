using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DrawDotsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //draw points in the form
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            CreateLinesCoordonates();

            //chose random zones
            List<int> randomZone = new List<int>();
            Random random = new Random();

            for (int i = 1; i <= 5; i++)
            {
                int numberOfZone;
                do numberOfZone = random.Next(1, 6); //chose a random number between 1 and 6
                while (randomZone.Contains(numberOfZone));

                switch (numberOfZone)
                {
                    case 1:
                        int x1Argb = 255, y1Argb = 0, z1Argb = 0; //red
                        Color colorZ1 = Color.FromArgb(x1Argb, y1Argb, z1Argb);
                        SolidBrush brushZ1 = new SolidBrush(colorZ1);
                        Pen penColorZ1 = new Pen(brushZ1);
                        DrawPoints(ValueZ1(), penColorZ1);
                        break;
                    case 2:
                        int x2Argb = 51, y2Argb = 255, z2Argb = 255; //blue
                        Color colorZ2 = Color.FromArgb(x2Argb, y2Argb, z2Argb);
                        SolidBrush brushZ2 = new SolidBrush(colorZ2);
                        Pen penColorZ2 = new Pen(brushZ2);
                        DrawPoints(ValueZ2(), penColorZ2);
                        break;
                    case 3:
                        int x3Argb = 196, y3Argb = 255, z3Argb = 15; //green
                        Color colorZ3 = Color.FromArgb(x3Argb, y3Argb, z3Argb);
                        SolidBrush brushZ3 = new SolidBrush(colorZ3);
                        Pen penColorZ3 = new Pen(brushZ3);
                        DrawPoints(ValueZ3(), penColorZ3);
                        break;
                    case 4:
                        int x4Argb = 150, y4Argb = 0, z4Argb = 232; //purple
                        Color colorZ4 = Color.FromArgb(x4Argb, y4Argb, z4Argb);
                        SolidBrush brushZ4 = new SolidBrush(colorZ4);
                        Pen penColorZ4 = new Pen(brushZ4);
                        DrawPoints(ValueZ4(), penColorZ4);
                        break;
                    case 5:
                        int x5Argb = 255, y5Argb = 255, z5Argb = 0; //yellow
                        Color colorZ5 = Color.FromArgb(x5Argb, y5Argb, z5Argb);
                        SolidBrush brushZ5 = new SolidBrush(colorZ5);
                        Pen penColorZ5 = new Pen(brushZ5);
                        DrawPoints(ValueZ5(), penColorZ5);
                        break;
                }
                randomZone.Add(numberOfZone);
            }
        }

        //create lines coordonate
        public void CreateLinesCoordonates()
        {
            Graphics oX_oY;
            oX_oY = this.CreateGraphics();

            //create oX from 0 to 600
            oX_oY.DrawLine(Pens.Black, 0, 300, 600, 300);

            //create oY from 0 to 600
            oX_oY.DrawLine(Pens.Black, 300, 0, 300, 600);

            //Grade oX by 5
            //for (int x = 0; x <= 600; x = x + 5)
            //{
            //    oX_oY.DrawLine(Pens.Black, x, 299, x, 302);
            //}

            ////Grade oY by 5
            //for (int y = 0; y <= 600; y = y + 5)
            //{
            //    oX_oY.DrawLine(Pens.Black, 299, y, 302, y);
            //}

            oX_oY.Dispose();
        }

        #region calculate new coordonates
        //calculate new x
        public float CalculateNewX(float x)
        {
            float newX = 300 + x;
            return newX;
        }

        //calculate new y
        public float CalculateNewY(float y)
        {
            float newY = 300 - y;
            return newY;
        }
        #endregion

        #region  set zones
        public List<float> ValueZ1()
        {
            float newX = CalculateNewX(27);
            float newY = CalculateNewY(-120);
            List<float> z1 = new List<float>();
            z1.Add(newX);
            z1.Add(newY);
            z1.Add(12); //deltaX
            z1.Add(10); //deltaY
            return z1;
        }
        public List<float> ValueZ2()
        {
            float newX = CalculateNewX(232);
            float newY = CalculateNewY(-20);
            List<float> z2 = new List<float>();
            z2.Add(newX);
            z2.Add(newY);
            z2.Add(14); //deltaX
            z2.Add(10); //deltaY
            return z2;
        }
        public List<float> ValueZ3()
        {
            float newX = CalculateNewX(-98);
            float newY = CalculateNewY(-120);
            List<float> z3 = new List<float>();
            z3.Add(newX);
            z3.Add(newY);
            z3.Add(25); //deltaX
            z3.Add(20); //deltaY
            return z3;
        }
        public List<float> ValueZ4()
        {
            float newX = CalculateNewX(-127);
            float newY = CalculateNewY(134);
            List<float> z4 = new List<float>();
            z4.Add(newX);
            z4.Add(newY);
            z4.Add(15); //deltaX
            z4.Add(13); //deltaY
            return z4;
        }
        public List<float> ValueZ5()
        {
            float newX = CalculateNewX(89);
            float newY = CalculateNewY(127);
            List<float> z5 = new List<float>();
            z5.Add(newX);
            z5.Add(newY);
            z5.Add(25); //deltaX
            z5.Add(20); //deltaY
            return z5;
        }
        #endregion

        //draw random points
        public void DrawPoints(List<float> zone, Pen pointsPen)
        {
            float mX = zone[0];
            float mY = zone[1];
            float delta_x = zone[2];
            float delta_y = zone[3];

            Random random = new Random();

            StreamWriter writetext = new StreamWriter("xyCoordonates.txt");

            //draw 1000 random points
            for (int k = 0; k < 1000; k++)
            {
                float x, Gx;
                float pas;
                float y, Gy;
                int max = 300, min = -300;

                do
                {
                    //draw points on x axis
                    x = random.Next(min, max);
                    x = CalculateNewX(x);
                    Gx = (float)Math.Pow(Math.E, (-((mX - x) * (mX - x) / (2 * delta_x * delta_x)))); //formula
                    pas = (float)random.NextDouble();
                }
                while (Gx < pas);
                do
                {
                    //draw points on y axis
                    y = random.Next(min, max);
                    y = CalculateNewY(y);

                    Gy = (float)Math.Pow(Math.E, (-((mY - y) * (mY - y) / (2 * delta_y * delta_y)))); //formula
                    pas = (float)random.NextDouble();
                }
                while (Gy < pas);
                writetext.WriteLine("Coordonates: " + "x="+x.ToString() + " y=" + y.ToString() + "\n");

                Graphics graph;
                graph = this.CreateGraphics();
                graph.DrawEllipse(pointsPen, x, y, 1, 1);
                graph.Dispose();
            }
            //close wroten file 
            writetext.Close();
        }
    }
}
