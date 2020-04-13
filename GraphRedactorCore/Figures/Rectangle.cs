using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;


namespace GraphRedactorCore.Figures
{
    internal class Rectangle : Figure
    {
        private Point firstCoord;
        private Point secondCoord;
        private Point firstDrawingCoord;
        private Point secondDrawingCoord;


        /// <summary>
        /// Инициализирует экземпляр прямоугольника
        /// </summary>
        /// <param name="initializeCoord">Точка, в которой создается прямоугольник</param>
        /// <param name="fillColor">Цвет заливки</param>
        /// <param name="contourColor">Цвет контура</param>
        /// <param name="width">Ширина контура</param>
        public Rectangle(Point initializeCoord, Color fillColor, Color contourColor, int width)
        {
            firstCoord = secondCoord = initializeCoord;
            this.fillColor = fillColor;
            this.contourColor = contourColor;
            this.width = width;
        }

        /// <summary>
        /// Заменяет вторую точку на ту, в которой происходит действие
        /// </summary>
        /// <param name="point">Точка, в которой произошёл вызов метода</param>
        public override void AddPoint(Point point)
        {
            secondCoord = point;
        }

        public override void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                bitmap.FillRectangle((int)firstDrawingCoord.X - width, (int)firstDrawingCoord.Y - width, (int)secondDrawingCoord.X + width, (int)secondDrawingCoord.Y + width, contourColor);
                bitmap.FillRectangle((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, fillColor);
            }
        }

        /// <summary>
        /// Распределяет координаты двух точек так, чтобы они были корректны для рисования
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
