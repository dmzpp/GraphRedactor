using GraphRedactorCore;
using GraphRedactorCore.Instruments;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Paint
{
    public partial class MainWindow : Window
    {
        private readonly GraphRedactor redactor;
        private void SetDefault()
        {
            FillColorPicker.SelectedColor = redactor.ToolsArgs.FirstColor;
            ConturColorPicker.SelectedColor = redactor.ToolsArgs.SecondColor;
        }

        public MainWindow()
        {
            redactor = new GraphRedactor(new WriteableBitmap(800, 400, 95, 95, PixelFormats.Bgra32, null));
            InitializeComponent();
            RenderCanvas();
            SetDefault();
            WidthSlider.ValueChanged += WidthSlider_ValueChanged;
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Rectangle;
        }
        private void RenderCanvas()
        {
            redactor.Render();
            canvas.Source = redactor.Bitmap;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            if (e.ClickCount >= 2 && redactor.ToolPicker.CurrentToolType == ToolPicker.Tools.CurveLine)
            {
                redactor.StopUsingTool(mouseCoords);
            }
            else
            {
                redactor.StartUsingSelectedTool(mouseCoords);
            }
            RenderCanvas();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.UseSelectedTool(mouseCoords);
            RenderCanvas();
        }
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);

            if (redactor.ToolPicker.CurrentToolType == ToolPicker.Tools.CurveLine)
            {
                redactor.ChangeToolPhase(mouseCoords);
            }
            else
            {
                redactor.StopUsingTool(mouseCoords);
            }
            RenderCanvas();
        }
        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.CurveLine;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var bitmap = (canvas.Source as WriteableBitmap).Resize((int)canvas.ActualWidth, (int)canvas.ActualHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
            redactor.Bitmap = bitmap;
            RenderCanvas();
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redactor.ToolsArgs.Width = (int)e.NewValue;
            SliderValueArea.Text = Convert.ToString((int)e.NewValue);
        }

        private void ConturColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ToolsArgs.FirstColor = (Color)e.NewValue;
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ToolsArgs.SecondColor = (Color)e.NewValue;
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.DefaultScale();
            RenderCanvas();
        }

        private void PenButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Pencil;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Rectangle;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Ellipse;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Zoom;
        }

        private void HandButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.HandTool;
        }

        private void CurveLineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.CurveLine;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Line;
        }
    }
}
