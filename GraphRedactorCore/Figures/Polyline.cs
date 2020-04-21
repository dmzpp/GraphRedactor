using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    class PolyLine : IDrawable
    {
        private readonly List<int> points;

        private Color contourColor;
        private int width;

        public PolyLine(Point initializePoint, Color contourColor, int width)
        {
            points = new List<int>();
            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);
            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);
            this.contourColor = contourColor;
            this.width = width;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                for (int i = 2; i < points.Count; i += 2)
                {
                    bitmap.FillEllipseCentered(points[i - 2], points[i - 1], width, width, contourColor);
                }
            }
        }

        public void AddPoint(Point newPoint)
        {
            if (points.Count == 0)
            {
                points.Add((int)newPoint.X);
                points.Add((int)newPoint.Y);
            }
            else
            {
                List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)newPoint.X, (int)newPoint.Y, width)
                    .ConvertAll<int>(new Converter<double, int>((value) => (int)value)));
                points.AddRange(newPoints);
            }
        }

        public void ChangeLastPoint(Point newPoint, bool SaveFirstPoints = false, int pointsCount = 1)
        {
            if (SaveFirstPoints && points.Count >= pointsCount * 2)
            {
                // удаляем все точки, кроме самой первой
                points.RemoveRange(pointsCount * 2, points.Count - pointsCount * 2);
            }
            // находим промежуточные точки
            List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)newPoint.X, (int)newPoint.Y, (width / 3) + 1)
                   .ConvertAll<int>(new Converter<double, int>((value) => (int)value)));
            points.AddRange(newPoints);

            // если промежуточных точек нет, то добавляем вторую точку
            if (newPoints == null)
            {
                points.Add((int)newPoint.X);
                points.Add((int)newPoint.Y);
            }
        }

    }
}
