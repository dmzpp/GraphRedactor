using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

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
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }

        public ViewPort(WriteableBitmap bitmap)
        {
            firstPoint.X = 0;
            firstPoint.Y = 0;
            secondPoint.X = bitmap.Width;
            secondPoint.Y = bitmap.Height;
            Scale = 1;
            ScaleX = ScaleY = 1;
        }

        public void Calculate(Point point, int toolScale = 2)
        {
            var newWidth = (secondPoint.X - firstPoint.X) / toolScale;
            var newHeight = (secondPoint.Y - firstPoint.Y) / toolScale;

            ScaleX = ((secondPoint.X - firstPoint.X) / newWidth) < 1
                ? 0.5 + ((secondPoint.X - firstPoint.X) / newWidth)
                : ((secondPoint.X - firstPoint.X) / newWidth);
            ScaleY = ((secondPoint.Y - firstPoint.Y) / newHeight) < 1
                ? 0.5 + ((secondPoint.Y - firstPoint.Y) / newHeight)
                : ((secondPoint.Y - firstPoint.Y) / newHeight);
            Scale = Math.Min(ScaleX, ScaleY) < 1 ? 0.5 + Math.Min(ScaleX, ScaleY) : Math.Min(ScaleX, ScaleY);
            ScaleX = Scale;
            ScaleY = Scale;

            firstPoint.X = (point.X - newWidth / 2) < 0 ? 0 : point.X - newWidth / 2;
            firstPoint.Y = (point.X - newHeight / 2) < 0 ? 0 : point.Y - newHeight / 2;
            secondPoint.X = point.X + newWidth / 2;
            secondPoint.Y = point.Y + newHeight / 2;

        }

        public void Calculate(Point firstPoint, Point secondPoint)
        {
            var newWidth = secondPoint.X - firstPoint.X;
            var newHeight = secondPoint.Y - firstPoint.Y;
            ScaleX = ((this.secondPoint.X - this.firstPoint.X) / newWidth) < 1
                ? 0.5 + ((this.secondPoint.X - this.firstPoint.X) / newWidth)
                : ((this.secondPoint.X - this.firstPoint.X) / newWidth);
            ScaleY = ((this.secondPoint.Y - this.firstPoint.Y) / newHeight) < 1
                ? 0.5 + ((this.secondPoint.Y - this.firstPoint.Y) / newHeight)
                : ((this.secondPoint.Y - this.firstPoint.Y) / newHeight);
            Scale = Math.Min(ScaleX, ScaleY) < 1 ? 0.5 + Math.Min(ScaleX, ScaleY) : Math.Min(ScaleX, ScaleY);
            ScaleX = Scale;
            ScaleY = Scale;
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
        }
    }
}
