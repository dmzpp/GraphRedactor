using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using GraphRedactorCore.Brushes;
using System.Windows.Controls;
using System;
using System.Windows.Shapes;
using System.Windows.Automation.Peers;

namespace GraphRedactorCore.ToolsParams
{
    public class FillColorParam : ToolParam
    {
        public Color Color { get; set; }
        public Type BrushType { get; set; }
        public FillColorParam(Color color, Type custoumBrushType)
        {
            BrushType = custoumBrushType;
            Color = color;
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            var colorPicker = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            colorPicker.SelectedColorChanged += FillColorParam_SelectedColorChanged;
            stackPanel.Children.Add(colorPicker);
            var comboBox = new ComboBox();
            foreach(var brush in BrushPicker.brushes)
            {
                ComboBoxItem item = new ComboBoxItem();
                Rectangle rectangle = new Rectangle()
                {
                    Fill = brush.Value.GetBrush(Color),
                    Width = 100,
                    Height = 50
                };
                item.Tag = brush.Key;
                item.Content = rectangle;
                item.Selected += Item_Selected;
                comboBox.Items.Add(item);
            }
            stackPanel.Children.Add(comboBox);
            ArgView = stackPanel;
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            BrushType = (((ComboBoxItem)sender).Tag as Type);
        }

        private void FillColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
        }
    }
}
