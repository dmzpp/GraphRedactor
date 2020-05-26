using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GraphRedactorCore.ToolsParams.AnimateToolParams
{
    public class ScalingParam : ToolParam
    {
        public double Value { get; set; }

        public ScalingParam(double width)
        {
            Value = width;

            var slider = new Slider() { Value = width, Maximum = 10, Minimum = 1, Width = 80, Height = 20, Margin = new Thickness(10) };
            slider.ValueChanged += Slider_ValueChanged;
            var textBlock = new TextBlock() { Text = "Масштабирование" };
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
