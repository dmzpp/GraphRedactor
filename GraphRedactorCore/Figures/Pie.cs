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

namespace GraphRedactorCore.Figures
{
    internal class Pie : IDrawable
    {
        private Point _centerPoint;
        private Point _firstPoint;
        private Point _secondPoint;
        private double _angle;
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
            _angle = 0;
            _radiuses = radiuses;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            var geometry = new StreamGeometry();
            var firstAngle = angle(_centerPoint, _firstPoint);
            var secondAngle = angle(_centerPoint, _secondPoint);

            var pieAngle = secondAngle - firstAngle;
            var pieAngleDiff = pieAngle;
            if(pieAngle < 0)
            {
                pieAngleDiff = secondAngle + 360 - firstAngle;
            }
            
            var percents = 1D;
            if (pieAngleDiff < 180)
            {
                percents = 1 + (pieAngle - 180) / 180;
            }
            var radiuses = new Size()
            {
                Width = _radiuses.Width * percents,
                Height = _radiuses.Height * percents
            };

            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(_centerPoint, true, false);
                geometryContext.LineTo(_firstPoint, false, false);

                geometryContext.ArcTo(_secondPoint, radiuses, 360,true, SweepDirection.Clockwise, true, false);

                geometryContext.LineTo(_centerPoint, false, false);
            }
            var actualWidth = _width * viewPort.Scale / _scale;
            var pen = _pen.GetPen(viewPort, _contourColor, actualWidth);
            var brush = _brush.GetBrush( _fillColor, viewPort);

            context.DrawGeometry(brush, pen, geometry);
        }

        public void ChangeLastPoint(Point newPoint)
        {
            _secondPoint = newPoint;
            // calculate angle
        /*    var firstSide = Math.Sqrt(Math.Abs(_firstPoint.X - _centerPoint.X) + Math.Abs(_firstPoint.Y - _centerPoint.Y));
            var secondSide = Math.Sqrt(Math.Abs(_secondPoint.X - _centerPoint.X) + Math.Abs(_secondPoint.Y - _centerPoint.Y));
            var thirdSide = Math.Sqrt(Math.Abs(_firstPoint.X - _secondPoint.X) + Math.Abs(_firstPoint.Y - _secondPoint.Y));
           // var cos = ((firstSide * firstSide) + (secondSide * secondSide) - (thirdSide * thirdSide)) / (2 * firstSide * secondSide);
            double cos = Math.Round((_firstPoint.X * _secondPoint.X + _firstPoint.Y * _secondPoint.Y) /
                (Math.Sqrt(_firstPoint.X * Machine.X + Machine.Y * Machine.Y) * Math.Sqrt(Destination.X * Destination.X + Destination.Y * Destination.Y)), 9);
            _angle = Math.Acos(cos) * 180 / Math.PI;*/
            //  arccos( (x1x2 + y1y2) / (R^2) ).
        }

        public static double angle(Point center, Point p1)
        {
            var p0 = new Point(center.X, center.Y - Math.Sqrt(Math.Abs(p1.X - center.X) * Math.Abs(p1.X - center.X)
                     + Math.Abs(p1.Y - center.Y) * Math.Abs(p1.Y - center.Y)));
            var result = ((2 * Math.Atan2(p1.Y - p0.Y, p1.X - p0.X)) * 180 / Math.PI) - 90;
            if (result < 0)
            {
                result += 360;
            }
            return result;
        }
    }
}
