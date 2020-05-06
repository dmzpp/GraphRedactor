using System.Collections.Generic;

namespace GraphRedactorCore
{
    public class GraphData
    {
        internal readonly ViewPortCollection viewPorts;
        internal readonly LinkedList<IDrawable> drawables;
        internal readonly DrawingCanvas canvas;

        public GraphData(int windowWidth, int windowHeight, DrawingCanvas drawingCanvas)
        {
            viewPorts = new ViewPortCollection
            {
                new ViewPort(windowWidth, windowHeight),
                new ViewPort(windowWidth, windowHeight)
            };
            drawables = new LinkedList<IDrawable>();
            canvas = drawingCanvas;
        }
    }
}
