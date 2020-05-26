using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphRedactorCore.ToolsParams.AnimateToolParams
{
    public class MovingParam : ToolParam
    {
        public double Value { get; set; }

        public MovingParam(double width)
        {
            Value = width;

            var slider = new Slider() { Value = width, Maximum = 200, Minimum = 0, Width = 80, Height = 20, Margin = new Thickness(10) };
            slider.ValueChanged += Slider_ValueChanged;
            var textBlock = new TextBlock() { Text = "Возвратно-поступательное движение" };
            ArgView = new StackPanel()
            {
                Children = {
                    slider, textBlock
                },
                Margin = new Thickness(5)
            };
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Value = e.NewValue;
        }
    }
}
