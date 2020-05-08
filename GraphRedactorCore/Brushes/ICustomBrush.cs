using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    internal interface ICustomBrush
    {
        Brush GetBrush(Color color, double Scale, double opacity = 1);
        Brush GetBrush(Color color);
    }
}
