using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphRedactorCore.Pens
{
    internal class DashPen : ICustomPen
    {
        public Pen GetPen(ViewPort viewPort, Color color, double width)
        {
            var pen = new Pen(new SolidColorBrush(color), width);
            pen.DashStyle = DashStyles.Dash;
            return pen;
        }

        public Pen GetPen(Color color, double width)
        {
            var pen = new Pen(new SolidColorBrush(color), width);
            pen.DashStyle = DashStyles.Dash;
            return pen;
        }
    }
}
