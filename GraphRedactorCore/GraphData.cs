using GraphRedactorCore.Tools.Animations;
using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GraphRedactorCore
{
    public class GraphData
    {
        internal readonly ViewPortCollection viewPorts;
        internal DrawableCollection drawables;
        internal readonly DrawingCanvas canvas;
        internal DispatcherTimer timer;
        internal List<Animation> animations;

        public GraphData(int windowWidth, int windowHeight, DrawingCanvas drawingCanvas)
        {
            viewPorts = new ViewPortCollection
            {
                new ViewPort(windowWidth, windowHeight),
                new ViewPort(windowWidth, windowHeight)
            };
            canvas = drawingCanvas;
            drawables = new DrawableCollection();
           // drawables.Change += () => canvas.Render(drawables.collection, viewPorts.Last());
            timer = new DispatcherTimer
            {
                Interval = new System.TimeSpan(0, 0, 0, 0, 16)
            };
            timer.Start();
            timer.Tick += (o, e) =>
            {
                canvas.Render(drawables.collection, viewPorts.Last());
            };
            animations = new List<Animation>();
            timer.Tick += (o, e) => { foreach (var animation in animations) animation.Tick(); };
        }
    }
}
