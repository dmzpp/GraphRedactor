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

namespace GraphRedactorApp
{
    class PossibleFigures
    {
        public static Rectangle Rectangle
        {
            get
            {
                return new Rectangle();
            }
        }
        public static Line Line
        {
            get
            {
                return new Line();
            }
        }
        public static Ellipse Ellipse
        {
            get
            {
                return new Ellipse();
            }
        }
    }
    class GraphRedactorApplication
    {
        private WriteableBitmap canvas;
        private Figure currentFigure;
        private Stack<Figure> figures;
        private Color currentColor;

        private enum States
        {
            stretching,
            positioning // выбор места для установки фигуры
        }
        private States currentState;
        public void SetCurrentFigure(Figure figure)
        {
            currentFigure = figure;
        }
        public GraphRedactorApplication(WriteableBitmap canvas)
        {
            currentColor = Colors.Red;
            this.canvas = canvas;
            currentFigure = new Rectangle(); // установка фигуры по умолчанию
            figures = new Stack<Figure>();
            currentState = States.positioning;
        }
        public void CreateFigure(int x1, int y1, int x2, int y2)
        {
            if (currentState == States.positioning)
            {
                figures.Push(currentFigure.GetFigure(x1, y1, x2, y2, currentColor));
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

        public void StrechLastFigure(int x, int y)
        {
            if (currentState == States.stretching)
            {
                Figure lastFigure = figures.Peek();
                lastFigure.Stretch(x, y);
                figures.Push(lastFigure);
            }
        }
        
    }
    
    abstract class Figure
    {
        public abstract void Draw(WriteableBitmap canvas);
        public abstract Figure GetFigure(int x1, int y1, int x2, int y2, Color color);
        protected int x1, x2, y1, y2;
        protected int firstDrawingX, secondDrawingX, firstDrawingY, secondDrawingY;
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
        protected virtual void CalculateDrawingCoordinats()
        {
            if (x1 > x2)
            {
                firstDrawingX = x2;
                secondDrawingX = x1;
            }
            else
            {
                firstDrawingX = x1;
                secondDrawingX = x2;
            }
            if (y1 > y2)
            {
                firstDrawingY = y2;
                secondDrawingY = y1;
            }
            else
            {
                firstDrawingY = y1;
                secondDrawingY = y2;
            }
        }
       
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
            CalculateDrawingCoordinats();
            canvas.DrawRectangle(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
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
        protected override void CalculateDrawingCoordinats()
        {
            int firstDrawingX = x1;
            int secondDrawingX = x2;
            int firstDrawingY = y1;
            int secondDrawingY = y2;
        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawLine(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
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
            CalculateDrawingCoordinats();
            canvas.DrawEllipse(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
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
