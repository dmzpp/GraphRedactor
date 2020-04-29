using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace GraphRedactorCore.ToolsParams
{
    public class FillColorParam : ToolParam
    {
        public Color Color { get; set; }
        public FillColorParam(Color color)
        {
            Color = color;
        }
    }
}
