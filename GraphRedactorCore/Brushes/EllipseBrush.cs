using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraphRedactorCore.Brushes
{
    class EllipseBrush : ICustomBrush
    {
        public Brush GetBrush(ViewPort viewPort, Color color)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(5, 5)));
            myGeometryGroup.Children.Add(new LineGeometry(new Point(5, 5), new Point(10, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 3), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(0, 0, 10 * viewPort.Scale, 10 * viewPort.Scale);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.Tile;
            brush.Stretch = Stretch.UniformToFill;
            brush.Transform = new RotateTransform(75);

            return brush;
        }

        public Brush GetBrush(Color color)
        {
            var brush = new DrawingBrush();
            GeometryGroup myGeometryGroup = new GeometryGroup();
            //myGeometryGroup.Children.Add(new LineGeometry(new Point(0, 0), new Point(10, 10)));
            myGeometryGroup.Children.Add(new EllipseGeometry(new Rect(0, 0, 10, 10)));

            GeometryDrawing myDrawing = new GeometryDrawing(null, new Pen(new SolidColorBrush(color), 3), myGeometryGroup);

            brush.Drawing = myDrawing;

            brush.Viewbox = new Rect(0, 0, 10, 10);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            brush.Viewport = new Rect(0, 0, 10, 10);
            brush.ViewportUnits = BrushMappingMode.Absolute;
            brush.TileMode = TileMode.Tile;
            brush.Stretch = Stretch.UniformToFill;
            brush.Transform = new RotateTransform(75);

            return brush;
        }
    }
}
