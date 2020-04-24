using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    internal class GraphGlobalData
    {
        public LinkedList<IDrawable> Drawables { get; set; }
        public WriteableBitmap Bitmap { get; set; }
        public ViewPort ViewPort { get; set; }
        public GraphGlobalData(WriteableBitmap bitmap)
        {
            Drawables = new LinkedList<IDrawable>();
            Bitmap = bitmap;
            ViewPort = new ViewPort(bitmap);
        }
    }

}
