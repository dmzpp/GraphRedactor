using System.Windows.Media;
using GraphRedactorCore.Figures;
using GraphRedactorCore.Instruments;

namespace GraphRedactorCore
{
    /// <summary>
    /// Используется для установки и выдачи различных параметров, необходимых для инструментов
    /// </summary>
    public class ToolParams
    {
        public ToolParams(Color contourColor, Color fillColor, int width, FigurePlacer.Figures currentFigureType, LinePlacer.Lines currentLineType)
        {
            ContourColor = contourColor;
            FillColor = fillColor;
            Width = width;
            CurrentFigureType = currentFigureType;
            CurrentLineType = currentLineType;
        }

        public Color ContourColor { get; set; }
        public Color FillColor { get; set; }
        public int Width { get; set; }
        public Instruments.FigurePlacer.Figures CurrentFigureType { get; set; }
        public Instruments.LinePlacer.Lines CurrentLineType { get; set; }



    }
}
