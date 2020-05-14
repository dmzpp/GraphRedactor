using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    internal class CrossBrush : ICustomBrush
    {
        public Brush GetBrush(Color color, ViewPort viewPort, double scale, Point firstPoint, Point secondPoint, double opacity = 1)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(new RectangleGeometry(new Rect(0, 0, 10, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 3), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(firstPoint.X, firstPoint.Y, 10 * viewPort.Scale / scale, 10 * viewPort.Scale / scale);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.FlipY;
            brush.Stretch = Stretch.Fill;
            brush.Transform = new RotateTransform(0);
            brush.Opacity = opacity;

            return brush;
        }

        public Brush GetBrush(Color color)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(new RectangleGeometry(new Rect(0, 0, 10, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 3), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(0, 0, 10, 10);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.Tile;
            brush.Stretch = Stretch.UniformToFill;
            brush.Transform = new RotateTransform(0);

            return brush;
        }
    }
}
