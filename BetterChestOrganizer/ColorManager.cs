using System;

namespace BetterChestOrganizer
{
    public class Color
    {
        private int R { get; }
        private int G { get; }
        private int B { get; }

        public double H { get; set; }
        public double S { get; set; }
        public double V { get; set; }

        private void ComputeHsv()
        {
            var min = Math.Min(R, Math.Min(G, B));
            var max = Math.Max(R, Math.Max(G, B));
            V = max;
            var delta = max - min;
            if (max == 0 || delta == 0)
            {
                S = 0;
                H = 0; // Actually undefined.
                return;
            }

            if (max > 0)
            {
                S = delta / (double) max;
            }

            if (R >= max)
            {
                H = (G - B) / 255.0 / delta;
            }
            else if (G >= max)
            {
                H = 2.0 + (B - R) / 255.0 / delta;
            }
            else
            {
                H = 4.0 + (R - G) / 255.0 / delta;
            }

            H *= 60.0;

            if (H < 0.0)
            {
                H += 360.0;
            }
        }

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
            ComputeHsv();
        }
    }
}