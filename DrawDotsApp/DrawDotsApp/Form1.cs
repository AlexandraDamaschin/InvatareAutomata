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
