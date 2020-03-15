using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphRedactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GraphRedactorApplication redactor;
        public MainWindow()
        {
            InitializeComponent();
            redactor = new GraphRedactorApplication(new WriteableBitmap(660, 350, 96, 96, PixelFormats.Bgra32, null));
            RenderCanvas();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.CreateFigure((int)mouseCoords.X, (int)mouseCoords.Y, (int)mouseCoords.X, (int)mouseCoords.Y, Colors.Red);
            RenderCanvas();
        }
        private void RenderCanvas()
        {
            canvas.Source = redactor.RenderCanvas();
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.CurrentFigure = new Rectangle(); // enum PossibleFigures ?? 
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(canvas);
            redactor.StrechLastFigure((int)mouseCoords.X, (int)mouseCoords.Y);
            RenderCanvas();
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            redactor.FinishStretching();
            RenderCanvas();
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.CurrentFigure = new Line();
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.CurrentFigure = new Ellipse();
        }
    }

} 
