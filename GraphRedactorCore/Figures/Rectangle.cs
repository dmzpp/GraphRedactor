using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace GraphRedactorCore.Figures
{
    internal class Rectangle : DrawableElement
    {
        public Point FirstPoint { get => _firstPoint; set => _firstPoint = value; }
        public Point SecondPoint { get => _secondPoint; set => _secondPoint = value; }
        public Type BrushType { get => _brushType; set => _brushType = value; }
        public Type PenType { get => _penType; set => _penType = value; }
        public Color FillColor { get => _fillColor; set => _fillColor = value; }
        public Color ContourColor { get => _contourColor; set => _contourColor = value; }
        public double Width { get => _width; set => _width = value; }

        private Point _firstPoint;
        private Point _secondPoint;
        private Type _brushType;
        private Type _penType;
        private Color _fillColor;
        private Color _contourColor;
        private double _width;

        private Point _firstDrawingCoord;
        private Point _secondDrawingCoord;

        public Rectangle()
        {
            _rotateAngle = 0;
        }

        public Rectangle(Point initializePoint, Color contourColor, Type pen, Color fillColor, Type fillBrush, double width, int zIndex, double scale)
        {
            FirstPoint = initializePoint;
            SecondPoint = initializePoint;
            PenType = pen;
            BrushType = fillBrush;
            FillColor = fillColor;
            this.ContourColor = contourColor;
            Width = width;
            Scale = scale;
            _zIndex = zIndex;
            _rotateAngle = 0;
            OffsetX = 0;
            OffsetX = 0;
        }

        public override void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            var actualWidth = Width * viewPort.Scale / Scale;

            var brush = BrushPicker.GetBrush(BrushType).GetBrush(_fillColor, viewPort, Scale, _firstDrawingCoord, _secondDrawingCoord);
            var pen = PenPicker.GetPen(PenType).GetPen(viewPort, _contourColor, actualWidth);
            Point centerPoint = new Point()
            {
                X = (_firstDrawingCoord.X + _secondDrawingCoord.X) / 2,
                Y = (_firstDrawingCoord.Y + _secondDrawingCoord.Y) / 2
            };

            context.PushTransform(new RotateTransform(RotateAngle, centerPoint.X, centerPoint.Y));
            context.PushTransform(new ScaleTransform(_scale, _scale, centerPoint.X, centerPoint.Y));
            context.PushTransform(new TranslateTransform(OffsetX, OffsetY));

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

        public Rect ToRect()
        {
            return new Rect(_firstDrawingCoord, _secondDrawingCoord);
        }

        public override bool IsIntersect(Rect area)
        {
            return ToRect().IntersectsWith(area);
        }
    }
}
