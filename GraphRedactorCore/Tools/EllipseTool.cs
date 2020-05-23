using GraphRedactorCore.Brushes;
using GraphRedactorCore.Figures;
using GraphRedactorCore.Pens;
using GraphRedactorCore.ToolsParams;
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
            _fillColor = new FillColorParam(Colors.Black, typeof(LinesBrush));
            _borderColor = new BorderColorParam(Colors.Yellow, typeof(SolidPen));
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

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            ellipse = new Ellipse(point, BorderColor.Color, BorderColor.PenType, FillColor.Color,
                FillColor.BrushType, Width.Value, graphData.viewPorts.Last().Scale, graphData.drawables.Count + 1);
            graphData.drawables.AddLast(ellipse);
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            Update(graphData);
            ellipse = null;
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (ellipse == null)
            {
                return;
            }
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            ellipse.ChangeLastPoint(point);
            Update(graphData);
        }

        private void Update(GraphData graphData)
        {
            if (graphData.drawables.Count == 0 || ellipse == null)
            {
                return;
            }
            graphData.drawables.Last.Value = ellipse;
            graphData.canvas.Render(graphData.drawables, graphData.viewPorts.Last());
        }
    }
}
