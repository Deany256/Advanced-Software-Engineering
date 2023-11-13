using System.Drawing;

namespace Component1
{
    /// <summary>
    /// Represents a triangle shape with a specified color, position, side length, and fill status.
    /// </summary>
    public class Triangle : Shape
    {
        private int sideLength; // Length of each side of the triangle

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class with the specified parameters.
        /// </summary>
        /// <param name="colour">The color of the triangle.</param>
        /// <param name="x">The X-coordinate of the triangle's centroid.</param>
        /// <param name="y">The Y-coordinate of the triangle's centroid.</param>
        /// <param name="sideLength">The length of each side of the triangle.</param>
        /// <param name="isFilled">A value indicating whether the triangle should be filled.</param>
        public Triangle(Color colour, int x, int y, int sideLength, bool isFilled)
            : base(colour, x, y, isFilled)
        {
            this.sideLength = sideLength;
        }

        /// <summary>
        /// Draws the triangle on the specified graphics surface.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw the triangle.</param>
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
