using System.Windows.Media;

namespace GraphRedactorCore
{
    internal interface IDrawable
    {
        void Draw(DrawingContext context, ViewPort veiwPort);
    }
}
