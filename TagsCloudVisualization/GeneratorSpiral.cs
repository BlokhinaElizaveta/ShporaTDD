using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class GeneratorSpiral
    {
        public readonly Point Center;
        public double Angle { get; private set; }
        public double Radius { get; private set; }
        private double offsetAngle;
        private double offsetRadius;
        private IEnumerator<Point> generator;

        public GeneratorSpiral(Point center, double offsetAngle=0.5, double offsetRadius = 0.1)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Center should be with positive cordinates");
            Center = center;
            this.offsetAngle = offsetAngle;
            this.offsetRadius = offsetRadius;
            generator = GenerateSpiral().GetEnumerator();
        }

        public Point GetNextPosition()
        {
            generator.MoveNext();
            return generator.Current;
        }

        private IEnumerable<Point> GenerateSpiral()
        {
            while (true)
            {
                yield return new Point((int)(Center.X + Radius * Math.Cos(Angle)),
                                       (int)(Center.Y + Radius * Math.Sin(Angle)));
                Radius += offsetRadius;
                Angle += offsetAngle;
            }         
        }
    }
}
