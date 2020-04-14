using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GraphRedactorCore.Figures;

namespace GraphRedactorCore.Instruments
{
    internal class Pencil : Tool
    {
        private Polyline drawableArea = null;
        public override void StopUsing(Point point)
        {
            drawableArea = null;
        }
        
        public override IDrawable Use(Point point)
        {
            (drawableArea ?? (drawableArea = new Polyline(point,Colors.Red, 1, true))).AddPoint(point);
            return drawableArea;
        }
    }
}
