using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    internal interface ICustomBrush
    {
        Brush GetBrush(ViewPort viewPort, Color color);
        Brush GetBrush(Color color);
    }
}
