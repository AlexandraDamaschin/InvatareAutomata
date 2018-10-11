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
            //set zones 
            Zone z1 = new Zone(120, 30, 10, 5, Pens.Blue);

            //draw points
            Graphics graphics = this.CreateGraphics();
            graphics.DrawEllipse(Pens.Black, 0, 300, 300, 50);


            graphics.DrawEllipse(z1.getCuloare(), z1.getX(), z1.getY(), 5, 5);
        }
    }
}
