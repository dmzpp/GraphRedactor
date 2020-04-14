using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    class Ellipse : Figure
    {
        Point firstCoord;
        Point secondCoord;

        Point firstDrawingCoord;
        Point secondDrawingCoord;

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
