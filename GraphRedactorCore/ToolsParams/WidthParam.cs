using System.Windows;
using System.Windows.Controls;

namespace GraphRedactorCore.ToolsParams
{
    public class WidthParam : ToolParam
    {
        public double Value { get; set; }

        public WidthParam(double width)
        {
            Value = width;
            ArgView = new Slider() { Value = width, Maximum = 50, Minimum = 1, Width = 80, Height = 20, Margin = new Thickness(10) };
            (ArgView as Slider).ValueChanged += WidthParam_ValueChanged;
        }

        private void WidthParam_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Value = e.NewValue;
        }
    }
}
