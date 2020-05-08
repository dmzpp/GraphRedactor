using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    internal class SolidBrush : ICustomBrush
    {
        public Brush GetBrush(Color color, double scale, double opacity = 1)
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
