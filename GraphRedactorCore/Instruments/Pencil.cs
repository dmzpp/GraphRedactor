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
        public override bool StopUsing(Point point, ToolParams toolParams, bool isCompletlyFinish = true)
        {
            drawableArea = null;
            return true;
        }
        
        public override IDrawable Use(Point point, ToolParams toolParams)
        {
            (drawableArea ?? (drawableArea = new Polyline(point, toolParams.ContourColor, toolParams.Width))).AddPoint(point);
            return drawableArea;
        }
    }
}

