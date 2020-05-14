using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    internal interface ICustomBrush
    {
        Brush GetBrush(Color color, ViewPort viewPort, double scale, Point firstPoint, Point secondPoint, double opacity = 1);
        Brush GetBrush(Color color);
    }
}
