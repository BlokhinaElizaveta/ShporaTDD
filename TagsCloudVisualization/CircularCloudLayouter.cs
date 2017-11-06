using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter : ICircularCloudLayouter
    {
        public readonly Point Center;
        public List<Rectangle> Rectangles { get;}
        private GeneratorSpiral generatorSpiral;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            Rectangles = new List<Rectangle>();
            generatorSpiral = new GeneratorSpiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var nextRectangle = new Rectangle(generatorSpiral.GetNextPosition(), rectangleSize);
            while (nextRectangle.IntersectsWithRectangles(Rectangles))
                nextRectangle = new Rectangle(generatorSpiral.GetNextPosition(), rectangleSize);
            nextRectangle = MoveToCenter(nextRectangle);
            Rectangles.Add(nextRectangle);
            return nextRectangle;
        }

        private Rectangle MoveToCenter(Rectangle rectangle)
        {
            while (true)
            {
                var direction = Center - new Size(rectangle.GetCenter());
                var offsetRectangle = MoveRectangleByOnePoint(rectangle, new Point(Math.Sign(direction.X), 0));
                if (offsetRectangle == rectangle)
                    break;

                offsetRectangle = MoveRectangleByOnePoint(offsetRectangle, new Point(0, Math.Sign(direction.Y)));
                if (offsetRectangle == rectangle)
                    break;
                rectangle = offsetRectangle;
            }
            return rectangle;
        }


        private Rectangle MoveRectangleByOnePoint(Rectangle rectangle, Point offset)
        {
            var offsetRectangle = new Rectangle( rectangle.Location + new Size(offset) , rectangle.Size);
            if (offsetRectangle.IntersectsWithRectangles(Rectangles))
                return rectangle;
            return offsetRectangle;
        }
    }
}
