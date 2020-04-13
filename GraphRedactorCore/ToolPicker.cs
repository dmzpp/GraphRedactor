﻿using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GraphRedactorCore.Instruments;

namespace GraphRedactorCore
{

    public enum Tools
    {
        Pencil,
        FigurePlacer
    }
    public class ToolPicker
    {
        private Pencil pencil;
        private Pencil Pencil
        {
            get => pencil ?? (pencil = new Pencil());
            set => pencil = value;
        }

        private FigurePlacer figurePlacer;
        private FigurePlacer FigurePlacer
        {
            get => figurePlacer ?? (figurePlacer = new FigurePlacer());
            set => figurePlacer = value;
        }

        private Tools toolType;

        public Tool CurrentTool
        {
            get
            {
                switch (toolType)
                {
                    case Tools.Pencil:
                        return Pencil;
                    case Tools.FigurePlacer:
                        return FigurePlacer;
                    default:
                        throw new Exception("Tool type is not selected");
                }
            }
        }


        /// <summary>
        /// Устанавливает выбранный инструмент, который в дальнейшем может быть использован
        /// </summary>
        /// <param name="tool">Выбранный инструмент</param>
        public void SetTool(Tools tool)
        {
            toolType = tool;
        }
    }
}
