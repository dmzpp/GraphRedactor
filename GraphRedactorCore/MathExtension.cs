using System;
using System.Collections.Generic;
using System.Windows;

namespace GraphRedactorCore
{
    internal static class MathExtension
    {
        public static double CalculateAngle(Point center, Point p1)
        {
            var point = new Point(center.X, center.Y - Math.Sqrt((Math.Abs(p1.X - center.X) * Math.Abs(p1.X - center.X))
                     + (Math.Abs(p1.Y - center.Y) * Math.Abs(p1.Y - center.Y))));
            var result = (2 * Math.Atan2(p1.Y - point.Y, p1.X - point.X) * 180 / Math.PI) - 90;
            if (result < 0)
            {
                result += 360;
            }
            return result;
        }
        public static Point CalculateIntersectionPoint(Vector radiuses, Point centerPoint, Point point)
        {
            var newPoint = new Point()
            {
                X = point.X - centerPoint.X,
                Y = point.Y - centerPoint.Y
            };
            var points = FindEllipseSegmentIntersections(new Size(radiuses.X, radiuses.Y), new Point(0, 0), newPoint, new Point(0, 0));
            var firstPointSubstract = Point.Subtract(newPoint, points[0]);
            var secondPointSubstract = Point.Subtract(newPoint, points[1]);

            if (Math.Abs(firstPointSubstract.X) >= Math.Abs(secondPointSubstract.X) && Math.Abs(firstPointSubstract.Y) >= Math.Abs(secondPointSubstract.Y))
            {
                return new Point()
                {
                    X = points[1].X + centerPoint.X,
                    Y = points[1].Y + centerPoint.Y
                };
            }
            else
            {
                return new Point()
                {
                    X = points[0].X + centerPoint.X,
                    Y = points[0].Y + centerPoint.Y
                };
            }
        }
        public static Point[] FindEllipseSegmentIntersections(Size sizes, Point pt1, Point pt2, Point center)
        {
            var a = sizes.Width / 2;
            var b = sizes.Height / 2;

            var A = (pt2.X - pt1.X) * (pt2.X - pt1.X) / a / a + ((pt2.Y - pt1.Y) * (pt2.Y - pt1.Y) / b / b);
            var B = 2 * pt1.X * (pt2.X - pt1.X) / a / a + 2 * pt1.Y * (pt2.Y - pt1.Y) / b / b;
            var C = pt1.X * pt1.X / a / a + pt1.Y * pt1.Y / b / b - 1;

            var rValues = new List<double>();

            var discriminant = Math.Abs(B * B - 4 * A * C);
            if (discriminant == 0)
            {
                rValues.Add(-B / 2 / A);
            }
            else if (discriminant > 0)
            {
                rValues.Add(((-B + Math.Sqrt(discriminant)) / 2 / A));
                rValues.Add(((-B - Math.Sqrt(discriminant)) / 2 / A));
            }
            var points = new List<Point>();
            foreach (var t in rValues)
            {
                var x = pt1.X + (pt2.X - pt1.X) * t + center.X;
                var y = pt1.Y + (pt2.Y - pt1.Y) * t + center.Y;
                points.Add(new Point(x, y));
            }

            return points.ToArray();
        }

    }

}
