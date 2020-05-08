using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace GraphRedactorCore.Brushes
{
    internal class SolidBrush : ICustomBrush
    {
        public Brush GetBrush(Color color, double scale, double opacity = 1)
        {
            var brush = new SolidColorBrush(color);
            brush.Opacity = opacity;
            return brush;
        }

        public Brush GetBrush(Color color)
        {
            var brush = new SolidColorBrush(color);
            return brush;
        }
    }
}
