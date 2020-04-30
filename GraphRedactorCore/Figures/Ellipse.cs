using System;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Ellipse : IDrawable
    {
        private readonly SolidColorBrush _fillColor;
        private readonly Pen _pen;

        internal Point firstCoord;
        internal Point secondCoord;

        private Point _firstDrawingCoord;
        private Point _secondDrawingCoord;
        private double _width;
        private double _scale;

        public Ellipse(Point initializePoint, Color contourColor, Color fillColor, double width, double scale)
        {
            firstCoord = initializePoint;
            secondCoord = initializePoint;
            _pen = new Pen(new SolidColorBrush(contourColor), width);
            _fillColor = new SolidColorBrush(fillColor);
            _width = width;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            _pen.Thickness = _width * viewPort.Scale / _scale;
            var diameters = Point.Subtract(_secondDrawingCoord, _firstDrawingCoord);
            context.DrawEllipse(_fillColor, _pen, Point.Subtract(_secondDrawingCoord, diameters / 2), diameters.X / 2, diameters.Y / 2);
        }

        public void ChangeLastPoint(Point newPoint)
            => secondCoord = newPoint;

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
