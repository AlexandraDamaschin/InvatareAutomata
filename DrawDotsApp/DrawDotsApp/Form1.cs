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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            CreateLinesCoordonates();

            //chose 3 zones random out of 5

            //calculate dispersion

            //draw points
            Graphics graphics = this.CreateGraphics();
            graphics.DrawEllipse(z1.getCuloare(), 0, 0, 5, 5);

            for (int i = 0; i < 1000; i++)
            {
                //draw 1000 different points 
                // graphics.DrawEllipse(z1.getCuloare(), z1x, z1y, 5, 5);
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

        //set zones
        public Zone ValueZ1()
        {
            float newX = CalculateNewX(10);
            float newY = CalculateNewY(-120);
            Zone z1 = new Zone(newX, newY, 10, 10, Pens.Blue);
            return z1;
        }
        public Zone ValueZ2()
        {
            float newX = CalculateNewX(260);
            float newY = CalculateNewY(-20);
            Zone z2 = new Zone(newX, newY, 5, 10, Pens.Blue);
            return z2;
        }
        public Zone ValueZ3()
        {
            float newX = CalculateNewX(-160);
            float newY = CalculateNewY(-210);
            Zone z3 = new Zone(newX, newY, 55, 25, Pens.Blue);
            return z3;
        }
        public Zone ValueZ4()
        {
            float newX = CalculateNewX(-120);
            float newY = CalculateNewY(120);
            Zone z4 = new Zone(newX, newY, 30, 35, Pens.Blue);
            return z4;
        }
        public Zone ValueZ5()
        {
            float newX = CalculateNewX(160);
            float newY = CalculateNewY(150);
            Zone z5 = new Zone(newX, newY, 10, 15, Pens.Blue);
            return z5;
        }

        //create zones with custom colors
        public void CreateZone(int x, int y, int z, Zone zone)
        {
            float mx = zone.getX();
            float my = zone.getY();
            int delta_x = zone.getDeltaX();
            int delta_y = zone.getDeltaY();

            Graphics graphics;
            graphics = this.CreateGraphics();

            Color customColor = Color.FromArgb(x, y, z);
            Color transparencyColor = Color.FromArgb(90, customColor);
            SolidBrush transparencyColorBrush = new SolidBrush(transparencyColor);
            graphics.FillEllipse(transparencyColorBrush, mx - delta_x / 2, my - delta_y / 2, delta_x, delta_y);
            graphics.Dispose();
        }

        public void Zona_1()
        {
            CreateZone(54, 51, 119, ValueZ1());
        }

        public void Zona_2()
        {
            CreateZone(140, 190, 95, ValueZ2());
        }

        public void Zona_3()
        {
            CreateZone(199, 78, 144, ValueZ3());
        }

        public void Zona_4()
        {
            CreateZone(6, 67, 178, ValueZ4());
        }

        public void Zona_5()
        {
            CreateZone(212, 147, 106, ValueZ5());
        }

        //draw random points
        public void DrawPoints(Zone zone, Pen pen)
        {
            float mx = zone.getX();
            float my = zone.getY();
            int delta_x = zone.getDeltaX();
            int delta_y = zone.getDeltaY();

            Random random = new Random();

            //draw 1000 random points
            for (int k = 0; k < 1000; k++)
            {
                float x, Gx, pa, y, Gy;
                float xy_dim = 1;
                do
                {
                    x = random.Next(-300, 300);
                    x = CalculateNewX(x);
                    Gx = (float)Math.Pow(Math.E, (-((mx - x) * (mx - x) / (2 * delta_x * delta_x))));

                    pa = (float)random.NextDouble();
                }
                while (Gx < pa);
                do
                {
                    y = random.Next(-300, 300);
                    y = CalculateNewY(y);
                    Gy = (float)Math.Pow(Math.E, (-((my - y) * (my - y) / (2 * delta_y * delta_y))));

                    pa = (float)random.NextDouble();
                }
                while (Gy < pa);
                Graphics graphics;
                graphics = this.CreateGraphics();
                graphics.DrawEllipse(pen, x, y, xy_dim, xy_dim);
                graphics.Dispose();

            }
        }
    }
}
