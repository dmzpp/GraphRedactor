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
            _fillColor = new FillColorParam(Colors.Black, typeof(SolidBrush));
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
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            rectangle = new Rectangle(point, BorderColor.Color,
                BorderColor.PenType, FillColor.Color,
                FillColor.BrushType, Width.Value, graphData.drawables.Count + 1, graphData.viewPorts.Last().Scale);

            graphData.drawables.AddLast(rectangle);
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            Update(graphData);
            rectangle = null;
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (rectangle == null)
            {
                return;
            }
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            rectangle.ChangeLastPoint(point);
            Update(graphData);
        }

        private void Update(GraphData data)
        {
            if (data.drawables.Count == 0 || rectangle == null)
            {
                return;
            }
            data.drawables.Last.Value = rectangle;
            data.canvas.Render(data.drawables.collection, data.viewPorts.Last());
        }
    }
}
