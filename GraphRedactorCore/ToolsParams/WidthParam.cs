using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.ToolsParams
{
    public class WidthParam : ToolParam
    {
        public double Value { get; set; }
        public WidthParam(double width)
        {
            Value = width;
        }
    }
}
