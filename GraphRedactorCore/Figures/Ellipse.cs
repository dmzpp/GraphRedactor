﻿using GraphRedactorCore.Brushes;
using GraphRedactorCore.Pens;
using System;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Figures
{
    internal class Ellipse : DrawableElement
    {
        public Point FirstPoint { get => _firstCoord; set => _firstCoord = value; }
        public Point SecondPoint { get => _secondCoord; set => _secondCoord = value; }
        public Type BrushType { get => _brushType; set => _brushType = value; }
        public Type PenType { get => _penType; set => _penType = value; }
        public Color FillColor { get => _fillColor; set => _fillColor = value; }
        public Color ContourColor { get => _contourColor; set => _contourColor = value; }
        public double Width { get => _width; set => _width = value; }

        private Type _brushType;
        private Type _penType;
        private Point _firstCoord;
        private Point _secondCoord;
        private Color _fillColor;
        private Color _contourColor;
        internal Point firstDrawingCoord;
        internal Point secondDrawingCoord;
        private double _width;
        internal Vector diameters;
        internal double opacity;

        public Ellipse()
        {
            opacity = 1;
        }

        public Ellipse(Point initializePoint, Color contourColor, Type pen, Color fillColor, Type fillBrush, double width, double scale, int zIndex, double opacity = 1)
        {
            _firstCoord = initializePoint;
            _secondCoord = initializePoint;
            _fillColor = fillColor;
            _contourColor = contourColor;
            _penType = pen;
            _brushType = fillBrush;
            _width = width;
            _scale = scale;
            _zIndex = zIndex;
            _animationScale = 1;

            this.opacity = opacity;
        }

        public override void Draw(DrawingContext context, ViewPort viewPort)
        {
            CalculateDrawingCoordinats(viewPort);
            var actualWidth = _width * viewPort.Scale / _scale;
            diameters = Point.Subtract(secondDrawingCoord, firstDrawingCoord);

            var brush = BrushPicker.GetBrush(_brushType).GetBrush(_fillColor, viewPort, _scale, firstDrawingCoord, secondDrawingCoord, opacity);
            var pen = PenPicker.GetPen(_penType).GetPen(viewPort, _contourColor, actualWidth);

            var centerPoint = Point.Subtract(secondDrawingCoord, diameters / 2);
            context.PushTransform(new RotateTransform(RotateAngle, centerPoint.X, centerPoint.Y));
            context.PushTransform(new ScaleTransform(AnimationScale, AnimationScale, centerPoint.X, centerPoint.Y));
            context.PushTransform(new TranslateTransform(OffsetX, OffsetY));

            context.DrawEllipse(brush, pen, centerPoint, diameters.X / 2, diameters.Y / 2);
        }

        public void ChangeLastPoint(Point newPoint)
            => _secondCoord = newPoint;

        private void CalculateDrawingCoordinats(ViewPort viewPort)
        {
            firstDrawingCoord.X = Math.Min(_firstCoord.X, _secondCoord.X);
            firstDrawingCoord.Y = Math.Min(_firstCoord.Y, _secondCoord.Y);
            secondDrawingCoord.X = Math.Max(_firstCoord.X, _secondCoord.X);
            secondDrawingCoord.Y = Math.Max(_firstCoord.Y, _secondCoord.Y);

            firstDrawingCoord.X = (firstDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            firstDrawingCoord.Y = (firstDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
            secondDrawingCoord.X = (secondDrawingCoord.X - viewPort.firstPoint.X) * viewPort.Scale;
            secondDrawingCoord.Y = (secondDrawingCoord.Y - viewPort.firstPoint.Y) * viewPort.Scale;
        }
        
        public override bool IsIntersect(Rect area, ViewPort viewPort)
        {
            var rect = new Rect(firstDrawingCoord, secondDrawingCoord);
            return rect.IntersectsWith(area);
        }
    }
}
