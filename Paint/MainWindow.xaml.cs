using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using GraphRedactorCore;
using GraphRedactorCore.Tools;
using GraphRedactorCore.ToolsParams;

namespace Paint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GraphRedactor redactor;
        private void SetDefault()
        {
           /* FillColorPicker.SelectedColor = redactor.ToolsArgs.FirstColor;
            ConturColorPicker.SelectedColor = redactor.ToolsArgs.SecondColor;
            WidthSlider.Value = redactor.ToolsArgs.Width;
            SliderValueArea.Text = Convert.ToString(redactor.ToolsArgs.Width);*/
        }

        public MainWindow()
        {
            InitializeComponent();
            ViewBox.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Canvas_MouseLeftButtonDown), true);
            redactor = new GraphRedactor(800, 400, Canvas);
            //WidthSlider.ValueChanged += WidthSlider_ValueChanged;
           // RenderToolsArgs();
        }
       /* private void RenderToolsArgs()
        {
            var toolArgs = redactor.ToolPicker.CurrentType().GetProperties();
            ToolArgs.Children.Clear();
            foreach (var arg in toolArgs)
            {
                if (arg.PropertyType.IsSubclassOf(typeof(ToolParam)))
                {
                    var view = ((arg.GetValue(redactor.ToolPicker.GetTool())) as ToolParam).ArgView;
                    if(view != null)
                    {
                        ToolArgs.Children.Add(view);
                    }
                }
            }
            
        }*/

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
           // SliderValueArea.Text = $"{mouseCoords.X} {mouseCoords.Y}";
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

       /* private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redactor.SetToolArg(new WidthParam(e.NewValue));
        }

        private void ConturColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.SetToolArg(new BorderColorParam((Color)e.NewValue));
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            redactor.SetToolArg(new FillColorParam((Color)e.NewValue));
        }*/

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
            //redactor.ToolPicker.CurrentToolType = ToolPicker.Tools.Line;
            // redactor.RenderToolArgs(ToolArgs);
        }
    }
}
