using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore
{
    public class GraphRedactor
    {
        public ToolPicker ToolPicker { get; private set; }
        public WriteableBitmap CurrentBitmap { get; set; }

        private LinkedList<IDrawable> drawables;
        private States currentState;
        private enum States
        {
            creating,
            editing
        }

        private void SetDefaultOptions()
        {
            ToolPicker.SetTool(Tools.Pencil);
            currentState = States.creating;
        }

        public GraphRedactor(WriteableBitmap bitmap)
        {
            CurrentBitmap = bitmap ?? throw new ArgumentNullException("Not initialized bitmap");
            ToolPicker = new ToolPicker();
            drawables = new LinkedList<IDrawable>();
            SetDefaultOptions();
        }


        /// <summary>
        /// Запустить процесс использования инструмента
        /// </summary>
        /// <param name="cursor">Точка, в которой произошёл запуск инструмента</param>
        public void UseTool(Point cursor)
        {

            if (currentState == States.creating)
            {
                currentState = States.editing;
            }
            else
            {
                drawables.RemoveLast();
            }
            drawables.AddLast(ToolPicker.CurrentTool.Use(cursor));
        }


        /// <summary>
        /// Отрисовывает все текущие элементы
        /// </summary>
        public void Render()
        {
            CurrentBitmap.Clear();
            foreach(IDrawable drawable in drawables)
            {
                drawable.Draw(CurrentBitmap);
            }
        }

        /// <summary>
        /// Остановить процесс использования инструмента
        /// </summary>
        /// <param name="cursor">Точка, в которой произошла остановка инструмента</param>
        public void StopUsingTool(Point cursor)
        {
            ToolPicker.CurrentTool.StopUsing(cursor);
            currentState = States.creating;
        }


    }
}
