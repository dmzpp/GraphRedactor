using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace GraphRedactorCore.ToolsParams
{
    public class BorderColorParam : ToolParam
    {
        public Color Color { get; set; }
        public BorderColorParam(Color color)
        {
            Color = color;
            ArgView = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            (ArgView as ColorPicker).SelectedColorChanged += BorderColorParam_SelectedColorChanged;
        }

        private void BorderColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
        }
    }
}
