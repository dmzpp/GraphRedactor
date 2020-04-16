using GraphRedactorCore.Figures;
using System;
using System.Windows;

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
        /// Прекращяет использование данного инструмента
        /// </summary>
        /// <param name="point">Точка, в которой происходит остановка работы</param>
        /// <param name="toolParams">Установленные параметры для инструментов</param>
        /// <param name="isCompletelyFinish">Указывает, нужно ли прекратить работу инструмента, несмотря ни на какие другие условия</param>
        /// <returns>Указывает на то, была ли работа инструмента прекращена полностью</returns>
        public override bool StopUsing(Point point, ToolParams toolParams, bool isCompletlyFinish = true)
        {
            if (isCompletlyFinish)
            {
                currentFigure = null;
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
