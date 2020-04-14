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

        public Polyline(Point inititializePoint, Color color, int width)
        {
            points = new List<int>();
            points.Add((int)inititializePoint.X);
            points.Add((int)inititializePoint.Y);
            points.Add((int)inititializePoint.X);
            points.Add((int)inititializePoint.Y);
            this.contourColor = color;
            this.width = width;
        }
        public override void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                for (int i = 2; i < points.Count; i += 2)
                {
                    bitmap.FillEllipseCentered(points[i - 2], points[i - 1], width, width, contourColor);
                }
            }
        }
        /// <summary>
        /// Добавляет новую точку, при этом еще и добавляет все промежуточные значения между двумя точками, если это необходимо
        /// </summary>
        /// <param name="point">Точка для добавления</param>
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


        /// <summary>
        /// Заменяет последние координаты и, если это необходимо, добавляет промежуточные значения между двумя точками
        /// </summary>
        /// <param name="newPoint">Координаты, на которые необходимо заменить</param>
        /// <param name="IsLine">Указывает на то, вызывается ли данный метод для редактирования прямой линии</param>
        public void ChangeLastPoint(Point newPoint, bool IsLine = false)
        {
            if (IsLine)
            {
                // удаляем все точки, кроме самой первой
                points.RemoveRange(2, points.Count - 2);
            }
            // находим промежуточные точки
            List<int> newPoints = (FigureDrawingTools.Interpolate(points[points.Count - 2], points[points.Count - 1], (int)newPoint.X, (int)newPoint.Y, width)
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
