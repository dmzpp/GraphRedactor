using System.Windows;

namespace GraphRedactorCore
{
    public class GraphRedactor
    {
        private readonly GraphData graphData;
        public ToolPicker ToolPicker { get; }
        private States currentState;
        private enum States
        {
            creating,
            editing,
            nothing
        }

        public GraphRedactor(int width, int height, DrawingCanvas drawingCanvas)
        {
            graphData = new GraphData(width, height, drawingCanvas);
            ToolPicker = new ToolPicker();
            currentState = States.nothing;
        }

        public void Render()
        {
            graphData.canvas.Render(graphData.drawables, graphData.viewPorts.Last());
        }

        public void PreviousScale()
        {
            graphData.viewPorts.RemoveLast();
        }

        public void Resize(double width, double height)
        {
            graphData.viewPorts.First().secondPoint.X = width;
            graphData.viewPorts.First().secondPoint.Y = height;
        }
        
        public void MouseMove(Point point)
        {
            ToolPicker.GetTool().MouseMove(point, graphData);
        }

        public void MouseLeftButtonUp(Point point)
        {
            ToolPicker.GetTool().MouseLeftButtonUp(point, graphData);
        }
        
        public void MouseRightButtonUp(Point point)
        {
            ToolPicker.GetTool().MouseRightButtonUp(point, graphData);
        }

        public void MouseLeftButtonDown(Point point)
        {
            ToolPicker.GetTool().MouseLeftButtonDown(point, graphData);
        }
    }
}
