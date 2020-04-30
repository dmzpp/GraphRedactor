using System;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Rectangle : IDrawable
    {
        internal Point firstCoord;
        internal Point secondCoord;
        private Point _firstDrawingCoord;
        private Point _secondDrawingCoord;
        private SolidColorBrush _fillColor;
        private double _width;
        private double _scale;
        private readonly Pen pen;

        public Rectangle(Point initializePoint, Color contourColor, Color fillColor, double width, double scale)
        {
            firstCoord = initializePoint;
            secondCoord = initializePoint;
            pen = new Pen(new SolidColorBrush(contourColor), width);
            _fillColor = new SolidColorBrush(fillColor);
            _width = width;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            pen.Thickness = _width * viewPort.Scale / _scale;
            context.DrawRectangle(_fillColor, pen, new Rect(_firstDrawingCoord, _secondDrawingCoord));
        }

        public void ChangeLastPoint(Point newPoint) =>
            secondCoord = newPoint;

        private void CalculateDrawingCoordinats(ViewPort viewPort)
        {
            _firstDrawingCoord.X = Math.Min(firstCoord.X, secondCoord.X);
            _firstDrawingCoord.Y = Math.Min(firstCoord.Y, secondCoord.Y);
            _secondDrawingCoord.X = Math.Max(firstCoord.X, secondCoord.X);
            _secondDrawingCoord.Y = Math.Max(firstCoord.Y, secondCoord.Y);

            _firstDrawingCoord.X = (_firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _firstDrawingCoord.Y = (_firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            _secondDrawingCoord.X = (_secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _secondDrawingCoord.Y = (_secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
    }
}
