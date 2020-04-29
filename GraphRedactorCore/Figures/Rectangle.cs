using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace GraphRedactorCore.Figures
{
    internal class Rectangle : IDrawable
    {
        internal Point firstCoord;
        internal Point secondCoord;
        private Point _firstDrawingCoord;
        private Point _secondDrawingCoord;

        private Color _fillColor;
        private Color _borderColor;
        private double _width;


        private double _scale;
        private Pen pen;

        public Rectangle(Point initializePoint, Color contourColor, Color fillColor, double width, double scale)
        {
            firstCoord = initializePoint;
            secondCoord = initializePoint;
            pen = new Pen(new SolidColorBrush(contourColor), width);
            _fillColor = fillColor;
            _borderColor = contourColor;
            _width = width;
            _scale = scale;
        }

        public void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            pen.Thickness = _width * viewPort.Scale / _scale;
            context.DrawRectangle(new SolidColorBrush(_fillColor), pen, new Rect(_firstDrawingCoord, _secondDrawingCoord));
        }

        public void ChangeLastPoint(Point newPoint)
        {
            secondCoord = newPoint;
        }

        private void CalculateDrawingCoordinats(ViewPort viewPort)
        {
            if (firstCoord.X > secondCoord.X)
            {
                _firstDrawingCoord.X = secondCoord.X;
                _secondDrawingCoord.X = firstCoord.X;
            }
            else
            {
                _firstDrawingCoord.X = firstCoord.X;
                _secondDrawingCoord.X = secondCoord.X;
            }
            if (firstCoord.Y > secondCoord.Y)
            {
                _firstDrawingCoord.Y = secondCoord.Y;
                _secondDrawingCoord.Y = firstCoord.Y;
            }
            else
            {
                _firstDrawingCoord.Y = firstCoord.Y;
                _secondDrawingCoord.Y = secondCoord.Y;
            }

            _firstDrawingCoord.X = (_firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _firstDrawingCoord.Y = (_firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            _secondDrawingCoord.X = (_secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            _secondDrawingCoord.Y = (_secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
    }
}
