using System.Collections.Generic;
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
           // drawingContext.DrawRectangle(new SolidColorBrush(Colors.Red), new Pen(new SolidColorBrush(Colors.Blue), 10), new Rect(10, 10, 100, 100));
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
