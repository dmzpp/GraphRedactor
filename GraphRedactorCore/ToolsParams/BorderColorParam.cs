using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphRedactorCore.ToolsParams
{
    public class BorderColorParam : ToolParam
    {
        public Color Color { get; set; }
        public BorderColorParam(Color color)
        {
            Color = color;
        }
    }
}
