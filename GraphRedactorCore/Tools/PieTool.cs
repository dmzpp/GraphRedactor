using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphRedactorCore.Figures;
using GraphRedactorCore.Tools;
using GraphRedactorCore.ToolsParams;
using GraphRedactorCore.Pens;
using GraphRedactorCore.Brushes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

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
            _fillColor = new FillColorParam(Colors.Black, typeof(LinesBrush));
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
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            if (currentState == States.nothing)
            {
                currentState = States.drawingEllipse;

                ellipse = new Ellipse(point, BorderColor.Color, PenPicker.GetPen(BorderColor.PenType), FillColor.Color,
                    BrushPicker.GetBrush(FillColor.BrushType), Width.Value, viewPort.Scale);
                graphData.drawables.AddLast(ellipse);
            }
            else if(currentState == States.drawingPie)
            {
                ellipse.opacity = 0.2;
                graphData.drawables.Last.Value = ellipse;

                var centerPoint = Point.Subtract(ellipse.secondDrawingCoord, ellipse.diameters / 2);
                var intersactionPoint = CalculateIntersectionPoint(ellipse.diameters, centerPoint, point);
                Size radiuses = new Size()
                {
                    Width = ellipse.diameters.X / 2,
                    Height = ellipse.diameters.Y / 2
                };

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
            else if(currentState == States.drawingPie)
            {
                graphData.drawables.Remove(graphData.drawables.Last.Previous);
                currentState = States.nothing;
            }
        }

        public override void MouseMove(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            if (currentState == States.drawingEllipse)
            {
                if (ellipse == null)
                {
                    return;
                }

                ellipse.ChangeLastPoint(point);
            }
            else if(currentState == States.drawingPie)
            {
                var centerPoint = Point.Subtract(ellipse.secondDrawingCoord, ellipse.diameters / 2);
                var intersactionPoint = CalculateIntersectionPoint(ellipse.diameters, centerPoint, point);

                if (pie != null)
                {
                    pie.ChangeLastPoint(intersactionPoint);
                }
            }
            Update(graphData.drawables);
        }

        private void Update(LinkedList<IDrawable> drawables)
        {
            if (drawables.Count == 0 || ellipse == null)
            {
                return;
            }
            //drawables.RemoveLast();
            if(currentState == States.drawingEllipse)
            {
               // drawables.AddLast(ellipse);
                drawables.Last.Value = ellipse;
            }
            else if(currentState == States.drawingPie)
            {
                drawables.Last.Value = pie;
            }
        }

        /// <summary>
        /// Вычисление координат пересечения прямой и окружности
        /// </summary>
        /// <param name="radius">Радиус окружности</param>
        /// <param name="centerPoint">Центр окружности</param>
        /// <param name="firstPoint">Первая точка прямой</param>
        /// <param name="secondPoint">Вторая точка прямой</param>
        /// <returns></returns>
        private Point CalculateIntersectionPoint(Vector radiuses, Point centerPoint, Point point)
        {
            var newPoint = new Point()
            {
                X = point.X - centerPoint.X,
                Y = point.Y - centerPoint.Y
            };
            var points = FindEllipseSegmentIntersections(radiuses.X, radiuses.Y, new Point(0,0), newPoint, 0, 0);
            var firstPointSubstract = Point.Subtract(newPoint, points[0]);
            var secondPointSubstract = Point.Subtract(newPoint, points[1]);

            if(Math.Abs(firstPointSubstract.X) >= Math.Abs(secondPointSubstract.X) && Math.Abs(firstPointSubstract.Y) >= Math.Abs(secondPointSubstract.Y))
            {
                return new Point()
                {
                    X = points[1].X + centerPoint.X,
                    Y = points[1].Y + centerPoint.Y
                };
            }
            else
            {
                return new Point()
                {
                    X = points[0].X + centerPoint.X,
                    Y = points[0].Y + centerPoint.Y
                };
            }
        }
        private Point[] FindEllipseSegmentIntersections(double width, double height, Point pt1, Point pt2, double centerX, double centerY)
        {
            var cx = centerX;
            var cy = centerY;
            var a = width / 2;
            var b = height / 2;

            var A = (pt2.X - pt1.X) * (pt2.X - pt1.X) / a / a +
                      (pt2.Y - pt1.Y) * (pt2.Y - pt1.Y) / b / b;
            var B = 2 * pt1.X * (pt2.X - pt1.X) / a / a +
                      2 * pt1.Y * (pt2.Y - pt1.Y) / b / b;
            var C = pt1.X * pt1.X / a / a + pt1.Y * pt1.Y / b / b - 1;

            var t_values = new List<double>();

            var discriminant = Math.Abs(B * B - 4 * A * C);
            if (discriminant == 0)
            {
                t_values.Add(-B / 2 / A);
            }
            else if (discriminant > 0)
            {
                t_values.Add((float)((-B + Math.Sqrt(discriminant)) / 2 / A));
                t_values.Add((float)((-B - Math.Sqrt(discriminant)) / 2 / A));
            }
            List<Point> points = new List<Point>();
            foreach (float t in t_values)
            {
                var x = pt1.X + (pt2.X - pt1.X) * t + cx;
                var y = pt1.Y + (pt2.Y - pt1.Y) * t + cy;
                points.Add(new Point(x, y));
            }

            return points.ToArray();
        }
    }
}
