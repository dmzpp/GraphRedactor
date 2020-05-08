using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Ellipse : IDrawable
    {
        private ICustomBrush _brush;
        private ICustomPen _pen;
        internal Point firstCoord;
        internal Point secondCoord;

        private Color _fillColor;
        private Color _contourColor;

        internal Point firstDrawingCoord;
        internal Point secondDrawingCoord;
        private double _width;
        private double _scale;
        internal Vector diameters;

        internal double opacity;

        public Ellipse(Point initializePoint, Color contourColor, ICustomPen pen, Color fillColor, ICustomBrush fillBrush, double width, double scale, double opacity = 1)
        {
            firstCoord = initializePoint;
            secondCoord = initializePoint;
            _fillColor = fillColor;
            _contourColor = contourColor;
            _pen = pen;
            _brush = fillBrush;
            _width = width;
            _scale = scale;

            this.opacity = opacity;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            var actualWidth = _width * viewPort.Scale / _scale;
            diameters = Point.Subtract(secondDrawingCoord, firstDrawingCoord);
            var brush = _brush.GetBrush(_fillColor, viewPort.Scale / _scale, opacity);
            var pen = _pen.GetPen(viewPort, _contourColor, actualWidth);

            context.DrawEllipse(brush, pen, Point.Subtract(secondDrawingCoord, diameters / 2), diameters.X / 2, diameters.Y / 2);
        }

        public void ChangeLastPoint(Point newPoint)
            => secondCoord = newPoint;

        private void CalculateDrawingCoordinats(ViewPort viewPort)
        {
            firstDrawingCoord.X = Math.Min(firstCoord.X, secondCoord.X);
            firstDrawingCoord.Y = Math.Min(firstCoord.Y, secondCoord.Y);
            secondDrawingCoord.X = Math.Max(firstCoord.X, secondCoord.X);
            secondDrawingCoord.Y = Math.Max(firstCoord.Y, secondCoord.Y);

            firstDrawingCoord.X = (firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            firstDrawingCoord.Y = (firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            secondDrawingCoord.X = (secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            secondDrawingCoord.Y = (secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
    }
}
