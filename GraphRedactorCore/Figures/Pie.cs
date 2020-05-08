using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Drawing.Drawing2D;

namespace GraphRedactorCore.Figures
{
    internal class Pie : IDrawable
    {
        private Point _centerPoint;
        private Point _firstPoint;
        private Point _secondPoint;
        private double _width;

        private ICustomBrush _brush;
        private ICustomPen _pen;
        private Color _contourColor;
        private Color _fillColor;
        private Size _radiuses;
        private double _scale;
        public Pie(Point centerPoint, Point intersectionPoint,
            Color contourColor, ICustomPen pen, Color fillColor, ICustomBrush brush, double width, Size radiuses, double scale)
        {
            _centerPoint = centerPoint;
            _firstPoint = _secondPoint = intersectionPoint;
            _contourColor = contourColor;
            _brush = brush;
            _pen = pen;
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
            var pen = _pen.GetPen(viewPort, _contourColor, actualWidth);
            var brush = _brush.GetBrush(_fillColor, viewPort.Scale / _scale);
            geometry = new StreamGeometry();
            var radiuses = new Size()
            {
                Width = _radiuses.Width * viewPort.Scale,
                Height = _radiuses.Height * viewPort.Scale
            };

            if (pieAngle > 179)
            {
                var thirdPointAngle = (firstAngle > secondAngle) ? firstAngle - pieAngle / 2 : firstAngle + pieAngle / 2;
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
                        geometryContext.ArcTo(thirdPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.ArcTo(secondPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.LineTo(centerPoint, false, false);
                    }
                    else
                    {
                        geometryContext.BeginFigure(centerPoint, true, false);
                        geometryContext.LineTo(secondPoint, false, false);
                        geometryContext.ArcTo(thirdPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.ArcTo(firstPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.LineTo(centerPoint, false, false);
                    }
                }
            }
            else
            {
                using (var geometryContext = geometry.Open())
                {
                    if (firstAngle < secondAngle)
                    {
                        geometryContext.BeginFigure(centerPoint, true, false);
                        geometryContext.LineTo(firstPoint, false, false);
                        geometryContext.ArcTo(secondPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.LineTo(centerPoint, false, false);
                    }
                    else
                    {
                        geometryContext.BeginFigure(centerPoint, true, false);
                        geometryContext.LineTo(secondPoint, false, false);
                        geometryContext.ArcTo(firstPoint, radiuses, 0, false, SweepDirection.Clockwise, true, false);
                        geometryContext.LineTo(centerPoint, false, false);
                    }
                }
            }
           // geometry.Freeze();
            context.DrawGeometry(brush, pen, geometry);

        }

        public void ChangeLastPoint(Point newPoint)
        {
            _secondPoint = newPoint;
        }
    }
}
