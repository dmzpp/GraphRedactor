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
                geometryContext.ArcTo(new System.Windows.Point(100, 200), new System.Windows.Size(50, 50), 720, true, SweepDirection.Clockwise, true, false);
                geometryContext.LineTo(new System.Windows.Point(75, 100), false, false);
            }
            drawingContext.DrawGeometry(new SolidColorBrush(Colors.Red), new System.Windows.Media.Pen(new SolidColorBrush(Colors.Yellow), 10), geometry);
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
