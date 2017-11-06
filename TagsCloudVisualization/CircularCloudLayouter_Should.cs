using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;

        [Test]
        public void EmptyAfterCreation()
        {
            layouter = new CircularCloudLayouter(new Point(10, 10));
            layouter.Rectangles.Should().BeEmpty();
        }

        [Test]
        public void CenterWithNegativeCoordinates_ExpectedArgumentException()
        {
            var center = new Point(-1, -1);
            Action act = () => new CircularCloudLayouter(center);
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void FirstRectangle_ShouldBeInCenter()
        {
            var center = new Point(100, 100);
            layouter = new CircularCloudLayouter(center);
            layouter.PutNextRectangle(new Size(50, 40));
            layouter.Rectangles.First().GetCenter().Should().Be(center);
        }

        [Test]
        public void AfterAddRectangle_RectanglesShouldNotBeIntersect()
        {
            var center = new Point(100, 100);
            layouter = new CircularCloudLayouter(center);
            var first = layouter.PutNextRectangle(new Size(50, 40));
            var second = layouter.PutNextRectangle(new Size(60, 90));
            first.IntersectsWith(second).Should().BeFalse();
        }

        [TestCase(600, 20, 30)]
        [TestCase(50, 60, 90)]
        [TestCase(100, 30, 70)]
        public void CoatingDensity(int count, int min, int max)
        {
            layouter = new CircularCloudLayouter(new Point(750, 400));
            var rectanglesSize = GenerateRectangles(count, min, max);
            var areaRectangles = 0;
            double radius = 0;
            foreach (var size in rectanglesSize)
            {
                var rect = layouter.PutNextRectangle(size);
                areaRectangles += rect.Height * rect.Width;

                var currentRadius = DistancePointToPoint(layouter.Center, rect.GetCenter());
                if (currentRadius > radius)
                    radius = currentRadius;
            }
            var expectedRadius = 1.3 * Math.Sqrt(areaRectangles / Math.PI);
            radius.Should().BeLessThan(expectedRadius);
        }

        private List<Size> GenerateRectangles(int count, int min, int max)
        {
            var rectangles = new List<Size>();
            var random = new Random();
            for (var i = 0; i < count; i++)
               rectangles.Add(new Size(random.Next(min, max), random.Next(min, max)));
            return rectangles;
        }

        private double DistancePointToPoint(Point a, Point b)
        {
            return Math.Pow((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y), 0.5);
        }

        [TearDown]
        public void CreateImade_IfTestFail()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory, TestContext.CurrentContext.Test.Name + ".bmp");
                var visualizator = new Visualizator(new Size(1500, 900));
                visualizator.DrawRectangles(layouter.Rectangles);
                visualizator.SaveImage(path);
                Console.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }
    }
}
