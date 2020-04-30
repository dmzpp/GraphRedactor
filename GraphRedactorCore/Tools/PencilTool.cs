using GraphRedactorCore.Figures;
using GraphRedactorCore.ToolsParams;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class PencilTool : Tool
    {
        private PolyLine polyLine = null;

        private FillColorParam _fillColor;
        private WidthParam _width;

        public FillColorParam FillColor { get => _fillColor; set => _fillColor = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        public PencilTool()
        {
            _fillColor = new FillColorParam(Colors.Red);
            _width = new WidthParam(10);
            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Pencil",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }
        public PencilTool(UIElement toolView) : this()
        {
            ToolView = toolView;
        }

        public override void NextPhase(Point point, GraphData graphData)
        {
            throw new NotImplementedException("Этот инструмент пока не поддерживает это");
        }

        public override void StartUsing(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            polyLine = new PolyLine(point, FillColor.Color, Width.Value, viewPort.Scale);
            graphData.drawables.AddLast(polyLine);
        }

        public override void StopUsing(Point point, GraphData graphData)
        {
            Update(graphData.drawables);
            polyLine = null;
        }

        public override void Use(Point point, GraphData graphData)
        {
            if (polyLine == null)
            {
                return;
            }
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            polyLine.AddPoint(point);
            Update(graphData.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count > 1 && polyLine != null)
            {
                drawables.RemoveLast();
                drawables.AddLast(polyLine);
            }
        }
    }
}
