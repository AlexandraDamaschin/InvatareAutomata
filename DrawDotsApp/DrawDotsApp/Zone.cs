using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawDotsApp
{
    public class Zone
    {
        private float mx;
        private float my;
        private int delta_x;
        private int delta_y;
        private Pen  culoare;

        //constructor
        public Zone(float x, float y, float deltax, int deltay, Pen cul)
        {
            mx = x;
            my = y;
            deltax = delta_x;
            deltay = delta_y;
            culoare = cul;
        }

        public float getX()
        {
            return mx;
        }
        public float getY()
        {
            return my;
        }
        public int getDeltaX()
        {
            return delta_x;
        }
        public int getDeltaY()
        {
            return delta_y;
        }
        public Pen getCuloare()
        {
            return culoare;
        }
    }
}
