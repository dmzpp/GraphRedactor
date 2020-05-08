using GraphRedactorCore.Brushes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace GraphRedactorCore.ToolsParams
{
    public class FillColorParam : ToolParam
    {
        public Color Color { get; set; }
        public Type BrushType { get; set; }

        private ComboBox brushesList;
        public FillColorParam(Color color, Type custoumBrushType)
        {
            this.brushesList = new ComboBox();
            BrushType = custoumBrushType;
            Color = color;
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            var colorPicker = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            colorPicker.SelectedColorChanged += FillColorParam_SelectedColorChanged;
            stackPanel.Children.Add(colorPicker);

            this.brushesList.Width = 150;
            this.brushesList.Height = 50;
            RenderBrushes();

            stackPanel.Children.Add(brushesList);
            ArgView = stackPanel;
        }

        private void RenderBrushes()
        {
            brushesList.Items.Clear();

            foreach (var brush in BrushPicker.brushes)
            {
                ComboBoxItem item = new ComboBoxItem();
                Rectangle rectangle = new Rectangle()
                {
                    Fill = brush.Value.GetBrush(Color),
                    Width = 100,
                    Height = 40
                };
                if (brush.Key == BrushType)
                {
                    item.IsSelected = true;
                }
                item.Tag = brush.Key;
                item.Content = rectangle;
                item.Selected += Item_Selected;
                brushesList.Items.Add(item);
            }
        }

        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            BrushType = (((ComboBoxItem)sender).Tag as Type);
        }

        private void FillColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
            RenderBrushes();
        }
    }
}
