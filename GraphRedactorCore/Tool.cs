using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
namespace GraphRedactorCore
{
    public abstract class Tool 
    {
        public Color ContourColor { get; set; }
        public Color FillColor { get; set; }
        public int Width { get; set; }
        /// <summary>
        /// Начинает использование выбранного инструмента
        /// </summary>
        /// <param name="point">Точка, в которой инструмент начал использоваться</param>
        public abstract IDrawable Use(Point point, ToolParams toolParams);

        /// <summary>
        /// Окончание использования инструмента
        /// </summary>
        /// <param name="point">Точка, в которой инструмент завершил работу</param>
        public abstract bool StopUsing(Point point, ToolParams toolParams, bool isCompletlyFinish = true);

    }

}
