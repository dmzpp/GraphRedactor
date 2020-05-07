using System;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls;
using GraphRedactorCore.Pens;
using System.Windows.Shapes;
using System.Collections;

namespace GraphRedactorCore.ToolsParams
{
    public class BorderColorParam : ToolParam
    {
        public Color Color { get; set; }
        public Type PenType { get; set; }
        public BorderColorParam(Color color, Type customPenType)
        {
            PenType = customPenType;
            Color = color;
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            var colorPicker = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            colorPicker.SelectedColorChanged += BorderColorParam_SelectedColorChanged;
            stackPanel.Children.Add(colorPicker);
            var comboBox = new ComboBox()
            {
                Width = 150,
                Height = 40
            };
            foreach (var pen in PenPicker.pens)
            {
                ComboBoxItem item = new ComboBoxItem();
                var brush = pen.Value.GetPen(Colors.Red, 1);
                Line line = new Line()
                {
                    Stroke = brush.Brush,
                    StrokeThickness = 10,
                    X1 = 20, X2 = 130,
                    Y1 = 20, Y2 = 20,
                    StrokeDashArray = brush.DashStyle.Dashes
                };
                item.Tag = pen.Key;
                item.Content = line;
                item.Selected += Item_Selected;
                comboBox.Items.Add(item);
            }
            stackPanel.Children.Add(comboBox);
            ArgView = stackPanel;
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            PenType = (((ComboBoxItem)sender).Tag as Type);
        }

        private void BorderColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
        }
    }
}
