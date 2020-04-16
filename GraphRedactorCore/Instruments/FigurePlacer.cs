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
    public class FigurePlacer : Tool
    {
        private Figure currentFigure = null;

        /// <summary>
        /// Возможные для создания фигуры
        /// </summary>
        public enum Figures
        {
            Rectangle,
            Ellipse,
        }
        
        /// <summary>
        /// Устанавливает тип фигуры, которая в дальнейшем будет создана
        /// </summary>

        public override bool StopUsing(Point point, ToolParams toolParams, bool isCompletlyFinish = true)
        {
            if (isCompletlyFinish)
            {
                currentFigure = null;
            }
            else
            {

            }
            return true;
        }

        public override IDrawable Use(Point point, ToolParams toolParams)
        {
            (currentFigure ?? (currentFigure = GetFigureInstance(point, toolParams))).AddPoint(point);
            return currentFigure;
        }

        private Figure GetFigureInstance(Point initializePoint, ToolParams toolParams)
        {
            switch (toolParams.CurrentFigureType)
            {
                case Figures.Rectangle:
                    return new Rectangle(initializePoint, toolParams.FillColor, toolParams.ContourColor, toolParams.Width);
                case Figures.Ellipse:
                    return new Ellipse(initializePoint, toolParams.ContourColor, toolParams.FillColor, toolParams.Width);
                default:
                    throw new ApplicationException("Undefined behaviour of application");
            }
        }
    }
}
