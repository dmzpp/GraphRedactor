using GraphRedactorCore.Brushes;
using GraphRedactorCore.Figures;
using GraphRedactorCore.Pens;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class ZoomTool : Tool
    {
        private Rectangle rectangle = null;
        private enum States
        {
            stretching,
            none
        }
        private States currentState;
        public ZoomTool()
        {
            currentState = States.none;
            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Zoom",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }
        public ZoomTool(UIElement toolView) : this()
        {
            ToolView = toolView;
        }

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            if (currentState != States.stretching)
            {
                var viewPort = graphData.viewPorts.Last();
                point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
                point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

                rectangle = new Rectangle(point, Colors.DarkOrange, PenPicker.GetPen(typeof(SolidPen)), Colors.Transparent, BrushPicker.GetBrush(typeof(LinesBrush)), 2, viewPort.Scale);
                graphData.drawables.AddLast(rectangle);
                currentState = States.stretching;
            }
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            if (rectangle == null)
            {
                return;
            }
            graphData.drawables.RemoveLast();
            if (rectangle.firstCoord.X == rectangle.secondCoord.X && rectangle.firstCoord.Y == rectangle.secondCoord.Y)
            {
                ViewPort newViewPort = CalculateViewPort(rectangle.firstCoord, graphData);
                graphData.viewPorts.Add(newViewPort);
            }
            else
            {
                Point firstDrawingCoord = new Point();
                Point secondDrawingCoord = new Point();

                firstDrawingCoord.X = Math.Min(rectangle.firstCoord.X, rectangle.secondCoord.X);
                firstDrawingCoord.Y = Math.Min(rectangle.firstCoord.Y, rectangle.secondCoord.Y);
                secondDrawingCoord.X = Math.Max(rectangle.firstCoord.X, rectangle.secondCoord.X);
                secondDrawingCoord.Y = Math.Max(rectangle.firstCoord.Y, rectangle.secondCoord.Y);

                ViewPort newViewPort = CalculateViewPort(firstDrawingCoord, secondDrawingCoord, graphData);
                graphData.viewPorts.Add(newViewPort);
            }
            graphData.canvas.Render(graphData.drawables, graphData.viewPorts.Last());
            rectangle = null;
            currentState = States.none;
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (rectangle == null)
            {
                return;
            }
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            rectangle.ChangeLastPoint(point);
            Update(graphData.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count > 1 && rectangle != null)
            {
                drawables.Last.Value = rectangle;
            }
        }

        private ViewPort CalculateViewPort(Point point, GraphData graphData)
        {
            ViewPort lastViewPort = graphData.viewPorts.Last();
            ViewPort firstViewPort = graphData.viewPorts.First();

            var newWidth = (firstViewPort.secondPoint.X - firstViewPort.firstPoint.X) / (lastViewPort.Scale * 2);
            var newHeight = (firstViewPort.secondPoint.Y - firstViewPort.firstPoint.Y) / (lastViewPort.Scale * 2);

            var scale = CalculateScale(newWidth, newHeight, firstViewPort);
            Point fPoint = new Point();
            Point sPoint = new Point();

            fPoint.X = (point.X - (newWidth / 2)) < 0 ? 0 : point.X - (newWidth / 2);
            fPoint.Y = (point.Y - (newHeight / 2)) < 0 ? 0 : point.Y - (newHeight / 2);
            sPoint.X = point.X + (newWidth / 2);
            sPoint.Y = point.Y + (newHeight / 2);

            return new ViewPort(fPoint, sPoint, scale);
        }
        private double CalculateScale(double newWidth, double newHeight, ViewPort firstViewPort)
        {
            var scaleX = ((firstViewPort.secondPoint.X - firstViewPort.firstPoint.X) / newWidth) < 1
                ? 0.5 + ((firstViewPort.secondPoint.X - firstViewPort.firstPoint.X) / newWidth)
                : ((firstViewPort.secondPoint.X - firstViewPort.firstPoint.X) / newWidth);
            var scaleY = ((firstViewPort.secondPoint.Y - firstViewPort.firstPoint.Y) / newHeight) < 1
                ? 0.5 + ((firstViewPort.secondPoint.Y - firstViewPort.firstPoint.Y) / newHeight)
                : ((firstViewPort.secondPoint.Y - firstViewPort.firstPoint.Y) / newHeight);
            var scale = Math.Min(scaleX, scaleY) < 1 ? 0.5 + Math.Min(scaleX, scaleY) : Math.Min(scaleX, scaleY);
            return scale;
        }
        private ViewPort CalculateViewPort(Point firstPoint, Point secondPoint, GraphData graphData)
        {
            var newWidth = secondPoint.X - firstPoint.X;
            var newHeight = secondPoint.Y - firstPoint.Y;
            double scale = CalculateScale(newWidth, newHeight, graphData.viewPorts.First());

            return new ViewPort(firstPoint, secondPoint, scale);
        }
    }
}
