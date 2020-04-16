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
            HidePanels(Tools.Pencil);
            FillColorPicker.SelectedColor = Colors.AliceBlue;
            ConturColorPicker.SelectedColor = Colors.Red;
        }

        public MainWindow()
        {
            redactor = new GraphRedactor(new WriteableBitmap(800, 400, 95, 95, PixelFormats.Bgra32, null));
            InitializeComponent();
            RenderCanvas();
            SetDefault();
            WidthSlider.ValueChanged += WidthSlider_ValueChanged;
        }
        private void RenderCanvas()
        {
            canvas.Source = redactor.CurrentBitmap;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            if (e.ClickCount == 1)
            {
                redactor.InitializeTool(mouseCoords);
            }
            else
            {
                redactor.StopUsingTool(mouseCoords, true);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            if (redactor.UseTool(mouseCoords))
            {
                redactor.Render();
                RenderCanvas();
            }
        }
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.StopUsingTool(mouseCoords,
                redactor.ToolPicker.ToolType != Tools.LinePlacer || redactor.ToolParams.CurrentLineType != LinePlacer.Lines.CurveLine);
            redactor.Render();
            RenderCanvas();
        }
        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(Tools.Pencil);
            HidePanels(Tools.Pencil);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var bitmap = (canvas.Source as WriteableBitmap).Resize((int)canvas.ActualWidth, (int)canvas.ActualHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
            redactor.CurrentBitmap = bitmap;
            redactor.Render();
            RenderCanvas();
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redactor.ToolParams.Width = (int)e.NewValue;
            SliderValueArea.Text = Convert.ToString((int)e.NewValue);
        }

        /// <summary>
        /// Скрывает все дополнительные панельки для инструментов, за исключением панелькки для инструмента tool
        /// </summary>
        /// <param name="tool">Инструмент, для которого необходимо сохранить дополнительную панель</param>
        private void HidePanels(GraphRedactorCore.Tools tool = Tools.Pencil)
        {
            if (tool == Tools.Pencil)
            {
                LinesTypes.Visibility = Visibility.Hidden;
                FigureTypes.Visibility = Visibility.Hidden;
            }
            else if (tool == Tools.LinePlacer)
            {
                LinesTypes.Visibility = Visibility.Visible;
                FigureTypes.Visibility = Visibility.Hidden;
            }
            else if (tool == Tools.FigurePlacer)
            {
                FigureTypes.Visibility = Visibility.Visible;
                LinesTypes.Visibility = Visibility.Hidden;
            }
        }
        private void FigurePlacerButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(Tools.FigurePlacer);
            HidePanels(Tools.FigurePlacer);
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolParams.CurrentFigureType = FigurePlacer.Figures.Rectangle;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolParams.CurrentFigureType = FigurePlacer.Figures.Ellipse;
        }

        private void LinesButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(Tools.LinePlacer);
            HidePanels(Tools.LinePlacer);
        }

        private void SimpeLineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolParams.CurrentLineType = LinePlacer.Lines.SimpleLine;
        }

        private void CurveLineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolParams.CurrentLineType = LinePlacer.Lines.CurveLine;
        }

        private void ConturColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ToolParams.ContourColor = (Color)e.NewValue;
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ToolParams.FillColor = (Color)e.NewValue;
        }
    }
}
