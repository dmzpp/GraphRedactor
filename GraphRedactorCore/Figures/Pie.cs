using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Pie : IDrawable
    {
        public Point CenterPoint { get => _centerPoint; set => _centerPoint = value; }
        public Point FirstPoint { get => _firstPoint; set => _firstPoint = value; }
        public Point SecondPoint { get => _secondPoint; set => _secondPoint = value; }
        public Type BrushType { get => _brushType; set => _brushType = value; }
        public Type PenType { get => _penType; set => _penType = value; }
        public Color FillColor { get => _fillColor; set => _fillColor = value; }
        public Color ContourColor { get => _contourColor; set => _contourColor = value; }
        public double Width { get => _width; set => _width = value; }
        public double Scale { get => _scale; set => _scale = value; }
        public Size Radiuses
        {
            get => _radiuses;
            set
            {
                _radiuses.Width = value.Width;
                _radiuses.Height = value.Height;
            }
        }

        private Point _centerPoint;
        private Point _firstPoint;
        private Point _secondPoint;
        private double _width;

        private Type _brushType;
        private Type _penType;
        private Color _contourColor;
        private Color _fillColor;
        private Size _radiuses;
        private double _scale;

        public Pie()
        {

        }
        public Pie(Point centerPoint, Point intersectionPoint,
            Color contourColor, Type pen, Color fillColor, Type brush, double width, Size radiuses, double scale)
        {
            _centerPoint = centerPoint;
            _firstPoint = _secondPoint = intersectionPoint;
            _contourColor = contourColor;
            _brushType = brush;
            _penType = pen;
            _fillColor = fillColor;
            _width = width;
            _radiuses = radiuses;
            _scale = scale;

        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {

            Point centerPoint = new Point()
            {
                X = (_centerPoint.X - viewPort.firstPoint.X) * viewPort.Scale,
                Y = (_centerPoint.Y - viewPort.firstPoint.Y) * viewPort.Scale,
            };
            Point firstPoint = new Point()
            {
                X = (_firstPoint.X - viewPort.firstPoint.X) * viewPort.Scale,
                Y = (_firstPoint.Y - viewPort.firstPoint.Y) * viewPort.Scale,
            };
            Point secondPoint = new Point()
            {
                X = (_secondPoint.X - viewPort.firstPoint.X) * viewPort.Scale,
                Y = (_secondPoint.Y - viewPort.firstPoint.Y) * viewPort.Scale,
            };

            var firstAngle = MathExtension.CalculateAngle(centerPoint, firstPoint);
            var secondAngle = MathExtension.CalculateAngle(centerPoint, secondPoint);

            var pieAngle = Math.Abs(secondAngle - firstAngle);

            var geometry = new StreamGeometry();
            var actualWidth = _width * viewPort.Scale / _scale;
            var pen = PenPicker.GetPen(_penType).GetPen(viewPort, _contourColor, actualWidth);
            var brush = BrushPicker.GetBrush(_brushType).GetBrush(_fillColor, viewPort, _scale, firstPoint, secondPoint);

            geometry = new StreamGeometry();
            var radiuses = new Size()
            {
                Width = _radiuses.Width * viewPort.Scale,
                Height = _radiuses.Height * viewPort.Scale
            };

            var thirdPointAngle = (firstAngle > secondAngle) ? firstAngle - (pieAngle / 2) : firstAngle + (pieAngle / 2);
            var cos = Math.Cos(thirdPointAngle * Math.PI / 180);
            var sin = Math.Sin(thirdPointAngle * Math.PI / 180);

            var thirdPoint = new Point()
            {
                X = (_radiuses.Width * cos) + _centerPoint.X,
                Y = (_radiuses.Height * sin) + _centerPoint.Y
            };
            thirdPoint.X = (thirdPoint.X - viewPort.firstPoint.X) * viewPort.Scale;
            thirdPoint.Y = (thirdPoint.Y - viewPort.firstPoint.Y) * viewPort.Scale;

            using (var geometryContext = geometry.Open())
            {
                if (firstAngle < secondAngle)
                {
                    geometryContext.BeginFigure(centerPoint, true, false);
                    geometryContext.LineTo(firstPoint, false, false);
                    if (pieAngle > 179)
                    {
                        geometryContext.ArcTo(thirdPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                    }
                    geometryContext.ArcTo(secondPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                    geometryContext.LineTo(centerPoint, false, false);
                }
                else
                {
                    geometryContext.BeginFigure(centerPoint, true, false);
                    geometryContext.LineTo(secondPoint, false, false);
                    if (pieAngle > 179)
                    {
                        geometryContext.ArcTo(thirdPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                    }
                    geometryContext.ArcTo(firstPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                    geometryContext.LineTo(centerPoint, false, false);
                }
            }
            context.DrawGeometry(brush, pen, geometry);
        }

        public void ChangeLastPoint(Point newPoint)
        {
            _secondPoint = newPoint;
        }
    }
}
