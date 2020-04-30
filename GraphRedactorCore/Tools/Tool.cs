using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools
{
    public abstract class Tool
    {
        public UIElement ToolView { get; set; }
        public abstract void Use(Point point,GraphData graphData);
        public abstract void StartUsing(Point point, GraphData graphData);
        public abstract void NextPhase(Point point, GraphData graphData);
        public abstract void StopUsing(Point point, GraphData graphData);

    }
}
