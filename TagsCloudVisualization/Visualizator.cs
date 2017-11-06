using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Visualizator
    {
        private Bitmap image;
        private Graphics graphics;
        private Pen pen;
        private List<Brush> colors;

        public Visualizator(Size sizeBackground)
        {
            image = new Bitmap(sizeBackground.Width, sizeBackground.Height);
            pen = new Pen(Color.Black, 2);
            graphics = Graphics.FromImage(image);
            graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), sizeBackground));
            colors = new List<Brush>() {Brushes.Bisque, Brushes.Gainsboro, Brushes.LightCyan};
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            var random = new Random();
            foreach (var rectangle in rectangles)
            {
                var numberColor = random.Next(0, colors.Count);
                graphics.FillRectangle(colors[numberColor], rectangle);
                graphics.DrawRectangle(pen, rectangle);
            }
        }

        public void SaveImage(string path)
        {
            image.Save(path);
        }
    }
}
