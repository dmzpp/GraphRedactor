using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore
{
    internal abstract class DrawableElement : IDrawable
    {
        public int ZIndex { get => _zIndex; set => _zIndex = value; }
        public double RotateAngle { get => _rotateAngle; set => _rotateAngle = value % 360; }
        public double Scale { get => _scale; set => _scale = value; }
        public double OffsetX { get => _offsetX; set => _offsetX = value; }
        public double OffsetY { get => _offsetY; set => _offsetY = value; }
        public abstract void Draw(DrawingContext context, ViewPort veiwPort);
        public abstract bool IsIntersect(Rect area);
        protected int _zIndex;
        protected double _scale;
        protected double _rotateAngle;
        protected double _offsetX;
        protected double _offsetY;
    }
}
