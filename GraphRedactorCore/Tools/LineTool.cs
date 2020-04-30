using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphRedactorCore.ToolsParams;
using GraphRedactorCore.Figures;
using System.Windows.Media;

namespace GraphRedactorCore.Tools
{
    public class LineTool : ITool
    {
        private PolyLine polyLine = null;

        private FillColorParam _fillColor;
        private WidthParam _width;

        public FillColorParam FillColor { get => _fillColor; set => _fillColor = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        public LineTool()
        {
            _fillColor = new FillColorParam(Colors.Blue);
            _width = new WidthParam(10);
        }

        public void NextPhase(Point point, GraphData graphData)
        {
            throw new NotImplementedException("Этот инструмент пока не поддерживает это");
        }

        public void StartUsing(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            polyLine = new PolyLine(point, FillColor.Color, Width.Value, viewPort.Scale);
            graphData.drawables.AddLast(polyLine);
        }

        public void StopUsing(Point point, GraphData graphData)
        {
            Update(graphData.drawables);
            polyLine = null;
        }

        public void Use(Point point, GraphData graphData)
        {
            if (polyLine == null)
            {
                return;
            }
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            polyLine.ChangeLastPoint(point);
            Update(graphData.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count > 1)
            {
                drawables.RemoveLast();
                drawables.AddLast(polyLine);
            }
        }
    }
}
