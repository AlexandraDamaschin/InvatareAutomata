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
        int centersNo;
        int[] cX;
        int[] cY;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
