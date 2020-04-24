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
        internal double scale;
        private Point offset;
        private readonly ViewPort viewPort;

        public Ellipse(Point initializePoint, Color contourColor, Color fillColor, int width, ViewPort viewPort)
        {
            firstCoord.X = (viewPort.firstPoint.X  + (initializePoint.X / viewPort.Scale));
            firstCoord.Y = (viewPort.firstPoint.Y  + (initializePoint.Y / viewPort.Scale));
            secondCoord.X = (viewPort.firstPoint.X + (initializePoint.X / viewPort.Scale));
            secondCoord.Y = (viewPort.firstPoint.Y + (initializePoint.Y / viewPort.Scale));

            this.contourColor = contourColor;
            this.fillColor = fillColor;
            this.width = width;

            scale = viewPort.Scale;
            offset = viewPort.firstPoint;
            this.viewPort = viewPort;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                var actualWidth = (int)(width * viewPort.Scale / scale) + 1;
                bitmap.FillEllipse((int)firstDrawingCoord.X - actualWidth, (int)firstDrawingCoord.Y - actualWidth, (int)secondDrawingCoord.X + actualWidth, (int)secondDrawingCoord.Y + actualWidth, contourColor);
                bitmap.FillEllipse((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, fillColor);
            }
        }

        public void ChangeLastPoint(Point newPoint)
        {
            secondCoord.X = offset.X + ((newPoint.X) / scale);
            secondCoord.Y = offset.Y + ((newPoint.Y) / scale);
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

            firstDrawingCoord.X = (firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            firstDrawingCoord.Y = (firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            secondDrawingCoord.X = (secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            secondDrawingCoord.Y = (secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
    }
}
