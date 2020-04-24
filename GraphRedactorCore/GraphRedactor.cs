using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    public class GraphRedactor
    {
        public ToolPicker ToolPicker { get; }
        public ToolsArgs ToolsArgs { get; }
        public WriteableBitmap Bitmap { get => globalData.Bitmap; set => globalData.Bitmap = value; }
        private readonly GraphGlobalData globalData;

        private States currentState;
        private enum States
        {
            creating,
            editing
        }

        public GraphRedactor(WriteableBitmap bitmap)
        {
            ToolPicker = new ToolPicker();
            ToolsArgs = new ToolsArgs();
            globalData = new GraphGlobalData(bitmap);
            currentState = States.creating;
        }

        public void StartUsingSelectedTool(Point point)
        {
            ToolPicker.CurrentTool.StartUsing(new ToolUsingArgs(point, ToolsArgs, globalData));
            currentState = States.editing; 
        }
        public void UseSelectedTool(Point point)
        {
            if (currentState == States.editing)
            {
                ToolPicker.CurrentTool.Use(new ToolUsingArgs(point, ToolsArgs, globalData));
            }
        }

        public void ChangeToolPhase(Point point)
        {
            ToolPicker.CurrentTool.NextPhase(new ToolUsingArgs(point, ToolsArgs, globalData));
        }

        public void StopUsingTool(Point point)
        {
            ToolPicker.CurrentTool.StopUsing(new ToolUsingArgs(point, ToolsArgs, globalData));
            currentState = States.creating;
        }

        public void Render()
        {
            Bitmap.Clear();
            foreach(var draweable in globalData.Drawables)
            {
                draweable?.Draw(Bitmap);
            }
        }

        public void DefaultScale()
        {
            globalData.ViewPort.Scale = 1;
            globalData.ViewPort.firstPoint.X = 0;
            globalData.ViewPort.firstPoint.Y = 0;
            globalData.ViewPort.secondPoint.X = globalData.Bitmap.Width;
            globalData.ViewPort.secondPoint.Y = globalData.Bitmap.Height;
            Render();
        }
    }
}
