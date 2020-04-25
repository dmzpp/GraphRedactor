using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    internal static class BitmapExtension
    {
        public static void Render(this WriteableBitmap bitmap, ICollection<IDrawable> drawables)
        {
            bitmap.Clear();
            foreach (IDrawable drawable in drawables)
            {
                drawable.Draw(bitmap);
            }
        }
    }
}
