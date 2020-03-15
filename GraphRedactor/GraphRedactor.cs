using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphRedactor
{
    class GraphRedactorApplication
    {
        private WriteableBitmap canvas;
        public Figure CurrentFigure { get; set; }
        private Stack<Figure> figures;
        private enum States
        {
            stretching,
            positioning // выбор места для установки фигуры
        }
        private States currentState;
        public GraphRedactorApplication(WriteableBitmap canvas)
        {
            this.canvas = canvas;
            CurrentFigure = new Rectangle(); // установка фигуры по умолчанию
            figures = new Stack<Figure>();
            currentState = States.positioning;
        }
        public void CreateFigure(int x1, int y1, int x2, int y2, Color color)
        {
            if (currentState == States.positioning)
            {
                figures.Push(CurrentFigure.GetFigure(x1, y1, x2, y2, color));
                currentState = States.stretching;
            }
        }
        public void FinishStretching()
        {
            currentState = States.positioning;
        }
        public WriteableBitmap RenderCanvas()
        {
            canvas.Clear();
            foreach(var figure in figures)
            {
                figure.Draw(canvas);
            }
            return canvas;
        }

        public void StrechLastFigure(int x2, int y2)
        {
            if (currentState == States.stretching)
            {
                Figure lastFigure = figures.Peek();
                //lastFigure.x2 = x2;
                //lastFigure.y2 = y2;
                lastFigure.Stretch(x2, y2);
                
                figures.Push(lastFigure);
            }
        }
        
    }
    abstract class Figure
    {
        public abstract void Draw(WriteableBitmap canvas);
        public abstract Figure GetFigure(int x1, int y1, int x2, int y2, Color color);
        public int x1, x2, y1, y2;
        public Color color;
        public Figure(int x1, int y1, int x2, int y2, Color color)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2= x2;
            this.y2 = y2;
            this.color = color;
        }
        public Figure()
        {

        }
        public abstract void Stretch(int mouseX, int mouseY);
    }
    class Rectangle : Figure
    {
        public Rectangle()
        {

        }
        public Rectangle(int x1, int y1, int x2, int y2, Color color) 
            :base(x1, y1, x2, y2, color)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            int firstCoordX;
            int secondCoordX;
            int firstCoordY;
            int secondCoordY;
            if (x1 > x2)
            {
                firstCoordX = x2;
                secondCoordX = x1;
            }
            else
            {
                firstCoordX = x1;
                secondCoordX = x2;
            }
            if (y1 > y2)
            {
                firstCoordY = y2;
                secondCoordY = y1;
            }
            else
            {
                firstCoordY = y1;
                secondCoordY = y2;
            }
            canvas.DrawRectangle(firstCoordX, firstCoordY, secondCoordX, secondCoordY, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color)
        {
            return new Rectangle(x1, y1, x2, y2, color);   
        }
        public override void Stretch(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
    }

    class Line : Figure
    {
        public Line()
        {

        }
        public Line(int x1, int y1, int x2, int y2, Color color)
            : base(x1, y1, x2, y2, color)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            int firstCoordX = x1;
            int secondCoordX = x2;
            int firstCoordY = y1;
            int secondCoordY = y2;
            /*if (x1 > x2)
            {
                firstCoordX = x2;
                secondCoordX = x1;
            }
            else
            {
                firstCoordX = x1;
                secondCoordX = x2;
            }
            if (y1 > y2)
            {
                firstCoordY = y2;
                secondCoordY = y1;
            }
            else
            {
                firstCoordY = y1;
                secondCoordY = y2;
            }*/
            canvas.DrawLine(firstCoordX, firstCoordY, secondCoordX, secondCoordY, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color)
        {
            return new Line(x1, y1, x2, y2, color);
        }
        public override void Stretch(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
    }
    class Ellipse : Figure
    {
        public Ellipse()
        {

        }
        public Ellipse(int x1, int y1, int x2, int y2, Color color)
            : base(x1, y1, x2, y2, color)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            int firstCoordX = x1;
            int secondCoordX = x2;
            int firstCoordY = y1;
            int secondCoordY = y2;
            if (x1 > x2)
            {
                firstCoordX = x2;
                secondCoordX = x1;
            }
            else
            {
                firstCoordX = x1;
                secondCoordX = x2;
            }
            if (y1 > y2)
            {
                firstCoordY = y2;
                secondCoordY = y1;
            }
            else
            {
                firstCoordY = y1;
                secondCoordY = y2;
            }
            canvas.DrawEllipse(firstCoordX, firstCoordY, secondCoordX, secondCoordY, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color)
        {
            return new Ellipse(x1, y1, x2, y2, color);
        }
        public override void Stretch(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
    }
}
