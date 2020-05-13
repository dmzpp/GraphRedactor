using GraphRedactorCore.Figures;
using System;
using System.Collections.Generic;

namespace GraphRedactorCore
{
    public class GraphData
    {
        internal readonly ViewPortCollection viewPorts;
        internal readonly Dictionary<Type, LinkedList<IDrawable>> drawables;
        internal readonly DrawingCanvas canvas;

        public GraphData(int windowWidth, int windowHeight, DrawingCanvas drawingCanvas)
        {
            viewPorts = new ViewPortCollection
            {
                new ViewPort(windowWidth, windowHeight),
                new ViewPort(windowWidth, windowHeight)
            };
            canvas = drawingCanvas;
            drawables = new Dictionary<Type, LinkedList<IDrawable>>();

            drawables.Add(typeof(Ellipse), new LinkedList<IDrawable>());
            drawables.Add(typeof(Rectangle), new LinkedList<IDrawable>());
            drawables.Add(typeof(Pie), new LinkedList<IDrawable>());
            drawables.Add(typeof(PolyLine), new LinkedList<IDrawable>());

        }
    }
}
