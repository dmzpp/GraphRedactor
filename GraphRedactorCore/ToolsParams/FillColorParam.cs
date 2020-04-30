﻿using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace GraphRedactorCore.ToolsParams
{
    public class FillColorParam : ToolParam
    {
        public Color Color { get; set; }
        public FillColorParam(Color color)
        {
            Color = color;
            ArgView = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            (ArgView as ColorPicker).SelectedColorChanged += FillColorParam_SelectedColorChanged;
        }

        private void FillColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
        }
    }
}
