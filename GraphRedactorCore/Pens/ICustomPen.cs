using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Pens
{
    internal interface ICustomPen
    {
        Pen GetPen(ViewPort viewPort, Color color, double width);
        Pen GetPen(Color color, double width);
    }
}
