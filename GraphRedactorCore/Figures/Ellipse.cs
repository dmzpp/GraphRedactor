using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Ellipse : IDrawable
    {
        private Point firstCoord;
        private Point secondCoord;
        private Point firstDrawingCoord;
        private Point secondDrawingCoord;

        private Color fillColor;
        private Color contourColor;
        private int width;

        public Ellipse(Point initializePoint, Color contourColor, Color fillColor, int width)
        {
            firstCoord = secondCoord = initializePoint;
            this.contourColor = contourColor;
            this.fillColor = fillColor;
            this.width = width;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                bitmap.FillEllipse((int)firstDrawingCoord.X - width, (int)firstDrawingCoord.Y - width, (int)secondDrawingCoord.X + width, (int)secondDrawingCoord.Y + width, contourColor);
                bitmap.FillEllipse((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, fillColor);
            }
        }

        public void ChangeLastPoint(Point newPoint)
        {
            secondCoord = newPoint;
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
