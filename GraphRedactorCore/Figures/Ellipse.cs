using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private readonly GraphGlobalData globalData;

        public Ellipse(Point initializePoint, Color contourColor, Color fillColor, int width, GraphGlobalData globalData)
        {
            firstCoord.X = (globalData.ViewPort.firstPoint.X + (initializePoint.X / globalData.ViewPort.Scale));
            firstCoord.Y = (globalData.ViewPort.firstPoint.Y + (initializePoint.Y / globalData.ViewPort.Scale));
            secondCoord.X = (globalData.ViewPort.firstPoint.X + (initializePoint.X / globalData.ViewPort.Scale));
            secondCoord.Y = (globalData.ViewPort.firstPoint.Y + (initializePoint.Y / globalData.ViewPort.Scale));

            this.contourColor = contourColor;
            this.fillColor = fillColor;
            this.width = width;

            scale = globalData.ViewPort.Scale;
            offset = globalData.ViewPort.firstPoint;
            this.globalData = globalData;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                var actualWidth = (int)(width * globalData.ViewPort.Scale / scale) + 1;
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

            firstDrawingCoord.X = (firstDrawingCoord.X - globalData.ViewPort.firstPoint.X) * globalData.ViewPort.Scale;
            firstDrawingCoord.Y = (firstDrawingCoord.Y - globalData.ViewPort.firstPoint.Y) * globalData.ViewPort.Scale;
            secondDrawingCoord.X = (secondDrawingCoord.X - globalData.ViewPort.firstPoint.X) * globalData.ViewPort.Scale;
            secondDrawingCoord.Y = (secondDrawingCoord.Y - globalData.ViewPort.firstPoint.Y) * globalData.ViewPort.Scale;
        }
    }
}
