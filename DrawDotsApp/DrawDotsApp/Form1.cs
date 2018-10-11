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
            //get current x and y 
            int x = 0;
            int y = 0;

            //set zones 
            Zone z1 = new Zone(120, 30, 10, 5, Pens.Blue);
            calculateNewCoordonate(z1.getX());
            calculateNewCoordonate(z1.getY());

            //draw points
            Graphics graphics = this.CreateGraphics();
            graphics.DrawEllipse(Pens.Black, 0, 300, 300, 50);


            graphics.DrawEllipse(z1.getCuloare(), z1.getX(), z1.getY(), 5, 5);
        }

        private int calculateNewCoordonate(int coordonate)
        {
            int newCoordonate = coordonate + 300;
            return newCoordonate;
        }
    }
}
