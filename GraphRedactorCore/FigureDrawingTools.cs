using System;
using System.Collections.Generic;
using System.Windows;

namespace GraphRedactorCore
{
    internal static class FigureDrawingTools
    {
        public static List<double> Interpolate(int x1, int y1, int x2, int y2, int width)
        {
            if (width == 0)
                return null;
            var result = new List<double>();
            double firstSide = x2 - x1;
            double secondSide = y2 - y1;

            var length = Math.Sqrt(firstSide * firstSide + secondSide * secondSide);
            var pointsCount = Math.Ceiling(length / width);

            firstSide = firstSide / (pointsCount + 1);
            secondSide = secondSide / (pointsCount + 1);

            for (var i = 1; i <= pointsCount; i++)
            {
                result.Add(x1 + firstSide * i);
                result.Add(y1 + secondSide * i);
            }

            return result;
        }

        public static List<int> InterpolateLagrange(Point first, Point second, Point third)
        {
            List<int> points = new List<int>();
            int firstX = 0;
            int secondX = 0;

            if (first.X > third.X)
            {
                firstX = (int)third.X;
                secondX = (int)first.X;
            }
            else
            {
                firstX = (int)first.X;
                secondX = (int)third.X;
            }

            for (var i = firstX; i < secondX; i++)
            {
                points.Add(i);
                points.Add((int)Interpolate(new Point[] { first, second, third }, i, 3));
            }

            return points;

        }
        private static double Interpolate(Point[] f, int xi, int n)
        {
            double result = 0; // Initialize result 

            for (int i = 0; i < n; i++)
            {
                // Compute individual terms 
                // of above formula 
                double term = f[i].Y;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                        term = term * (xi - f[j].X) /
                                  (f[i].X - f[j].X);
                }

                // Add current term to result 
                result += term;
            }
            return result;
        }
    }
}

