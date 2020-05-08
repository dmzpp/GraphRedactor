using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace GraphRedactorCore
{
    internal class ViewPortCollection : IEnumerable<ViewPort>
    {
        private readonly LinkedList<ViewPort> viewPorts;

        public int Count => viewPorts.Count;

        public ViewPortCollection()
        {
            viewPorts = new LinkedList<ViewPort>();
        }
        public void Add(ViewPort item)
        {
            viewPorts.AddLast(item);
        }
        public ViewPort Last()
        {
            return viewPorts.Last.Value;
        }
        public ViewPort First()
        {
            return viewPorts.First.Next.Value;
        }

        public ViewPort Previous()
        {
            if (viewPorts.Count == 1)
            {
                return viewPorts.Last.Value;
            }
            else
            {
                return viewPorts.Last.Previous.Value;
            }
        }

        public IEnumerator<ViewPort> GetEnumerator()
        {
            return viewPorts.GetEnumerator();
        }

        public void RemoveLast()
        {
            if (Count > 2)
            {
                viewPorts.RemoveLast();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return viewPorts.GetEnumerator();
        }

        public Point ConvertToBaseViewPort(Point point)
        {
            var viewPort = Last();
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);
            return point;
        }
        public Point ConvertToBaseViewPort(Point point, ViewPort viewPort)
        {
            point.X = viewPort.firstPoint.X + (point.X / viewPort.Scale);
            point.Y = viewPort.firstPoint.Y + (point.Y / viewPort.Scale);
            return point;
        }
    }
}
