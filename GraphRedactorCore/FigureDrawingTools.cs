using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore
{
    internal static class FigureDrawingTools
    {
        public static List<double> Interpolate(int x1, int y1, int x2, int y2, int width)
        {
            var result = new List<double>();
            double firstSide = x2 - x1;
            double secondSide = y2 - y1;

            var length = Math.Sqrt(firstSide * firstSide + secondSide * secondSide);
            var pointsCount = Math.Ceiling(length / width);

            firstSide = firstSide / (pointsCount + 1);
            secondSide = secondSide / (pointsCount + 1);

            for(var i = 1; i <= pointsCount; i++)
            {
                result.Add(x1 + firstSide * i);
                result.Add(y1 + secondSide * i);
            }
            
            return result;
        }
    }
}

