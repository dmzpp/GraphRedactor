using GraphRedactorCore.Figures;
using System;
using System.Windows;

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

        /// <summary>
        /// Завершает использование инструмента, если выбранный инструмент не является кривой линией.
        /// В случае прямой линии происходит переход на новую линию, если параметр isCompletelyFinish равен false.
        /// </summary>
        /// <param name="point">Точка, в которой происходит остановка работы</param>
        /// <param name="toolParams">Установленные параметры для инструментов</param>
        /// <param name="isCompletelyFinish">Указывает, нужно ли прекратить работу инструмента, несмотря ни на какие другие условия</param>
        /// <returns>Указывает на то, было ли произведено полное прекращение работы инструмента</returns>
        public override bool StopUsing(Point point, ToolParams toolParams, bool isCompletelyFinish = true)
        {
            if (toolParams.CurrentLineType == Lines.CurveLine && !isCompletelyFinish)
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

        /// <summary>
        /// Получает новый экземпляр выбранной линии
        /// </summary>
        /// <param name="initializePoint">Точка, в которой происходит инициализация линии</param>
        /// <param name="toolParams">Установленные параметры для инструментов</param>
        /// <returns>Экземпляр выбранной линии</returns>
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
