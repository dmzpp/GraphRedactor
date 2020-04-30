using System.Collections;
using System.Collections.Generic;

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
            return viewPorts.First.Value;
        }

        public ViewPort Previous()
        {
            if (viewPorts.Count == 1)
            {
                return viewPorts.First.Value;
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
            if (Count > 1)
            {
                viewPorts.RemoveLast();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return viewPorts.GetEnumerator();
        }
    }
}
