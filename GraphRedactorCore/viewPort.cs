using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
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
            CalculateScale(newWidth, newHeight);

            firstPoint.X = (point.X - (newWidth / 2)) < 0 ? 0 : point.X - (newWidth / 2);
            firstPoint.Y = (point.Y - (newHeight / 2)) < 0 ? 0 : point.Y - (newHeight / 2);
            secondPoint.X = point.X + (newWidth / 2);
            secondPoint.Y = point.Y + (newHeight / 2);

        }

        public void Calculate(Point firstPoint, Point secondPoint)
        {
            var newWidth = secondPoint.X - firstPoint.X;
            var newHeight = secondPoint.Y - firstPoint.Y;
            CalculateScale(newWidth, newHeight);

            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
        }

        private void CalculateScale(double newWidth, double newHeight)
        {
            var scaleX = ((secondPoint.X - firstPoint.X) / newWidth) < 1
                ? 0.5 + ((secondPoint.X - firstPoint.X) / newWidth)
                : ((secondPoint.X - firstPoint.X) / newWidth);
            var scaleY = ((secondPoint.Y - firstPoint.Y) / newHeight) < 1
                ? 0.5 + ((secondPoint.Y - firstPoint.Y) / newHeight)
                : ((secondPoint.Y - firstPoint.Y) / newHeight);
            Scale = Math.Min(scaleX, scaleY) < 1 ? 0.5 + Math.Min(scaleX, scaleY) : Math.Min(scaleX, scaleY);
        }
    }
}
