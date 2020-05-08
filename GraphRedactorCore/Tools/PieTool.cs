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
    public class PieTool : Tool
    {
        private Ellipse ellipse = null;
        private Pie pie = null;
        private FillColorParam _fillColor;
        private BorderColorParam _borderColor;
        private WidthParam _width;

        public FillColorParam FillColor { get => _fillColor; set => _fillColor = value; }
        public BorderColorParam BorderColor { get => _borderColor; set => _borderColor = value; }
        public WidthParam Width { get => _width; set => _width = value; }

        private enum States
        {
            drawingEllipse,
            drawingPie,
            nothing
        }
        private States currentState;

        public PieTool()
        {
            _fillColor = new FillColorParam(Colors.Black, typeof(SolidBrush));
            _borderColor = new BorderColorParam(Colors.Yellow, typeof(SolidPen));
            _width = new WidthParam(10);
            ToolView = new Button()
            {
                Width = 60,
                Height = 30,
                Content = "Pie",
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Colors.White)
            };
            currentState = States.nothing;
        }

        public PieTool(UIElement toolView) : this()
        {
            ToolView = toolView;
        }

        public override void MouseLeftButtonDown(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            if (currentState == States.nothing)
            {
                currentState = States.drawingEllipse;

                ellipse = new Ellipse(point, BorderColor.Color, PenPicker.GetPen(BorderColor.PenType), FillColor.Color,
                    BrushPicker.GetBrush(FillColor.BrushType), Width.Value, viewPort.Scale);
                graphData.drawables.AddLast(ellipse);
            }
            else if (currentState == States.drawingPie)
            {
                ellipse.opacity = 0.2;
                graphData.drawables.Last.Value = ellipse;
                Size radiuses = new Size()
                {
                    Width = ellipse.diameters.X / 2 / viewPort.Scale,
                    Height = ellipse.diameters.Y / 2 / viewPort.Scale
                };

                var centerPoint = Point.Subtract(ellipse.secondDrawingCoord, ellipse.diameters / 2);
                centerPoint = graphData.viewPorts.ConvertToBaseViewPort(centerPoint, viewPort);
                if(ellipse.diameters.X == 0 && ellipse.diameters.Y == 0)
                {
                    currentState = States.nothing;
                    return;
                }
                var intersactionPoint = MathExtension.CalculateIntersectionPoint(ellipse.diameters / viewPort.Scale, centerPoint, point);


                pie = new Pie(centerPoint, intersactionPoint,
                    BorderColor.Color, PenPicker.GetPen(BorderColor.PenType),
                    FillColor.Color, BrushPicker.GetBrush(FillColor.BrushType),
                    Width.Value, radiuses, viewPort.Scale);
                graphData.drawables.AddLast(pie);
                Update(graphData.drawables);
            }
        }

        public override void MouseLeftButtonUp(Point point, GraphData graphData)
        {
            if (currentState == States.drawingEllipse)
            {
                currentState = States.drawingPie;
            }
            else if (currentState == States.drawingPie)
            {
                graphData.drawables.Remove(graphData.drawables.Last.Previous);
                pie = null;
                currentState = States.nothing;
            }
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point = graphData.viewPorts.ConvertToBaseViewPort(point);

            if (currentState == States.drawingEllipse)
            {
                if (ellipse == null)
                {
                    return;
                }

                ellipse.ChangeLastPoint(point);
            }
            else if (currentState == States.drawingPie && pie != null)
            {
                var centerPoint = Point.Subtract(ellipse.secondDrawingCoord, ellipse.diameters / 2);
                centerPoint = graphData.viewPorts.ConvertToBaseViewPort(centerPoint);

                if (ellipse.diameters.X == 0 && ellipse.diameters.Y == 0)
                {
                    currentState = States.nothing;
                    return;
                }

                Size radiuses = new Size()
                {
                    Width = ellipse.diameters.X / graphData.viewPorts.Last().Scale,
                    Height = ellipse.diameters.Y / graphData.viewPorts.Last().Scale
                };

                var intersactionPoint = MathExtension.CalculateIntersectionPoint(ellipse.diameters / graphData.viewPorts.Last().Scale, centerPoint, point);
                pie.ChangeLastPoint(intersactionPoint);
            }
            Update(graphData.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count == 0)
            {
                return;
            }
            if (currentState == States.drawingEllipse && ellipse != null)
            {
                drawables.Last.Value = ellipse;
            }
            else if (currentState == States.drawingPie && pie != null)
            {
                drawables.Last.Value = pie;
            }
        }
    }
}
