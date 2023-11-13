using System;
using System.Drawing;

namespace Component1
{
    abstract class Shape
    {
        protected Color colour; // Shape's colour
        protected int x, y;    // Position
        protected bool isFilled; // Indicates whether the shape should be filled

        public Shape(Color colour, int x, int y, bool isFilled)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.isFilled = isFilled;
        }

        public abstract void Draw(Graphics g);
    }
}