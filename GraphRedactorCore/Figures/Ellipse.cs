using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    internal class Ellipse : Figure
    {
        // изначально переданные координаты
        private Point firstCoord;
        private Point secondCoord;

        // координаты, по которым происходит построение окружности
        private Point firstDrawingCoord;
        private Point secondDrawingCoord;

        public Ellipse(Point initializeCoord, Color contourColor, Color fillColor, int width)
        {
            firstCoord = secondCoord = initializeCoord;
            this.contourColor = contourColor;
            this.fillColor = fillColor;
            this.width = width;
        }

        public override void AddPoint(Point point)
        {
            secondCoord = point;
        }

        public override void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                bitmap.FillEllipse((int)firstDrawingCoord.X - width, (int)firstDrawingCoord.Y - width, (int)secondDrawingCoord.X + width, (int)secondDrawingCoord.Y + width, contourColor);
                bitmap.FillEllipse((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, fillColor);
            }
        }

        /// <summary>
        /// Определяет координаты, необходимые для рисования
        /// </summary>
        private void CalculateDrawingCoordinats()
        {
            if (firstCoord.X > secondCoord.X)
            {
                firstDrawingCoord.X = secondCoord.X;
                secondDrawingCoord.X = firstCoord.X;
            }
            else
            {
                firstDrawingCoord.X = firstCoord.X;
                secondDrawingCoord.X = secondCoord.X;
            }
            if (firstCoord.Y > secondCoord.Y)
            {
                firstDrawingCoord.Y = secondCoord.Y;
                secondDrawingCoord.Y = firstCoord.Y;
            }
            else
            {
                firstDrawingCoord.Y = firstCoord.Y;
                secondDrawingCoord.Y = secondCoord.Y;
            }
        }
    }
}
