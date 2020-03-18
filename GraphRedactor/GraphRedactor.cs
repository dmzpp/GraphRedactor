using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public static DottedLine DottedLine
        {
            get
            {
                return new DottedLine();
            }
        }
    }
    class GraphRedactorApplication
    {
        private WriteableBitmap canvas;
        private Figure currentFigure;
        private LinkedList<IDrawable> figures;
        private Color conturColor;
        private Color fillColor;

        private enum States
        {
            stretching,
            positioning // выбор места для установки фигуры
        }
        public void SetFillColor(Color color)
        {
            this.fillColor = color;
        }

        public void SetConturColor(Color color)
        {
            this.conturColor = color;
        }
        

        private States currentState;
        public void SetCurrentFigure(Figure figure)
        {
            currentFigure = figure;
        }
        public GraphRedactorApplication(WriteableBitmap canvas)
        {
            LinkedList<string> test = new LinkedList<string>();

            conturColor = Colors.Red;
            fillColor = Colors.Green;
            this.canvas = canvas;
            currentFigure = new Rectangle(); 
            figures = new LinkedList<IDrawable>();
            currentState = States.positioning;
        }
        public void CreateFigure(int x1, int y1, int x2, int y2)
        {
            if (currentState == States.positioning)
            {
                figures.AddLast(currentFigure.GetFigure(x1, y1, x2, y2, conturColor, fillColor));
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
                IDrawable lastFigure = figures.Last.Value;
                lastFigure.Stretch(x, y);
                //figures.Push(lastFigure);
            }
        }
        
    }
    interface IDrawable
    {
        void Draw(WriteableBitmap canvas);
        void Stretch(int mouseX, int mouseY);

    }
    abstract class Figure : IDrawable
    {
        public abstract void Draw(WriteableBitmap canvas);
        public abstract Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor);
        protected int x1, x2, y1, y2;
        protected int firstDrawingX, secondDrawingX, firstDrawingY, secondDrawingY;
        public Color color;
        public Color fillColor;
        public Figure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2= x2;
            this.y2 = y2;
            this.color = color;
            this.fillColor = fillColor;
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
        public Rectangle(int x1, int y1, int x2, int y2, Color color, Color fillColor) 
            :base(x1, y1, x2, y2, color, fillColor)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawRectangle(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
            canvas.FillRectangle(firstDrawingX + 1, firstDrawingY + 1, secondDrawingX, secondDrawingY, fillColor);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
        {
            return new Rectangle(x1, y1, x2, y2, color, fillColor);   
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
        public Line(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            : base(x1, y1, x2, y2, color, fillColor)
        {

        }
        protected override void CalculateDrawingCoordinats()
        {
            firstDrawingX = x1;
            secondDrawingX = x2;
            firstDrawingY = y1;
            secondDrawingY = y2;
        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawLine(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
        {
            return new Line(x1, y1, x2, y2, color, fillColor);
        }
        public override void Stretch(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
    }
    class DottedLine : Line
    {
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawLineDotted(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, 10, 10, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
        {
            return new DottedLine(x1, y1, x2, y2, color, fillColor);
        }
        public DottedLine(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            : base(x1, y1, x2, y2, color, fillColor)
        {

        }
        public DottedLine()
        {

        }

    }
    class Ellipse : Figure
    {
        public Ellipse()
        {

        }
        public Ellipse(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            : base(x1, y1, x2, y2, color, fillColor)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawEllipse(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, color);
            canvas.FillEllipse(firstDrawingX + 1, firstDrawingY + 1, secondDrawingX, secondDrawingY, fillColor);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
        {
            return new Ellipse(x1, y1, x2, y2, color, fillColor);
        }
        public override void Stretch(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
    }
}
