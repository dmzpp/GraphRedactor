using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
namespace GraphRedactorCore
{
    public abstract class Tool 
    {
        private Color contourColor;
        private Color fillColor;

        /// <summary>
        /// Начинает использование выбранного инструмента
        /// </summary>
        /// <param name="point">Точка, в которой инструмент начал использоваться</param>
        public abstract IDrawable Use(Point point);

        /// <summary>
        /// Окончание использования инструмента
        /// </summary>
        /// <param name="point">Точка, в которой инструмент завершил работу</param>
        public abstract void StopUsing(Point point);
    }

}
