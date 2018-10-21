using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    class DataPoint
    {
        public int X;
        public int Y;
        public int Center;

        public DataPoint(int x, int y, int center)
        {
            X = x;
            Y = y;
            Center = center;
        }
    }
}
