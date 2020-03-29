using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GraphRedactorApp;

namespace GraphRedactor
{
    public partial class MainWindow : Window
    {
        GraphRedactorApplication redactor;
        public MainWindow()
        {
            InitializeComponent();
            redactor = new GraphRedactorApplication(new WriteableBitmap(800, 400, 125, 125, PixelFormats.Pbgra32, null));
            RenderCanvas();
        }
        private void RenderCanvas()
        {
            canvas.Source = redactor.RenderCanvas();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point mouseCoords = e.GetPosition(canvas);
            redactor.StartDrawing((int)mouseCoords.X, (int)mouseCoords.Y);
            RenderCanvas();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point mouseCoords = e.GetPosition(canvas);
            redactor.ChangeLastEntity((int)mouseCoords.X, (int)mouseCoords.Y);
            RenderCanvas();
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            redactor.FinishChanging();
            RenderCanvas();
        }

        /*
        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            redactor.FinishStretching();
            RenderCanvas();
        }
        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.SetCurrentFigure(PossibleFigures.Rectangle);
        }
        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.SetCurrentFigure(PossibleFigures.Line);
        }
        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.SetCurrentFigure(PossibleFigures.Ellipse);
        }
        private void DottedLine_Click(object sender, RoutedEventArgs e)
        {
            redactor.SetCurrentFigure(PossibleFigures.DottedLine);
        }
        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ChangeFillColor((Color)e.NewValue);
        }
        private void ConturColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.ChangeConturColor((Color)e.NewValue);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WriteableBitmap bitmap = (canvas.Source as WriteableBitmap).Resize((int)canvas.ActualWidth, (int)canvas.ActualHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
            redactor.BitmapUpdate(bitmap);
            RenderCanvas();
        }
    */
    }


} 
