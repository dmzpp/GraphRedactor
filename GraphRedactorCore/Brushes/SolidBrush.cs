using System.Windows.Media;
using System.Windows;

namespace GraphRedactorCore.Brushes
{
    internal class SolidBrush : ICustomBrush
    {
        public Brush GetBrush(Color color, ViewPort viewPort, double scale, Point firstPoint, Point secondPoint, double opacity = 1)
        {
            var brush = new SolidColorBrush(color)
            {
                Opacity = opacity
            };
            return brush;
        }

        public Brush GetBrush(Color color)
        {
            var brush = new SolidColorBrush(color);
            return brush;
        }
    }
}
