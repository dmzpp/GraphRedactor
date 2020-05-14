using System.Windows.Media;

namespace GraphRedactorCore
{
    internal abstract class DrawableElement : IDrawable
    {
        public int ZIndex { get => _zIndex; set => _zIndex = value; }
        protected int _zIndex;
        public abstract void Draw(DrawingContext context, ViewPort veiwPort);
    }
}
