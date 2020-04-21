using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore
{
    internal class GraphGlobalData
    {
        public LinkedList<IDrawable> Drawables { get; set; }

        public GraphGlobalData()
        {
            Drawables = new LinkedList<IDrawable>();
        }
    }
}
