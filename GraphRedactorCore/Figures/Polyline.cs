using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    internal class PolyLine : IDrawable
    {
        private readonly List<int> points;

        internal Color contourColor;
        internal int width;

        private ViewPort viewPort;
        private double scale;
        private double offsetX;
        private double offsetY;

        public PolyLine(Point initializePoint, Color contourColor, int width, ViewPort viewPort)
        {
            points = new List<int>();

            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);
            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);

           /* points.Add((int)(viewPort.firstPoint.X + initializePoint.X / viewPort.ScaleX));
            points.Add((int)(viewPort.firstPoint.Y + initializePoint.Y / viewPort.ScaleY));
            points.Add((int)(viewPort.firstPoint.X + initializePoint.X / viewPort.ScaleX));
            points.Add((int)(viewPort.firstPoint.Y + initializePoint.Y / viewPort.ScaleY));*/


            this.contourColor = contourColor;
            this.width = width;

            this.viewPort = viewPort;
            scale = viewPort.Scale;
            offsetX = viewPort.firstPoint.X;
            offsetY = viewPort.firstPoint.Y;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                for (int i = 2; i < points.Count; i += 2)
                {
                    int firstCoord, secondCoord;
                    int actualWidth = (int)(width * viewPort.Scale / scale) + 1;
                    if (scale != viewPort.Scale)
                    {
                        firstCoord = (int)((offsetX + points[i - 2] / scale - viewPort.firstPoint.X) * viewPort.Scale);
                        secondCoord = (int)((offsetY + points[i - 1] / scale - viewPort.firstPoint.Y) * viewPort.Scale);
                    }
                    else
                    {
                        firstCoord = points[i - 2];
                        secondCoord = points[i - 1];
                    }
                    bitmap.FillEllipseCentered(firstCoord, secondCoord, actualWidth, actualWidth, contourColor);
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

            int pointX = (int)(viewPort.firstPoint.X + newPoint.X / viewPort.ScaleX);
            int pointY = (int)(viewPort.firstPoint.Y + newPoint.Y / viewPort.Scale);
            // находим промежуточные точки
            List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], pointX, pointY, (width / 3) + 1)
                   .ConvertAll<int>(new Converter<double, int>((value) => (int)value)));
            points.AddRange(newPoints);

            // если промежуточных точек нет, то добавляем вторую точку
            if (newPoints == null)
            {
                points.Add((int)(viewPort.firstPoint.X + newPoint.X / viewPort.ScaleX));
                points.Add((int)(viewPort.firstPoint.Y + newPoint.Y / viewPort.ScaleY));
            }
        
        }

    }
}
