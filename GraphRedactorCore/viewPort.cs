using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphRedactorCore
{
    internal class ViewPort
    {
        public Point firstPoint;
        public Point secondPoint;
        public double Scale { get; set; }

        public ViewPort(double width, double height)
        {
            firstPoint = new Point(0, 0);
            secondPoint = new Point(width, height);
            Scale = 1;
        }

        public ViewPort(Point firstPoint, Point secondPoint, double scale)
        {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.Scale = scale;
        }
    }
}
