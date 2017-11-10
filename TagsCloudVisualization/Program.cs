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
            var visualizator = new Visualizator(new Size(1400, 800));
            var layouter = new CircularCloudLayouter(new Point(700, 400));
            var rectangles = new List<Rectangle>();
            var x = 73;
            var y = 10;
            var delta = 7;
            var interval = 100;
            for (var i = 1; i < 1000; i++)
            {
                if (i % interval == 0)
                    x -= delta;

                var size = new Size(x, y);
                rectangles.Add(layouter.PutNextRectangle(size));
            }
            visualizator.DrawRectangles(rectangles);
            visualizator.SaveImage(path);
        }
    }
}
