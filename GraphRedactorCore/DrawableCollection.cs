using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphRedactorCore
{
    internal delegate void ChangeHandler();

    internal class DrawableCollection : IEnumerable<DrawableElement>
    {
        internal LinkedList<DrawableElement> collection = new LinkedList<DrawableElement>();
        public event ChangeHandler Change;
        public int Count { get => collection.Count; }
        public LinkedListNode<DrawableElement> Last { get => collection.Last; }

        public IEnumerator<DrawableElement> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return collection.GetEnumerator();
        }

        public void Clear()
        {
            collection.Clear();
            Change?.Invoke();
        }

        public void AddLast(DrawableElement drawable)
        {
            collection.AddLast(drawable);
            Change?.Invoke();
        }

        public void Remove(LinkedListNode<DrawableElement> node)
        {
            collection.Remove(node);
            Change?.Invoke();
        }

        public IEnumerable<DrawableElement> SelectElements(Rect area)
        {
            var list = new List<DrawableElement>();
            
            foreach(var drawable in collection)
            {
                if (drawable.IsIntersect(area))
                {
                    list.Add(drawable);
                }
            }

            return list;
        }
    }
}
