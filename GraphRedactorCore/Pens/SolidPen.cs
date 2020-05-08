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
