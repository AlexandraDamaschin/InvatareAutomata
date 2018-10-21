using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    class DataPoint
    {
        public int x;
        public int y;
        public int center;

        public DataPoint(int x, int y, int center)
        {
            this.x = x;
            this.y = y;
            this.center = center;
        }
    }
}
