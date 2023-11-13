using System;
using System.Drawing;

namespace Component1
{
    /// <summary>
    /// Represents an abstract shape with a specified color, position, and fill status.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Gets or sets the color of the shape.
        /// </summary>
        protected Color colour;

        /// <summary>
        /// Gets or sets the X-coordinate of the shape's position.
        /// </summary>
        protected int x;

        /// <summary>
        /// Gets or sets the Y-coordinate of the shape's position.
        /// </summary>
        protected int y;

        /// <summary>
        /// Gets or sets a value indicating whether the shape should be filled.
        /// </summary>
        protected bool isFilled;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class with the specified parameters.
        /// </summary>
        /// <param name="colour">The color of the shape.</param>
        /// <param name="x">The X-coordinate of the shape's position.</param>
        /// <param name="y">The Y-coordinate of the shape's position.</param>
        /// <param name="isFilled">A value indicating whether the shape should be filled.</param>
        public Shape(Color colour, int x, int y, bool isFilled)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.isFilled = isFilled;
        }

        /// <summary>
        /// Draws the shape on the specified graphics surface.
        /// </summary>
        /// <param name="g">The graphics surface on which to draw the shape.</param>
        public abstract void Draw(Graphics g);
    }
}
