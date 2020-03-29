using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorApp
{
   
    public class GraphRedactorApplication
    {
        private WriteableBitmap canvas;
        private Figure currentFigure;
        private Instrument currentInstrument;
        private LinkedList<IDrawable> graphicEntities;
        private Color conturColor;
        private Color fillColor;
        private States currentState;
        private int currentWidth;

        public void ChangeConturColor(Color color)
        {
            conturColor = color;
        }
        public void ChangeFillColor(Color color)
        {
            fillColor = color;
        }
        private enum States
        {
            instrumentSelected,
            figureSelected,
            changing
        }
        public void SetFillColor(Color color)
        {
            this.fillColor = color;
        }

        public void SetConturColor(Color color)
        {
            this.conturColor = color;
        }
        
        public void BitmapUpdate(WriteableBitmap bitmap)
        {
            this.canvas = bitmap;
        }

        public void SetCurrentFigure(Figure figure)
        {
            currentFigure = figure;
            currentInstrument = null;
            currentState = States.figureSelected;
        }

        public void SetCurrentInstrument(Instrument instrument)
        {
            currentFigure = null;
            currentInstrument = instrument;
            currentState = States.instrumentSelected;
        }

        private void SetDefault()
        {
            conturColor = Colors.Red;
            fillColor = Colors.Transparent;
            currentFigure = null;
            currentInstrument = new Pencil();
            currentState = States.instrumentSelected;
        }
        public GraphRedactorApplication(WriteableBitmap canvas)
        {
            this.canvas = canvas;
            graphicEntities = new LinkedList<IDrawable>();
            SetDefault();
        }

        // будет вызываться при нажатии мышкой на холст
        public void StartDrawing(int x, int y)
        {
            if(currentState == States.figureSelected)
            {
                CreateFigure(x, y);
            }
            else if(currentState == States.instrumentSelected)
            {
                StartUsingInstrument(x, y);
            }
            currentState = States.changing;
        }

        // будет вызываься при ведении мышкой
        public void UpdateCurrentState(int x, int y)
        {
            ChangeLastEntity(x, y);
        }

        private void StartUsingInstrument(int x, int y)
        {
            graphicEntities.AddLast(currentInstrument.CreateInstrument(x, y, conturColor, 0));
        }
        public void CreateFigure(int x, int y)
        {
            graphicEntities.AddLast(currentFigure.GetFigure(x, y, x, y, conturColor, fillColor));
        }

        public void FinishChanging()
        {
            currentState = (currentFigure == null) ? States.instrumentSelected : States.figureSelected;
        }
        public WriteableBitmap RenderCanvas()
        {
            canvas.Clear();
            foreach(var figure in graphicEntities)
            {
                figure.Draw(canvas);
            }
            return canvas;
        }

        public void ChangeLastEntity(int x, int y)
        {
            if (currentState == States.changing)
            {
                IDrawable lastFigure = graphicEntities.Last.Value;
                lastFigure.Change(x, y);
            }
        }
        
    }
    interface IDrawable
    {
        void Draw(WriteableBitmap canvas);
        void Change(int mouseX, int mouseY);

    }
    public abstract class Figure : IDrawable
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
        public virtual void Change(int mouseX, int mouseY)
        {
            x2 = mouseX;
            y2 = mouseY;
        }
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
    public class Rectangle : Figure
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
            canvas.DrawRectangle(firstDrawingX - 1, firstDrawingY - 1, secondDrawingX, secondDrawingY, color);
            canvas.FillRectangle(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, fillColor);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            => new Rectangle(x1, y1, x2, y2, color, fillColor);   
    }
    public class Line : Figure
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
            => new Line(x1, y1, x2, y2, color, fillColor);
        
    }
    public class DottedLine : Line
    {
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawLineDotted(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, 10, 10, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            => new DottedLine(x1, y1, x2, y2, color, fillColor);
        
        public DottedLine(int x1, int y1, int x2, int y2, Color color, Color fillColor)
            : base(x1, y1, x2, y2, color, fillColor)
        {

        }
        public DottedLine()
        {

        }

    }
    public class Ellipse : Figure
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
            => new Ellipse(x1, y1, x2, y2, color, fillColor);
    }
    public class PossibleFigures
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

    struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public abstract class Instrument : IDrawable
    {
        public abstract void Draw(WriteableBitmap canvas);
        public abstract void Change(int mouseX, int mouseY);
        public abstract Instrument CreateInstrument(int x, int y, Color color, int width);
    }
    public class Pencil : Instrument
    {
        private int width;
        private Color color;

        //   private List<Point> points;
        private List<int> points;
        public Pencil()
        {

        }
        public Pencil(int x, int y, Color color, int width)
        {
            this.width = 0;
            this.color = color;
            //points = new List<Point>();
            //points.Add(new Point(x, y));
            points = new List<int>();
            points.Add(x);
            points.Add(y);
        }
        public override Instrument CreateInstrument(int x, int y, Color color, int width)
        {
            return new Pencil(x,y,color,width);
        }
        public override void Draw(WriteableBitmap canvas)
        {
            int[] coords = points.ToArray();
            canvas.DrawPolyline(coords, color);
        }
        public override void Change(int mouseX, int mouseY)
        {
            points.Add(mouseX);
            points.Add(mouseY);
        }
    }
    public class PossibleInstruments
    {
        public static Pencil Pencil
        { 
            get{
                return new Pencil();
            } 
        }
    }
}
