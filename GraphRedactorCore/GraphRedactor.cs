using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    public class GraphRedactor
    {
        public ToolPicker ToolPicker { get; }
        public ToolParams ToolParams { get; }
        public WriteableBitmap CurrentBitmap { get; set; }

        private readonly LinkedList<IDrawable> drawables;
        private States currentState;
        private enum States
        {
            creating,
            editing
        }

        /// <summary>
        /// Установка всех значений графического редатора по умолчанию
        /// </summary>
        private void SetDefaultOptions()
        {
            ToolPicker.SetTool(Tools.Pencil);
            currentState = States.creating;
        }

        public GraphRedactor(WriteableBitmap bitmap)
        {
            CurrentBitmap = bitmap ?? throw new ArgumentNullException("Not initialized bitmap");
            ToolPicker = new ToolPicker();
            ToolParams = new ToolParams(Colors.Black, Colors.Blue, 1, Instruments.FigurePlacer.Figures.Rectangle, Instruments.LinePlacer.Lines.SimpleLine);
            drawables = new LinkedList<IDrawable>();
            SetDefaultOptions();
        }

        public void InitializeTool(Point initializePoint)
        {
            if (currentState == States.creating)
            {
                currentState = States.editing;
                drawables.AddLast(ToolPicker.CurrentTool.Use(initializePoint, ToolParams));
            }
        }

        /// <summary>
        /// Запустить процесс использования инструмента
        /// </summary>
        /// <param name="cursor">Точка, в которой произошёл запуск инструмента</param>
        public bool UseTool(Point cursor)
        {
            if (currentState == States.editing)
            {
                drawables.RemoveLast();
                drawables.AddLast(ToolPicker.CurrentTool.Use(cursor, ToolParams));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Отрисовывает все текущие элементы
        /// </summary>
        public void Render()
        {
            CurrentBitmap.Clear();
            foreach (IDrawable drawable in drawables)
            {
                drawable.Draw(CurrentBitmap);
            }
        }

        /// <summary>
        /// Остановить процесс использования инструмента
        /// </summary>
        /// <param name="cursor">Точка, в которой произошла остановка инструмента</param>
        public void StopUsingTool(Point cursor, bool isCompletelyFinish = false)
        {
            if (ToolPicker.CurrentTool.StopUsing(cursor, ToolParams, isCompletelyFinish))
            {
                currentState = States.creating;
            }
        }
    }
}
