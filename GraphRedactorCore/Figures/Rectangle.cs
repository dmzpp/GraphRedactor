﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Rectangle : IDrawable
    {
        internal Point firstCoord;
        internal Point secondCoord;
        internal Point firstDrawingCoord;
        internal Point secondDrawingCoord;

        internal Color fillColor;
        internal Color contourColor;
        internal int width;

        private readonly ViewPort viewPort;
        internal double scale;
        private Point offset;

        public Rectangle(Point initializePoint, Color contourColor, Color fillColor, int width, ViewPort viewPort)
        {
            firstCoord.X = (viewPort.firstPoint.X + initializePoint.X / viewPort.Scale);
            firstCoord.Y = (viewPort.firstPoint.Y + initializePoint.Y / viewPort.Scale);
            secondCoord.X = (viewPort.firstPoint.X + initializePoint.X / viewPort.Scale);
            secondCoord.Y = (viewPort.firstPoint.Y + initializePoint.Y / viewPort.Scale);

            this.fillColor = fillColor;
            this.contourColor = contourColor;
            this.width = width;
            this.viewPort = viewPort;
            scale = viewPort.Scale;
            offset = viewPort.firstPoint;
        }

        public void Draw(WriteableBitmap bitmap)
        {
            CalculateDrawingCoordinats();
            using (bitmap.GetBitmapContext())
            {
                var actualWidth = width * viewPort.Scale / scale;
                if (fillColor != Colors.Transparent)
                {
                    bitmap.FillRectangle((int)firstDrawingCoord.X - (int)(actualWidth), (int)firstDrawingCoord.Y - (int)(actualWidth), (int)secondDrawingCoord.X + (int)(actualWidth), (int)secondDrawingCoord.Y + (int)(actualWidth), contourColor);
                    bitmap.FillRectangle((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, fillColor);
                }
                else
                {
                    bitmap.DrawRectangle((int)firstDrawingCoord.X, (int)firstDrawingCoord.Y, (int)secondDrawingCoord.X, (int)secondDrawingCoord.Y, contourColor);
                }
            }
        }

        public void ChangeLastPoint(Point newPoint)
        {
            secondCoord.X = offset.X + (newPoint.X) / scale;
            secondCoord.Y = offset.Y + (newPoint.Y) / scale;
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