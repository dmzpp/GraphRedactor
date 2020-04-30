﻿using GraphRedactorCore.Figures;
using GraphRedactorCore.ToolsParams;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class EllipseTool : Tool
    {
        private Ellipse ellipse = null;
        private FillColorParam _fillColor;
        private BorderColorParam _borderColor;
        private WidthParam _width;

        public FillColorParam FillColor { get => _fillColor; set => _fillColor = value; }
        public BorderColorParam BorderColor { get => _borderColor; set => _borderColor = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        public EllipseTool()
        {
            _fillColor = new FillColorParam(Colors.Black);
            _borderColor = new BorderColorParam(Colors.Yellow);
            _width = new WidthParam(10);
            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Ellipse",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }

        public EllipseTool(UIElement toolView) : this()
        {
            ToolView = toolView;
        }

        public override void NextPhase(Point point, GraphData graphData)
        {
            throw new NotImplementedException("Данная фигура не поддерживает подобной функции");
        }

        public override void StartUsing(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            ellipse = new Ellipse(point, BorderColor.Color, FillColor.Color, Width.Value, viewPort.Scale);
            graphData.drawables.AddLast(ellipse);
        }

        public override void StopUsing(Point point, GraphData graphData)
        {
            Update(graphData.drawables);
            ellipse = null;
        }

        public override void Use(Point point, GraphData data)
        {
            if (ellipse == null)
            {
                throw new NullReferenceException("Работа инструмента не начата");
            }
            var viewPort = data.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            ellipse.ChangeLastPoint(point);
            Update(data.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count == 0 || ellipse == null)
            {
                return;
            }
            drawables.RemoveLast();
            drawables.AddLast(ellipse);
        }
    }
}
