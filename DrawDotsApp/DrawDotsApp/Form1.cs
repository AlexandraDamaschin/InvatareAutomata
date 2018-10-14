using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawDotsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            for (int x = 0; x <= 600; x = x + 5)
            {
                oX_oY.DrawLine(Pens.Black, x, 299, x, 302);
            }

            //Grade oY by 5
            for (int y = 0; y <= 600; y = y + 5)
            {
                oX_oY.DrawLine(Pens.Black, 299, y, 302, y);
            }

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
            float newX = CalculateNewX(10);
            float newY = CalculateNewY(-120);
            List<float> z1 = new List<float>();
            z1.Add(newX);
            z1.Add(newY);
            z1.Add(10); //deltaX
            z1.Add(10); //deltaY
            return z1;
        }
        public List<float> ValueZ2()
        {
            float newX = CalculateNewX(260);
            float newY = CalculateNewY(-20);
            List<float> z2 = new List<float>();
            z2.Add(newX);
            z2.Add(newY);
            z2.Add(5); //deltaX
            z2.Add(10); //deltaY
            return z2;
        }
        public List<float> ValueZ3()
        {
            float newX = CalculateNewX(-100);
            float newY = CalculateNewY(-150);
            List<float> z3 = new List<float>();
            z3.Add(newX);
            z3.Add(newY);
            z3.Add(25); //deltaX
            z3.Add(20); //deltaY
            return z3;
        }
        public List<float> ValueZ4()
        {
            float newX = CalculateNewX(-120);
            float newY = CalculateNewY(120);
            List<float> z4 = new List<float>();
            z4.Add(newX);
            z4.Add(newY);
            z4.Add(15); //deltaX
            z4.Add(10); //deltaY
            return z4;
        }
        public List<float> ValueZ5()
        {
            float newX = CalculateNewX(100);
            float newY = CalculateNewY(130);
            List<float> z5 = new List<float>();
            z5.Add(newX);
            z5.Add(newY);
            z5.Add(5); //deltaX
            z5.Add(10); //deltaY
            return z5;
        }
        #endregion

        //create zones with custom colors
        public void CreateZone(int x, int y, int z, List<float> zone)
        {
            float mx = zone[0];
            float my = zone[1];
            float delta_x = zone[2];
            float delta_y = zone[3];

            Graphics graphics;
            graphics = this.CreateGraphics();

            Color customColor = Color.FromArgb(x, y, z);
            Color transparencyColor = Color.FromArgb(90, customColor);
            SolidBrush transparencyColorBrush = new SolidBrush(transparencyColor);
            graphics.FillEllipse(transparencyColorBrush, mx - delta_x / 2, my - delta_y / 2, delta_x, delta_y);
            graphics.Dispose();
        }

        #region custom colors for zones
        public void Zona_1()
        {
            CreateZone(191, 18, 246, ValueZ1()); //PURPLE
        }

        public void Zona_2()
        {
            CreateZone(140, 190, 95, ValueZ2()); //GREEN
        }

        public void Zona_3()
        {
            CreateZone(199, 78, 144, ValueZ3());  //PINK
        }

        public void Zona_4()
        {
            CreateZone(6, 67, 178, ValueZ4());  // DARK BLUE
        }

        public void Zona_5()
        {
            CreateZone(212, 147, 106, ValueZ5());   // BEIGE
        }
        #endregion

        //draw random points
        public void DrawPoints(List<float> zone, Pen pen)
        {
            float mx = zone[0];
            float my = zone[1];
            float delta_x = zone[2];
            float delta_y = zone[3];

            Random random = new Random();

            //draw 1000 random points
            for (int k = 0; k < 1000; k++)
            {
                float x, G_X;
                float pa;
                float y, G_Y;
                float x_Y_dim = 1;
                do
                {
                    x = random.Next(-300, 300);
                    x = CalculateNewX(x);
                    G_X = (float)Math.Pow(Math.E, (-((mx - x) * (mx - x) / (2 * delta_x * delta_x))));
                    pa = (float)random.NextDouble();
                }
                while (G_X < pa);
                do
                {
                    y = random.Next(-300, 300);
                    y = CalculateNewY(y);
                    G_Y = (float)Math.Pow(Math.E, (-((my - y) * (my - y) / (2 * delta_y * delta_y))));
                    pa = (float)random.NextDouble();
                }
                while (G_Y < pa);
                Graphics graph;
                graph = this.CreateGraphics();
                graph.DrawEllipse(pen, x, y, x_Y_dim, x_Y_dim);
                graph.Dispose();
            }
        }

        //todo: here might be the error
        //drawing just the first dot
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            CreateLinesCoordonates();

            //chose random zones out of 5
            List<int> randomZone = new List<int>();
            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                int numberOfZone;
                do numberOfZone = random.Next(1, 6);
                while (randomZone.Contains(numberOfZone));

                switch (numberOfZone)
                {
                    case 1:
                        Zona_1();
                        Color colorZ1 = Color.FromArgb(54, 51, 119);
                        Color.FromArgb(54, 51, 119);
                        SolidBrush brushZ1 = new SolidBrush(colorZ1);
                        Pen penColorZ1 = new Pen(brushZ1);
                        DrawPoints(ValueZ1(), penColorZ1);
                        break;
                    case 2:
                        Color colorZ2 = Color.FromArgb(140, 190, 95);
                        SolidBrush brushZ2 = new SolidBrush(colorZ2);
                        Pen penColorZ2 = new Pen(brushZ2);
                        DrawPoints(ValueZ2(), penColorZ2);
                        break;
                    case 3:
                        Zona_3();
                        Color colorZ3 = Color.FromArgb(199, 78, 144);
                        SolidBrush brushZ3 = new SolidBrush(colorZ3);
                        Pen penColorZ3 = new Pen(brushZ3);
                        DrawPoints(ValueZ3(), penColorZ3);
                        break;
                    case 4:
                        Zona_4();
                        Color colorZ4 = Color.FromArgb(6, 67, 178);
                        SolidBrush brushZ4 = new SolidBrush(colorZ4);
                        Pen penColorZ4 = new Pen(brushZ4);
                        DrawPoints(ValueZ4(), penColorZ4);
                        break;
                    case 5:
                        Zona_5();
                        Color colorZ5 = Color.FromArgb(212, 147, 106);
                        SolidBrush brushZ5 = new SolidBrush(colorZ5);
                        Pen penColorZ5 = new Pen(brushZ5);
                        DrawPoints(ValueZ5(), penColorZ5);
                        break;
                }
                randomZone.Add(numberOfZone);
            }
        }
    }
}
