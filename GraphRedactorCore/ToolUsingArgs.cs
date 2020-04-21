using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

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
