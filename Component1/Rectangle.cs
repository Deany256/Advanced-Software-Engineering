using System.Drawing;

namespace Component1
{
    /// <summary>
    /// Represents a rectangle shape with a specified color, position, dimensions, and fill status.
    /// </summary>
    public class Rectangle : Shape
    {
        private int width;  // Width of the rectangle
        private int height; // Height of the rectangle

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class with the specified parameters.
        /// </summary>
        /// <param name="colour">The color of the rectangle.</param>
        /// <param name="x">The X-coordinate of the rectangle's position.</param>
        /// <param name="y">The Y-coordinate of the rectangle's position.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="isFilled">A value indicating whether the rectangle should be filled.</param>
        public Rectangle(Color colour, int x, int y, int width, int height, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Draws the rectangle on the specified graphics surface.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw the rectangle.</param>
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
