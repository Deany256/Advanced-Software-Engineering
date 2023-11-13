using System.Drawing;

namespace Component1
{
    /// <summary>
    /// Represents a circle shape with a specified color, position, radius, and fill status.
    /// </summary>
    public class Circle : Shape
    {
        private int radius; // Radius of the circle

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class with the specified parameters.
        /// </summary>
        /// <param name="colour">The color of the circle.</param>
        /// <param name="x">The X-coordinate of the circle's center.</param>
        /// <param name="y">The Y-coordinate of the circle's center.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="isFilled">A value indicating whether the circle should be filled.</param>
        public Circle(Color colour, int x, int y, int radius, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.radius = radius;
        }

        /// <summary>
        /// Draws the circle on the specified graphics surface.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw the circle.</param>
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
