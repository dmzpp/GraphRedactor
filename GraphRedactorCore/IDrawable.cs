using System.Windows.Media.Imaging;
namespace GraphRedactorCore
{
    public interface IDrawable
    {
        void Draw(WriteableBitmap bitmap);
    }
}
