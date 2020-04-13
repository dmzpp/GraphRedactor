using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GraphRedactorCore;
namespace Paint
{
    public partial class MainWindow : Window
    {

        GraphRedactor redactor;
        public MainWindow()
        {
            redactor = new GraphRedactor(new WriteableBitmap(800, 400, 95, 95, PixelFormats.Bgra32, null));
            InitializeComponent();
            RenderCanvas();
        }
        private void RenderCanvas()
        {
            canvas.Source = redactor.CurrentBitmap;
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas.AddHandler(Image.MouseMoveEvent, new MouseEventHandler(canvas_MouseMove));
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.UseTool(mouseCoords);
            redactor.Render();
            RenderCanvas();
        }
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.StopUsingTool(mouseCoords);
            redactor.Render();
            RenderCanvas();
            canvas.RemoveHandler(Image.MouseMoveEvent, new MouseEventHandler(canvas_MouseMove));
        }
        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(Tools.Pencil);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var bitmap = (canvas.Source as WriteableBitmap).Resize((int)canvas.ActualWidth, (int)canvas.ActualHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
            redactor.CurrentBitmap = bitmap;
            redactor.Render();
            RenderCanvas();
        }


        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void FigurePlacerButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(Tools.FigurePlacer);
        }
    }

}
