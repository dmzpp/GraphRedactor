using GraphRedactorCore.Tools;
using GraphRedactorCore.ToolsParams;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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
            ToolPicker.AddTool(new RectangleTool());
            ToolPicker.AddTool(new ZoomTool());
            ToolPicker.AddTool(new EllipseTool());
            ToolPicker.AddTool(new PencilTool());
            ToolPicker.AddTool(new HandTool());
            currentState = States.nothing;
        }


        public void StartUsingSelectedTool(Point point)
        {
            if (currentState == States.nothing)
            {
                ToolPicker.GetTool().StartUsing(point, graphData);
                currentState = States.editing;
            }
        }
        public void UseSelectedTool(Point point)
        {
            if (currentState == States.editing)
            {
                ToolPicker.GetTool().Use(point, graphData);
            }
        }

        public void ChangeToolPhase(Point point)
        {
            ToolPicker.GetTool().NextPhase(point, graphData);
        }

        public void StopUsingTool(Point point)
        {
            ToolPicker.GetTool().StopUsing(point, graphData);
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
        public void SetToolArg(ToolParam arg)
        {
            var properties = ToolPicker.CurrentType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == arg.GetType())
                {
                    property.SetValue(ToolPicker.GetTool(), arg);
                }
            }
        }

        public void RenderToolArgs(Panel panel)
        {
            var toolArgs = ToolPicker.CurrentType().GetProperties();
            panel.Children.Clear();
            foreach (var arg in toolArgs)
            {
                if (arg.PropertyType.IsSubclassOf(typeof(ToolParam)))
                {
                    var view = ((arg.GetValue(ToolPicker.GetTool())) as ToolParam).ArgView;
                    if (view != null)
                    {
                        panel.Children.Add(view);
                    }
                }
            }
        }
    }
}
