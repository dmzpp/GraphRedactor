using GraphRedactorCore.Pens;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace GraphRedactorCore.ToolsParams
{
    public class BorderColorParam : ToolParam
    {
        public Color Color { get; set; }
        public Type PenType { get; set; }
        private readonly ComboBox _pensList;
        public BorderColorParam(Color color, Type customPenType)
        {
            _pensList = new ComboBox();
            PenType = customPenType;
            Color = color;
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            var colorPicker = new ColorPicker() { SelectedColor = color, Width = 50, Height = 50, Margin = new Thickness(10) };
            colorPicker.SelectedColorChanged += BorderColorParam_SelectedColorChanged;
            stackPanel.Children.Add(colorPicker);

            _pensList.Width = 150;
            _pensList.Height = 40;

            RenderPens();

            stackPanel.Children.Add(_pensList);
            ArgView = stackPanel;
        }

        private void RenderPens()
        {
            _pensList.Items.Clear();

            foreach (var pen in PenPicker.pens)
            {
                ComboBoxItem item = new ComboBoxItem();
                var brush = pen.Value.GetPen(Color, 1);
                Line line = new Line()
                {
                    Stroke = brush.Brush,
                    StrokeThickness = 10,
                    X1 = 20,
                    X2 = 130,
                    Y1 = 20,
                    Y2 = 20,
                    StrokeDashArray = brush.DashStyle.Dashes
                };
                item.Tag = pen.Key;
                if (pen.Key == PenType)
                {
                    item.IsSelected = true;
                }
                item.Content = line;
                item.Selected += Item_Selected;
                _pensList.Items.Add(item);
            }
        }
        private void Item_Selected(object sender, RoutedEventArgs e)
        {
            PenType = (((ComboBoxItem)sender).Tag as Type);
        }

        private void BorderColorParam_SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color = (Color)e.NewValue;
            RenderPens();
        }
    }
}
