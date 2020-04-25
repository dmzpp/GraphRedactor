using System.Windows.Media.Imaging;
namespace GraphRedactorCore
{
    internal interface IDrawable
    {
        void Draw(WriteableBitmap bitmap);
    }
}
