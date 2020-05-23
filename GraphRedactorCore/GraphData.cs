using System.Collections.Generic;

namespace GraphRedactorCore
{
    public class GraphData
    {
        internal readonly ViewPortCollection viewPorts;
        internal DrawableCollection drawables;
        internal readonly DrawingCanvas canvas;

        public GraphData(int windowWidth, int windowHeight, DrawingCanvas drawingCanvas)
        {
            viewPorts = new ViewPortCollection
            {
                new ViewPort(windowWidth, windowHeight),
                new ViewPort(windowWidth, windowHeight)
            };
            canvas = drawingCanvas;
            drawables = new DrawableCollection();
            drawables.Change += () => canvas.Render(drawables.collection, viewPorts.Last());
        }
    }
}
