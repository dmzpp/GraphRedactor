﻿using GraphRedactorCore;
using GraphRedactorCore.Tools;
using System.IO;
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
            _redactor.ToolPicker.AddTool(new PieTool());
            _redactor.ToolPicker.AddTool(new AnimateTool());

            _redactor.ToolPicker.RenderTools(Tools, ToolArgs);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            _redactor.MouseLeftButtonDown(mouseCoords);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            _redactor.MouseMove(mouseCoords);
        }
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mouseCoords = e.GetPosition(ViewBox);
            _redactor.MouseLeftButtonUp(mouseCoords);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _redactor.Resize(e.NewSize.Width, e.NewSize.Height);
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _redactor.PreviousScale();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "myimage";
            dlg.DefaultExt = ".yaml";
            dlg.Filter = "Yaml documents (.yaml)|*.yaml";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                _redactor.SaveFile(filename);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "myimage";
            dlg.DefaultExt = ".yaml";
            dlg.Filter = "Text documents (.yaml)|*.yaml";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                try
                {
                    _redactor.OpenFile(filename);
                }
                catch (FileFormatException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _redactor.ClearCanvas();
        }
    }
}
