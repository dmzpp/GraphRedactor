using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace GraphRedactorCore
{
    public abstract class Figure : IDrawable
    {
        protected int width;
        protected Color fillColor;
        protected Color contourColor;
        public abstract void Draw(WriteableBitmap bitmap);
    }
}
