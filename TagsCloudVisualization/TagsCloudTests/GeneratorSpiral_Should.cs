using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;

namespace TagsCloudVisualization
{
    [TestFixture]
    class GeneratorSpiral_Should
    {
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        [TestCase(-1, -1)]
        public void CenterWithNegativeCoordinates_ExpectedArgumentException(int x, int y)
        {
            var center = new Point(x, y);
            Action act = () => new GeneratorSpiral(center);
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void FirstPosition_ShouldBeCenter()
        {
            var center = new Point(200, 200);
            var generator = new GeneratorSpiral(center);
            generator.GetNextPosition().Should().Be(center);
        }

        [Test]
        public void RadiusAndAngleAfterShift_ShouldBeEqualsOffset()
        {
            var offsetAngle = 1;
            var offsetRadius = 2;
            var center = new Point(200, 200);
            var generator = new GeneratorSpiral(center, offsetAngle, offsetRadius);
            generator.GetNextPosition();
            generator.GetNextPosition();
            generator.Radius.Should().Be(offsetRadius);
            generator.Angle.Should().Be(offsetAngle);
        }
    }
}
