using GraphRedactorCore.Pens;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class PolyLine : DrawableElement
    {
        public List<Point> Points { get => _points; set => _points = value; }
        public Type PenType { get => _penType; set => _penType = value; }
        public double Width { get => _width; set => _width = value; }
        public Color ContourColor { get => _contourColor; set => _contourColor = value; }

        private List<Point> _points;
        private Type _penType;
        private double _width;
        private Color _contourColor;

        public PolyLine()
        {

        }

        public PolyLine(Point initializePoint, Color contourColor, Type pen, double width, double scale, int zIndex)
        {
            _points = new List<Point>
            {
                initializePoint,
                initializePoint
            };
            _penType = pen;
            _width = width;
            _scale = scale;
            _zIndex = zIndex;
            _contourColor = contourColor;
            _animationScale = 1;
        }

        public override void Draw(DrawingContext context, ViewPort viewPort)
        {
            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(
                    new Point((_points[0].X - viewPort.firstPoint.X) * viewPort.Scale,
                                (_points[0].Y - viewPort.firstPoint.Y) * viewPort.Scale), true, false);

                for (int i = 1; i < _points.Count; i++)
                {
                    Point newPoint = new Point(
                        (_points[i].X - viewPort.firstPoint.X) * viewPort.Scale,
                        (_points[i].Y - viewPort.firstPoint.Y) * viewPort.Scale);
                    geometryContext.LineTo(newPoint, true, true);
                }
            }

            var actualWidth = _width * viewPort.Scale / _scale;
            var pen = PenPicker.GetPen(_penType).GetPen(viewPort, _contourColor, actualWidth);

            var centerPoint = new Point(geometry.Bounds.X + geometry.Bounds.Width / 2, geometry.Bounds.Y + geometry.Bounds.Height / 2);
            context.PushTransform(new RotateTransform(RotateAngle, centerPoint.X, centerPoint.Y));
            context.PushTransform(new ScaleTransform(AnimationScale, AnimationScale, centerPoint.X, centerPoint.Y));
            context.PushTransform(new TranslateTransform(OffsetX, OffsetY));
            context.DrawGeometry(null, pen, geometry);
        }

        public void AddPoint(Point newPoint)
        {
            _points.Add(newPoint);
        }

        public void ChangeLastPoint(Point newPoint)
        {
            if (_points.Count > 1)
            {
                _points[_points.Count - 1] = newPoint;
            }
        }

        public override bool IsIntersect(Rect area)
        {
            if (_points.Count == 2)
            {
                return area.IntersectsWith(new Rect(_points[0], _points[1]));
            }
            else
            {
                foreach (var point in _points)
                {
                    if (area.IntersectsWith(new Rect(point, point)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
