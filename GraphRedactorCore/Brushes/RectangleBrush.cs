using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace GraphRedactorCore.Brushes
{
    internal class RectangleBrush : ICustomBrush
    {
        public Brush GetBrush(ViewPort viewPort, Color color)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(new LineGeometry(new Point(10, 0), new Point(0, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 10), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(0, 0, 10 * viewPort.Scale, 10 * viewPort.Scale);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.FlipY;

            return brush;
        }

        public Brush GetBrush(Color color)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(new LineGeometry(new Point(10, 0), new Point(0, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 10), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(0, 0, 10, 10);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.FlipY;

            return brush;
        }
    }
}
