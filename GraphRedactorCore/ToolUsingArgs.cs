using System.Windows;

namespace GraphRedactorCore
{
    internal class ToolUsingArgs
    {
        public ToolUsingArgs(Point point, ToolsArgs toolsArgs, GraphGlobalData graphGlobalData)
        {
            Point = point;
            ToolsArgs = toolsArgs;
            GraphGlobalData = graphGlobalData;
        }

        public Point Point { get; set; }
        public ToolsArgs ToolsArgs { get; set; }
        public GraphGlobalData GraphGlobalData { get; set; }
    }
}
