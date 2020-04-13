using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace GraphRedactorCore.Figures
{
    internal class Polyline : Figure
    {
        private List<int> points;
        private bool isPencil;

        public Polyline(Color color, int width, bool isPencil = false)
        {
            this.contourColor = color;
            this.width = width;
            this.isPencil = isPencil;
            points = new List<int>();
        }
        public override void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                if (isPencil)
                {
                    for (int i = 2; i < points.Count; i += 2)
                    {
                        bitmap.FillEllipseCentered(points[i - 2], points[i - 1], width, width, contourColor);
                    }
                }
                else
                {

                }
            }
        }
        public override void AddPoint(Point point)
        {
            if(points.Count == 0)
            {
                points.Add((int)point.X);
                points.Add((int)point.Y);
            }
            else
            {
                List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)point.X, (int)point.Y, width)
                    .ConvertAll<int>(new Converter<double, int>((value) => (int)value)));
                points.AddRange(newPoints);
            }
        }
    }
}
