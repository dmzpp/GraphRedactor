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

        internal void Render(IEnumerable<DrawableElement> drawables, ViewPort viewPort)
        {
            collection.Clear();
            foreach(var drawable in drawables)
            {
                var visual = new DrawingVisual();

                using (var context = visual.RenderOpen())
                {
                    drawable.Draw(context, viewPort);
                }
                collection.Add(visual);
            }
        }
    }
}
