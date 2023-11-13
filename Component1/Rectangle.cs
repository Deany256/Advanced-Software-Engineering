using System.Drawing;

namespace Component1
{
    class Rectangle : Shape
    {
        private int width, height;

        public Rectangle(Color colour, int x, int y, int width, int height, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.width = width;
            this.height = height;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(colour))
            {
                if (isFilled)
                {
                    g.FillRectangle(brush, x, y, width, height);
                }
                else
                {
                    g.DrawRectangle(new Pen(brush), x, y, width, height);
                }
            }
        }
    }
}