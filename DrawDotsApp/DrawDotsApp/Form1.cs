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


        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            CreateLinesCoordonates();
            //set default x and y 
            int x = 0;
            int y = 0;

            //set zones 
            Zone z1 = new Zone(120, 30, 10, 5, Pens.Blue);
            int z1x = calculateNewCoordonate(z1.getX());
            int z1y = calculateNewCoordonate(z1.getY());

            //chose 3 zones random out of 5

            //calculate dispersion

            //draw points
            Graphics graphics = this.CreateGraphics();
            graphics.DrawEllipse(z1.getCuloare(), z1x, z1y, 5, 5);

            for (int i = 0; i < 1000; i++)
            {
                //draw 1000 different points 
                // graphics.DrawEllipse(z1.getCuloare(), z1x, z1y, 5, 5);
            }

        }

        private int calculateNewCoordonate(int coordonate)
        {
            int newCoordonate = coordonate + 300;
            return newCoordonate;
        }
    }
}
