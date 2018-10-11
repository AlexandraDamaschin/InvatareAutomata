namespace DrawDotsApp
{
    public class Zone
    {
        private int mx;
        private int my;
        private int delta_x;
        private int delta_y;
        private int culoare;

        //constructor
        public Zone(int x, int y, int deltax, int deltay, int cul)
        {
            mx = x;
            my = y;
            deltax = delta_x;
            deltay = delta_y;
            culoare = cul;
        }

        public int getX()
        {
            return mx;
        }
        public int getY()
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
        public int getCuloare()
        {
            return culoare;
        }
    }
}
