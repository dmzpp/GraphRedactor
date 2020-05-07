using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphRedactorCore.Pens
{
    internal class SolidPen : ICustomPen
    {
        public Pen GetPen(ViewPort viewPort, Color color, double width)
        {
            return new Pen(new SolidColorBrush(color), width);
        }

        public Pen GetPen(Color color, double width)
        {
            return new Pen(new SolidColorBrush(color), width);
        }
    }
}
