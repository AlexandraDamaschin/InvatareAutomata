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

            Graphics graphics = this.CreateGraphics();
            graphics.DrawEllipse(Pens.Black, 0, 300, 300, 50); 

        }
    }
}
