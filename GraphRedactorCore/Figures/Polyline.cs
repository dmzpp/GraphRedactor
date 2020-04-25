using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    internal class PolyLine : IDrawable
    {
        private readonly List<int> points;

        internal Color ContourColor { get; set; }
        internal int Width { get; set; }

        private readonly GraphGlobalData globalData;
        private double scale;
        private double offsetX;
        private double offsetY;

        public PolyLine(Point initializePoint, Color contourColor, int width, GraphGlobalData globalData)
        {
            points = new List<int>
            {
                (int)initializePoint.X,
                (int)initializePoint.Y,
                (int)initializePoint.X,
                (int)initializePoint.Y
            };

            ContourColor = contourColor;
            Width = width;

            this.globalData = globalData;
            scale = globalData.ViewPort.Scale;
            offsetX = globalData.ViewPort.firstPoint.X;
            offsetY = globalData.ViewPort.firstPoint.Y;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                for (int i = 2; i < points.Count; i += 2)
                {
                    int firstCoord, secondCoord;
                    int actualWidth = (int)(Width * globalData.ViewPort.Scale / scale) + 1;
                    firstCoord = (int)(((offsetX + (points[i - 2]) / scale - globalData.ViewPort.firstPoint.X)) * globalData.ViewPort.Scale);
                    secondCoord = (int)(((offsetY + (points[i - 1]) / scale - globalData.ViewPort.firstPoint.Y)) * globalData.ViewPort.Scale);

                    bitmap.FillEllipseCentered(firstCoord, secondCoord, actualWidth, actualWidth, ContourColor);
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
                List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)newPoint.X, (int)newPoint.Y, (Width / 3) + 1)
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
            List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)newPoint.X, (int)newPoint.Y, (Width / 3) + 1)
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
