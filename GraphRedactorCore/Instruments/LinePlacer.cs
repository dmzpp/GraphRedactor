using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphRedactorCore.Figures;
using System.Windows.Media;

namespace GraphRedactorCore.Instruments
{
    public class LinePlacer : Tool
    {
        public enum Lines
        {
            SimpleLine,
            CurveLine
        }
        private Figure currentLine = null;

        public override bool StopUsing(Point point, ToolParams toolParams, bool isCompletelyFinish = true)
        {
            if(toolParams.CurrentLineType == Lines.CurveLine && !isCompletelyFinish)
            {
                (currentLine as CurveLine)?.NextLine(point);
                return false;
            }
            else
            {
                currentLine = null;
                return true;
            }
        }

        public override IDrawable Use(Point point, ToolParams toolParams)
        {
            (currentLine ?? (currentLine = GetLineInstance(point, toolParams))).AddPoint(point);
            return currentLine;
        }
        private Figure GetLineInstance(Point initializePoint, ToolParams toolParams)
        {
            switch (toolParams.CurrentLineType)
            {
                case Lines.SimpleLine:
                    return new Line(initializePoint, toolParams.FillColor, toolParams.ContourColor, toolParams.Width);
                case Lines.CurveLine:
                    return new CurveLine(initializePoint, toolParams.ContourColor, toolParams.FillColor, toolParams.Width);
                default:
                    throw new ApplicationException("Undefined behaviour of application");
            }
        }
    }
}
