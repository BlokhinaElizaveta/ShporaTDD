using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;

namespace TagsCloudVisualization
{
    [TestFixture]
    class RectangleExtension_Should
    {
        [TestCase(0, 0, 100, 50, 50, 25, TestName = "Rectangle without shift")]
        [TestCase(10, 0, 100, 50, 60, 25, TestName = "Rectangle with x-shift should be center with x-shift")]
        [TestCase(0, 10, 100, 50, 50, 35, TestName = "Rectangle with y-shift should be center with y-shift")]
        public void GetCenter(int x, int y, int width, int height, int xCenter, int yCenter)
        {
            var rectangle = new Rectangle(x, y, width, height);
            rectangle.GetCenter().Should().Be(new Point(xCenter, yCenter));
        }

        [TestCase(0, 0, 100, 50, ExpectedResult = true, TestName = "Intersecting rectangles")]
        [TestCase(130, 130, 10, 40, ExpectedResult = false, TestName = "Non-ntersecting rectangles")]
        public bool IntersectsWithOtherRectangles(int x, int y, int width, int height)
        {
            var rectangle = new Rectangle(x, y, width, height);
            var rectangles = new[] {new Rectangle(0, 0, 10, 10), new Rectangle(20, 20, 100, 50)};
            return rectangle.IntersectsWithRectangles(rectangles);
        }
    }
}
