using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Rectangle : IDrawable
    {
        public Point FirstPoint { get => _firstPoint; set => _firstPoint = value; }
        public Point SecondPoint { get => _secondPoint; set => _secondPoint = value; }
        public Type BrushType { get => _brushType ; set => _brushType = value; }
        public Type PenType { get => _penType; set => _penType = value; }
        public Color FillColor { get => _fillColor; set => _fillColor = value; }
        public Color ContourColor { get => _contourColor; set => _contourColor = value; }
        public double Width { get => _width; set => _width = value; }
        public double Scale { get => _scale; set => _scale = value; }

        private Point _firstPoint;
        private Point _secondPoint;
        private Type _brushType;
        private Type _penType;
        private Color _fillColor;
        private Color _contourColor;
        private double _width;
        private double _scale;

        private Point _firstDrawingCoord;
        private Point _secondDrawingCoord;

        public Rectangle()
        {

        }

        public Rectangle(Point initializePoint, Color contourColor, Type pen, Color fillColor, Type fillBrush, double width, double scale)
        {
            FirstPoint = initializePoint;
            SecondPoint = initializePoint;
            PenType = pen;
            BrushType = fillBrush;
            FillColor = fillColor;
            this.ContourColor = contourColor;
            Width = width;
            Scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            var actualWidth = Width * viewPort.Scale / Scale;

            var brush = BrushPicker.GetBrush(BrushType).GetBrush(_fillColor, viewPort, Scale, _firstDrawingCoord, _secondDrawingCoord);
            var pen = PenPicker.GetPen(PenType).GetPen(viewPort, _contourColor, actualWidth);
            context.DrawRectangle(brush, pen, new Rect(_firstDrawingCoord, _secondDrawingCoord));
        }

        public void ChangeLastPoint(Point newPoint) =>
            SecondPoint = newPoint;

        private void CalculateDrawingCoordinats(ViewPort viewPort)
        {
            _firstDrawingCoord.X = Math.Min(FirstPoint.X, SecondPoint.X);
            _firstDrawingCoord.Y = Math.Min(FirstPoint.Y, SecondPoint.Y);
            _secondDrawingCoord.X = Math.Max(FirstPoint.X, SecondPoint.X);
            _secondDrawingCoord.Y = Math.Max(FirstPoint.Y, SecondPoint.Y);

            _firstDrawingCoord.X = (_firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _firstDrawingCoord.Y = (_firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            _secondDrawingCoord.X = (_secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _secondDrawingCoord.Y = (_secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
    }
}
