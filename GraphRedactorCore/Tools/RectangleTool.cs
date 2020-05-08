using GraphRedactorCore.Figures;
using GraphRedactorCore.ToolsParams;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GraphRedactorCore.Brushes;
using System.Windows.Media;
using GraphRedactorCore.Pens;

namespace GraphRedactorCore.Tools
{
    public class RectangleTool : Tool
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
            _fillColor = new FillColorParam(Colors.Black, typeof(LinesBrush));
            _borderColor = new BorderColorParam(Colors.Yellow, typeof(SolidPen));
            _width = new WidthParam(10);

            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Rectangle",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
        }

        public RectangleTool(UIElement toolView) : this()
        {
            ToolView = toolView;
        }

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            rectangle = new Rectangle(point, BorderColor.Color, PenPicker.GetPen(BorderColor.PenType), FillColor.Color, BrushPicker.GetBrush(FillColor.BrushType), Width.Value, viewPort.Scale);
            graphData.drawables.AddLast(rectangle);
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            Update(graphData.drawables);
            rectangle = null;
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
            if (drawables.Count == 0 || rectangle == null)
            {
                return;
            }
            drawables.RemoveLast();
            drawables.AddLast(rectangle);
        }
    }
}
