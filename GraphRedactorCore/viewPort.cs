using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    internal class ViewPort
    {
        public Point firstPoint;
        public Point secondPoint;

        public double Scale { get; set; }
        private readonly GraphGlobalData globalData;

        public ViewPort(WriteableBitmap bitmap, GraphGlobalData globalData)
        {
            firstPoint.X = 0;
            firstPoint.Y = 0;
            secondPoint.X = bitmap.Width;
            secondPoint.Y = bitmap.Height;
            Scale = 1;
            this.globalData = globalData;
        }
        public ViewPort(Point firstPoint, Point secondPoint, double scale, GraphGlobalData globalData)
        {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.Scale = scale;
            this.globalData = globalData;
        }

        public ViewPort Calculate(Point point)
        {
            var newWidth = (globalData.FirstViewPort.secondPoint.X - globalData.FirstViewPort.firstPoint.X) / (Scale * 2);
            var newHeight = (globalData.FirstViewPort.secondPoint.Y - globalData.FirstViewPort.firstPoint.Y) / (Scale * 2);

            var scale = CalculateScale(newWidth, newHeight);
            Point fPoint = new Point();
            Point sPoint = new Point();

            fPoint.X = (point.X - (newWidth / 2)) < 0 ? 0 : point.X - (newWidth / 2);
            fPoint.Y = (point.Y - (newHeight / 2)) < 0 ? 0 : point.Y - (newHeight / 2);
            sPoint.X = point.X + (newWidth / 2);
            sPoint.Y = point.Y + (newHeight / 2);

            return new ViewPort(fPoint, sPoint, scale, globalData);
        }

        public ViewPort Calculate(Point firstPoint, Point secondPoint)
        {
            var newWidth = secondPoint.X - firstPoint.X;
            var newHeight = secondPoint.Y - firstPoint.Y;
            double scale = CalculateScale(newWidth, newHeight);

            return new ViewPort(firstPoint, secondPoint, scale, globalData);
        }

        private double CalculateScale(double newWidth, double newHeight)
        {
            var scaleX = ((globalData.FirstViewPort.secondPoint.X - globalData.FirstViewPort.firstPoint.X) / newWidth) < 1
                ? 0.5 + ((globalData.FirstViewPort.secondPoint.X - globalData.FirstViewPort.firstPoint.X) / newWidth)
                : ((globalData.FirstViewPort.secondPoint.X - globalData.FirstViewPort.firstPoint.X) / newWidth);
            var scaleY = ((globalData.FirstViewPort.secondPoint.Y - globalData.FirstViewPort.firstPoint.Y) / newHeight) < 1
                ? 0.5 + ((globalData.FirstViewPort.secondPoint.Y - globalData.FirstViewPort.firstPoint.Y) / newHeight)
                : ((globalData.FirstViewPort.secondPoint.Y - globalData.FirstViewPort.firstPoint.Y) / newHeight);
            var scale = Math.Min(scaleX, scaleY) < 1 ? 0.5 + Math.Min(scaleX, scaleY) : Math.Min(scaleX, scaleY);
            return scale;
        }
    }
}
