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

        internal void Render(LinkedList<DrawableElement> drawables, ViewPort viewPort)
        {
            collection.Clear();
            DrawingVisual visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
                foreach (var drawable in drawables)
                {
                    drawable.Draw(context, viewPort);
                }
            }
            collection.Add(visual);
        }
    }
}
