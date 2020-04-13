using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GraphRedactorCore.Instruments;

namespace GraphRedactorCore
{

    public enum Tools
    {
        Pencil
    }
    public class ToolPicker
    {
        private Pencil pencil;
        private Pencil Pencil
        {
            get {
                if (pencil == null)
                {
                    pencil = new Pencil();
                }
                return pencil;
            }
            set
            {
                pencil = value;
            }
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
