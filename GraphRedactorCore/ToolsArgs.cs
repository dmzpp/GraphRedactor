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
