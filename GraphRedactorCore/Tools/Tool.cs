using System.Windows;

namespace GraphRedactorCore.Tools
{
    public abstract class Tool
    {
        public UIElement ToolView { get; set; }
        public abstract void Use(Point point, GraphData graphData);
        public abstract void StartUsing(Point point, GraphData graphData);
        public abstract void NextPhase(Point point, GraphData graphData);
        public abstract void StopUsing(Point point, GraphData graphData);

    }
}
