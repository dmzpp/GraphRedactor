﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using GraphRedactorCore.Figures;
using System.Windows.Media;
using GraphRedactorCore.ToolsParams;
using System.Windows.Controls;

namespace GraphRedactorCore.Tools
{
    public class RectangleTool : ITool
    {
        private Rectangle rectangle = null;
        private FillColorParam _fillColor;
        private BorderColorParam _borderColor;
        private WidthParam _width;

        public FillColorParam FillColor { get => _fillColor; set => _fillColor = value; }
        public BorderColorParam BorderColor { get => _borderColor; set => _borderColor = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        public RectangleTool()
        {
            _fillColor = new FillColorParam(Colors.Black);
            _borderColor = new BorderColorParam(Colors.Yellow);
            _width = new WidthParam(10);
        }

        public void NextPhase(Point point, GraphData graphData)
        {
            throw new NotImplementedException("Данная фигура не поддерживает подобной функции");
        }

        public void StartUsing(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            rectangle = new Rectangle(point, BorderColor.Color, FillColor.Color, Width.Value, viewPort.Scale);
            graphData.drawables.AddLast(rectangle);
        }

        public void StopUsing(Point point, GraphData graphData)
        {
            Update(graphData.drawables); 
            rectangle = null;
        }

        public void Use(Point point, GraphData data)
        {
            if (rectangle == null)
            {
                throw new NullReferenceException("Работа инструмента не начата");
            }
            var viewPort = data.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            rectangle.ChangeLastPoint(point);
            Update(data.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count == 0)
            {
                return;
            }
            drawables.RemoveLast();
            drawables.AddLast(rectangle);
        }
    }
}
