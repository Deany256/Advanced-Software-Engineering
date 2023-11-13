using System.Drawing;

namespace Component1
{
    class Triangle : Shape
    {
        private int sideLength;

        public Triangle(Color colour, int x, int y, int sideLength, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.sideLength = sideLength;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(colour))
            {
                Point[] points = new Point[3];

                points[0] = new Point(x, y - sideLength / 2);
                points[1] = new Point(x - sideLength / 2, y + sideLength / 2);
                points[2] = new Point(x + sideLength / 2, y + sideLength / 2);

                if (isFilled)
                {
                    g.FillPolygon(brush, points);
                }
                else
                {
                    g.DrawPolygon(new Pen(brush), points);
                }
            }
        }
    }
}
