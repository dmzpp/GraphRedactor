using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools
{
    public interface ITool
    {
        void Use(Point point,GraphData graphData);
        void StartUsing(Point point, GraphData graphData);
        void NextPhase(Point point, GraphData graphData);
        void StopUsing(Point point, GraphData graphData);
    }
}
