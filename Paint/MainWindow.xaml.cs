using GraphRedactorCore;
using GraphRedactorCore.Tools;
using System.Windows;
using System.Windows.Input;

namespace Paint
{
    public partial class MainWindow : Window
    {
        private readonly GraphRedactor _redactor;
        public MainWindow()
        {
            InitializeComponent();
            ViewBox.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Canvas_MouseLeftButtonDown), true);
            _redactor = new GraphRedactor(800, 400, Canvas);

            _redactor.ToolPicker.AddTool(new RectangleTool());
            _redactor.ToolPicker.AddTool(new ZoomTool());
            _redactor.ToolPicker.AddTool(new EllipseTool());
            _redactor.ToolPicker.AddTool(new PencilTool());
            _redactor.ToolPicker.AddTool(new HandTool());
            _redactor.ToolPicker.AddTool(new LineTool());

            _redactor.ToolPicker.RenderTools(Tools, ToolArgs);
        }

        private void RenderCanvas()
        {
            _redactor.Render();
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
            _redactor.StartUsingSelectedTool(mouseCoords);
            RenderCanvas();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            _redactor.UseSelectedTool(mouseCoords);
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

            _redactor.StopUsingTool(mouseCoords);
            if (_redactor.ToolPicker.CurrentType() != typeof(ZoomTool))
            {
                RenderCanvas();
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _redactor.Resize(e.NewSize.Width, e.NewSize.Height);
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _redactor.PreviousScale();
            RenderCanvas();
        }

    }
}
