using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    internal class GraphGlobalData
    {
        public LinkedList<IDrawable> Drawables { get; set; }
        public WriteableBitmap Bitmap { get; set; }
        public ViewPort ViewPort { get; set; }
        public GraphGlobalData(WriteableBitmap bitmap)
        {
            Drawables = new LinkedList<IDrawable>();
            Bitmap = bitmap;
            ViewPort = new ViewPort(bitmap);
        }
    }

    internal class ViewPort
    {
        public Point firstPoint;
        public Point secondPoint;

        public double Scale { get; set; }

        public ViewPort(WriteableBitmap bitmap)
        {
            firstPoint.X = 0;
            firstPoint.Y = 0;
            secondPoint.X = bitmap.Width;
            secondPoint.Y = bitmap.Height;
            Scale = 1;
        }

        public void Calculate(Point point, int toolScale = 2)
        {
            var newWidth = (secondPoint.X - firstPoint.X) / toolScale;
            var newHeight = (secondPoint.Y - firstPoint.Y) / toolScale;
            Scale = toolScale;
            firstPoint.X = point.X - (newWidth / 2);
            firstPoint.Y = point.Y - (newHeight / 2);
            secondPoint.Y = point.Y + (newHeight / 2);
            secondPoint.X = point.X + (newWidth / 2);
        }
    }
}
