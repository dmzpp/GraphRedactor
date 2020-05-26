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
                using(var context = visual.RenderOpen())
                {
                    drawable.Draw(context, viewPort);
                }
                collection.Add(visual);
            }
        }


        private bool isAdditionaAdd = false;
        internal void RenderAdditionalElement(DrawableElement drawable, ViewPort viewPort)
        {
            if (isAdditionaAdd)
            {
                RemoveLast();
            }
            DrawingVisual visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
               drawable.Draw(context, viewPort);
            }
            collection.Add(visual);
            isAdditionaAdd = true;
        }

        internal void RemoveLast()
        {
            if (collection.Count > 0)
            {
                collection.RemoveAt(collection.Count - 1);
            }
        }

        internal IEnumerable<DrawingVisual> SelectElements(Rect area)
        {
            List<DrawingVisual> selectedElements = new List<DrawingVisual>();
            foreach(var drawable in collection)
            {
                var intersect = Rect.Intersect(area, ((DrawingVisual)drawable).ContentBounds);
                if(intersect != Rect.Empty)
                {
                    selectedElements.Add(drawable as DrawingVisual);
                }
            }
            return selectedElements;
        }
    }
}
