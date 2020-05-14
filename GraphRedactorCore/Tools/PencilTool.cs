using GraphRedactorCore.Figures;
using GraphRedactorCore.Pens;
using GraphRedactorCore.ToolsParams;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class PencilTool : Tool
    {
        private PolyLine polyLine = null;

        private BorderColorParam _contour;
        private WidthParam _width;

        public BorderColorParam Contour { get => _contour; set => _contour = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        public PencilTool()
        {
            _contour = new BorderColorParam(Colors.Red, typeof(SolidPen));
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

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            polyLine = new PolyLine(point, Contour.Color, Contour.PenType, Width.Value, graphData.viewPorts.Last().Scale, graphData.drawables.Count + 1);
            graphData.drawables.AddLast(polyLine);
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            Update(graphData.drawables);
            polyLine = null;
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            if (polyLine == null)
            {
                return;
            }
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            polyLine.AddPoint(point);
            Update(graphData.drawables);
        }

        private void Update(LinkedList<DrawableElement> drawables)
        {
            if (drawables.Count > 1 && polyLine != null)
            {
                drawables.Last.Value = polyLine;
            }
        }
    }
}
