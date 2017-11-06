using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
    static class RectangleExtension
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            return rectangle.Location + new Size(rectangle.Width / 2, rectangle.Height / 2);
        }

        public static bool IntersectsWithRectangles(this Rectangle rectangle, IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}
