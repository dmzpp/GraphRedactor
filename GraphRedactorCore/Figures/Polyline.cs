using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class PolyLine : IDrawable
    {
        private readonly List<Point> _points;
        private readonly Pen _pen;
        private double _width;
        private double _scale;

        public PolyLine(Point initializePoint, Color contourColor, double width, double scale)
        {
            _points = new List<Point>
            {
                initializePoint,
                initializePoint
            };
            _pen = new Pen(new SolidColorBrush(contourColor), width);
            _width = width;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
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
            _pen.Thickness = actualWidth;
            context.DrawGeometry(null, _pen, geometry);
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

    }
}
