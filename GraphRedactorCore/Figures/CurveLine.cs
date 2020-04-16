using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    internal class CurveLine : Figure
    {
        private readonly List<Line> lines;
        private readonly List<int> points;

        public CurveLine(Point initializePoint, Color contourColor, Color fillColor, int width)
        {
            points = new List<int>();
            lines = new List<Line>();
            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);
            points.Add((int)initializePoint.X);
            points.Add((int)initializePoint.Y);
            this.fillColor = fillColor;
            this.contourColor = contourColor;
            this.width = width;

            lines.Add(new Line(initializePoint, fillColor, contourColor, width));
        }

        /// <summary>
        /// Добавляет новую точку для отображения линии.
        /// </summary>
        /// <param name="point">Новая точка</param>
        public override void AddPoint(Point point)
        {
            points[points.Count - 2] = (int)point.X;
            points[points.Count - 1] = (int)point.Y;
            lines.Last<Line>().AddPoint(point);
        }

        /// <summary>
        /// Устанавливает конечную точку, начиная с которой будет создаваться следующая линия
        /// </summary>
        /// <param name="point">Конечная точка</param>
        public void NextLine(Point point)
        {
            points.Add((int)point.X);
            points.Add((int)point.Y);

            lines.Add(new Line(point, fillColor, contourColor, width));
        }

        public override void Draw(WriteableBitmap bitmap)
        {
            using (bitmap.GetBitmapContext())
            {
                foreach (var line in lines)
                {
                    line.Draw(bitmap);
                }
            }
        }
    }
}
