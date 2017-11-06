using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "example.bmp"); ;
            var visualizator = new Visualizator(new Size(1500, 800));
            var layouter = new CircularCloudLayouter(new Point(750, 400));
            var rectangles = new List<Rectangle>();
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(random.Next(50, 100), random.Next(10, 50));
                rectangles.Add(layouter.PutNextRectangle(size));
            }
            visualizator.DrawRectangles(rectangles);
            visualizator.SaveImage(path);
        }
    }
}
