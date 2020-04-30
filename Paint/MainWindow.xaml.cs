using System.Windows;
using System.Windows.Input;
using GraphRedactorCore;
using GraphRedactorCore.Tools;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private readonly GraphRedactor redactor;
        public MainWindow()
        {
            InitializeComponent();
            ViewBox.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Canvas_MouseLeftButtonDown), true);
            redactor = new GraphRedactor(800, 400, Canvas);
        }

        private void RenderCanvas()
        {
            redactor.Render();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            /*if (e.ClickCount >= 2 && redactor.ToolPicker.CurrentType() == ToolPicker.Tools.CurveLine)
            {
                redactor.StopUsingTool(mouseCoords);
            }
            else
            {
                redactor.StartUsingSelectedTool(mouseCoords);
            }*/
            redactor.StartUsingSelectedTool(mouseCoords);
            RenderCanvas();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            redactor.UseSelectedTool(mouseCoords);
            RenderCanvas();
        }
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Point mouseCoords = e.GetPosition(ViewBox);

            /*if (redactor.ToolPicker.CurrentToolType == ToolPicker.Tools.CurveLine)
            {
                redactor.ChangeToolPhase(mouseCoords);
            }
            else
            {
                redactor.StopUsingTool(mouseCoords);
            }
            if (redactor.ToolPicker.CurrentToolType != ToolPicker.Tools.Zoom)
                RenderCanvas();*/

            redactor.StopUsingTool(mouseCoords);
            if(redactor.ToolPicker.CurrentType() != typeof(ZoomTool))
            {
                RenderCanvas();
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redactor.Resize(e.NewSize.Width, e.NewSize.Height);
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            redactor.PreviousScale();
            RenderCanvas();
        }

        private void PenButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(PencilTool));
            redactor.RenderToolArgs(ToolArgs);
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(RectangleTool));
            redactor.RenderToolArgs(ToolArgs);
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(EllipseTool));
            redactor.RenderToolArgs(ToolArgs);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(ZoomTool));
            redactor.RenderToolArgs(ToolArgs);
        }

        private void HandButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(HandTool));
            redactor.RenderToolArgs(ToolArgs);
        }

        private void CurveLineButton_Click(object sender, RoutedEventArgs e)
        {
            //redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.CurveLine;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            redactor.ToolPicker.SetTool(typeof(LineTool));
            redactor.RenderToolArgs(ToolArgs);
        }
    }
}
