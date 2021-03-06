﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class HandTool : Tool
    {
        private Point coursorPoint;
        private bool isMoving = false;

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (!isMoving)
            {
                return;
            }

            var viewPort = graphData.viewPorts.Previous();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            var distance = Point.Subtract(coursorPoint, point);
            viewPort = graphData.viewPorts.Last();
            viewPort.firstPoint = Point.Add(viewPort.firstPoint, distance);
            viewPort.secondPoint = Point.Add(viewPort.secondPoint, distance);
            coursorPoint = point;

            graphData.canvas.Render(graphData.drawables.collection, graphData.viewPorts.Last());
        }
        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Previous();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);
            coursorPoint = point;
            isMoving = true;
        }

        public HandTool()
        {
            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Hand",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }
        public HandTool(UIElement toolView)
        {
            ToolView = toolView;
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            isMoving = false;
        }

    }
}
