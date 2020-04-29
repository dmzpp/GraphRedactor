using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GraphRedactorCore;
using GraphRedactorCore.Tools;

namespace GraphRedactorCore.Tools
{
    public class HandTool : ITool
    {
        private Point coursorPoint;

        public void NextPhase(Point point, GraphData graphData)
        {
            throw new NotImplementedException();
        }

        public void StartUsing(Point point, GraphData graphData)
        {
            var viewPort = graphData.viewPorts.Previous();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            coursorPoint = point;
        }

        public void StopUsing(Point point, GraphData graphData)
        {
            return;
        }

        public void Use(Point point, GraphData graphData)
        {
            
            if (graphData.viewPorts.Last() == graphData.viewPorts.First())
            {
                return;
            }

            var viewPort = graphData.viewPorts.Previous();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);

            var distance = Point.Subtract(coursorPoint, point);

            viewPort = graphData.viewPorts.Last();
            viewPort.firstPoint.X += distance.X;
            viewPort.firstPoint.Y += distance.Y;
            viewPort.secondPoint.X += distance.X;
            viewPort.secondPoint.Y += distance.Y;
            coursorPoint = point;
        }
    }
}
