using System.Drawing;

namespace Component1
{
    class Circle : Shape
    {
        private int radius;

        public Circle(Color colour, int x, int y, int radius, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(colour))
            {
                if (isFilled)
                {
                    g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
                }
                else
                {
                    g.DrawEllipse(new Pen(brush), x - radius, y - radius, 2 * radius, 2 * radius);
                }
            }
        }
    }
}
