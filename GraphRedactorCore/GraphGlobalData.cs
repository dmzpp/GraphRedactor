using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

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
