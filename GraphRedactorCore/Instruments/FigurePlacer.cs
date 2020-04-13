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
        private Figures currentType;
        private Figure currentFigure = null;
        private int width;

        /// <summary>
        /// Возможные для создания фигуры
        /// </summary>
        public enum Figures
        {
            Rectangle
        }
        
        /// <summary>
        /// Устанавливает тип фигуры, которая в дальнейшем будет создана
        /// </summary>
        public void SetFigureType(Figures figureType)
        {
            currentType = figureType;
        }

        public FigurePlacer()
        {
            SetFigureType(Figures.Rectangle);
            // ВРЕМЕННО 
            width = 1;
            contourColor = Colors.Red;
            fillColor = Colors.Orange;
            // ВРЕМЕННО
        }

        public override void StopUsing(Point point)
        {
            currentFigure = null;
        }

        public override IDrawable Use(Point point)
        {
            (currentFigure ?? (currentFigure = GetFigureInstance(point))).AddPoint(point);
            return currentFigure;
        }

        private Figure GetFigureInstance(Point initializePoint)
        {
            switch (currentType)
            {
                case Figures.Rectangle:
                    return new Rectangle(initializePoint, fillColor, contourColor, width);
                default:
                    throw new ApplicationException("Undefined behaviour of application");
            }
        }
    }
}
