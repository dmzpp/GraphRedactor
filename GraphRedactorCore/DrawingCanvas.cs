using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore
{
    public class DrawingCanvas : FrameworkElement
    {
        private readonly VisualCollection collection;
        protected override Visual GetVisualChild(int index)
        {
            return collection[index];
        }
        protected override int VisualChildrenCount => collection.Count;

        public DrawingCanvas()
        {
            collection = new VisualCollection(this);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new System.Windows.Point(100,100), true, false);
                geometryContext.LineTo(new System.Windows.Point(100,70), false, false);
                geometryContext.ArcTo(new System.Windows.Point(100, 130), new System.Windows.Size(30, 30), 720, false, SweepDirection.Clockwise, true, true);
                geometryContext.ArcTo(new System.Windows.Point(70, 100), new System.Windows.Size(30, 30), 720, false, SweepDirection.Clockwise, true, true);
                geometryContext.LineTo(new System.Windows.Point(100, 100), false, false);
            }
            drawingContext.DrawGeometry(new SolidColorBrush(Colors.Red), new System.Windows.Media.Pen(new SolidColorBrush(Colors.Yellow), 10), geometry);
            
          /*  geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new System.Windows.Point(100, 130), true, false);
                geometryContext.ArcTo(new System.Windows.Point(70, 100), new System.Windows.Size(30, 30), 0, false, SweepDirection.Clockwise, true, true);
                geometryContext.LineTo(new System.Windows.Point(100, 100), false, false);
            }
            drawingContext.DrawGeometry(new SolidColorBrush(Colors.Red), new System.Windows.Media.Pen(new SolidColorBrush(Colors.Yellow), 10), geometry);*/
        }

        internal void Render(ICollection<IDrawable> drawables, ViewPort viewPort)
        {
            collection.Clear();
            DrawingVisual visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
                foreach (var drawable in drawables)
                {
                    drawable?.Draw(context, viewPort);
                }
            }
            collection.Add(visual);
        }
    }
}
