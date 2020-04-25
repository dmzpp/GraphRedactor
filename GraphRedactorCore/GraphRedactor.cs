using System.Windows;
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

        public void ResizeBitmap(int actualWidth, int actualHeight)
        {
            globalData.Bitmap = globalData.Bitmap.Resize(actualWidth, actualHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
            globalData.FirstViewPort.secondPoint.X = actualWidth;
            globalData.FirstViewPort.secondPoint.Y = actualHeight;
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
            Bitmap.Render(globalData.Drawables);
        }

        public void PreviousScale()
        {
            globalData.PopViewPort();
            Render();
        }
    }
}
