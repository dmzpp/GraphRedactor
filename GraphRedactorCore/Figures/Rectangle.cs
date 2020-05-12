using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
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

        private Type _brushType;
        private Type _penType;

        private Color _fillColor;
        private Color _contourColor;

        private double _width;
        private double _scale;
        public Rectangle(Point initializePoint, Color contourColor, Type pen, Color fillColor, Type fillBrush, double width, double scale)
        {
            firstCoord = initializePoint;
            secondCoord = initializePoint;
            _penType = pen;
            _brushType = fillBrush;
            _fillColor = fillColor;
            this._contourColor = contourColor;
            _width = width;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            var actualWidth = _width * viewPort.Scale / _scale;

            var brush = BrushPicker.GetBrush(_brushType).GetBrush(_fillColor, viewPort, _scale, _firstDrawingCoord, _secondDrawingCoord);
            var pen = PenPicker.GetPen(_penType).GetPen(viewPort, _contourColor, actualWidth);
            context.DrawRectangle(brush, pen, new Rect(_firstDrawingCoord, _secondDrawingCoord));
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
