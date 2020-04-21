using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphRedactorCore
{
    public class ToolsArgs
    {
        public Color FirstColor { get; set; }
        public Color SecondColor { get; set; }
        public int Width { get; set; }


        public ToolsArgs()
        {
            FirstColor = Colors.Black;
            SecondColor = Colors.Gray;
            Width = 10;
        }
    }
}
