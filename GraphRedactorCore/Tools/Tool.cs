using System.Windows;

namespace GraphRedactorCore.Tools
{
    public abstract class Tool
    {
        public UIElement ToolView { get; set; }
        public virtual void MouseMove(Point point, GraphData graphData) { }
        public virtual void MouseRightButtonUp(Point point, GraphData graphData) { }
        public virtual void MouseRightButtonDown(Point point, GraphData graphData) { }
        public virtual void MouseLeftButtonUp(Point point, GraphData graphData) { }
        public virtual void MouseLeftButtonDown(Point point, GraphData graphData) { }
        public virtual void MouseLeftButtonDoubleClick(Point point, GraphData graphData) { }
    }
}
