using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore
{
    public class GraphData
    {
        internal readonly ViewPortCollection viewPorts;
        internal readonly LinkedList<IDrawable> drawables;
        internal readonly DrawingCanvas canvas;

        public GraphData(int windowWidth, int windowHeight, DrawingCanvas drawingCanvas)
        {
            viewPorts = new ViewPortCollection();
            viewPorts.Add(new ViewPort(windowWidth, windowHeight));
            drawables = new LinkedList<IDrawable>();
            canvas = drawingCanvas;
        }
    }
}
