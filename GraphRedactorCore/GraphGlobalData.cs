using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    internal class GraphGlobalData
    {
        internal LinkedList<IDrawable> Drawables { get; set; }
        internal WriteableBitmap Bitmap { get; set; }
        private readonly LinkedList<ViewPort> previousViewPorts;
        internal ViewPort ViewPort { get => previousViewPorts.Last.Value; }
        internal ViewPort FirstViewPort { get => previousViewPorts.First.Value; }

        public GraphGlobalData(WriteableBitmap bitmap)
        {
            Drawables = new LinkedList<IDrawable>();
            Bitmap = bitmap;
            previousViewPorts = new LinkedList<ViewPort>();
            previousViewPorts.AddLast(new ViewPort(bitmap, this));
        }

        internal ViewPort PopViewPort()
        {
            GraphRedactorCore.ViewPort viewPort = previousViewPorts.Last.Value;
            if (previousViewPorts.Count > 1)
            {
                previousViewPorts.RemoveLast();
            }
            return viewPort;
        }

        internal void PushViewPort(ViewPort viewPort)
        {
            previousViewPorts.AddLast(viewPort);
        }
    }
}
