using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Figures
{
    public class Line : Figure
    {
        private Polyline polyline;

        public Line(Point initializeCoord, Color fillColor, Color contourColor, int width)
        {
            polyline = new Polyline(initializeCoord, contourColor, width);
            polyline.AddPoint(initializeCoord);
        }

        public override void AddPoint(Point point)
        {
            polyline.ChangeLastPoint(point, true);
        }

        public override void Draw(WriteableBitmap bitmap)
        {
            polyline.Draw(bitmap);
        }

    }
}
