using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
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
        public int currentWidth { get; set; }

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
            currentWidth = 1;
            currentInstrument = new Pencil();
            currentState = States.instrumentSelected;
        }
        public GraphRedactorApplication(WriteableBitmap canvas)
        {
            this.canvas = canvas;
            graphicEntities = new LinkedList<IDrawable>();
            SetDefault();
        }

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

        public void UpdateCurrentState(int x, int y)
        {
            ChangeLastEntity(x, y);
        }

        private void StartUsingInstrument(int x, int y)
        {
            graphicEntities.AddLast(currentInstrument.CreateInstrument(x, y, conturColor, currentWidth));
        }
        public void CreateFigure(int x, int y)
        {
            graphicEntities.AddLast(currentFigure.GetFigure(x, y, x, y, conturColor, fillColor, currentWidth));
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
        public abstract Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width);
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }

        protected int firstDrawingX, secondDrawingX, firstDrawingY, secondDrawingY;
        public Color color;
        public Color fillColor;
        protected int width;
        public Figure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2= x2;
            this.y2 = y2;
            this.color = color;
            this.fillColor = fillColor;
            this.width = width;
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
        public Rectangle(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width) 
            :base(x1, y1, x2, y2, color, fillColor, width)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            int actualWidth = width;
            while(actualWidth >= 0)
            {
                canvas.DrawRectangle(firstDrawingX - actualWidth, firstDrawingY - actualWidth, secondDrawingX + actualWidth, secondDrawingY + actualWidth, color);
                actualWidth--;
            }
            canvas.FillRectangle(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, fillColor);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            => new Rectangle(x1, y1, x2, y2, color, fillColor, width);   
    }
    public class Line : Figure
    {
        public Line()
        {

        }
        public Line(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            : base(x1, y1, x2, y2, color, fillColor, width)
        {

        }
        protected int thirdDrawingX, thirdDrawingY;
        protected int fourthDrawingX, fourthDrawingY;
        protected int fifthDrawingX, fifthDrawingY; 
        protected int sixthDrawingX, sixthDrawingY;
        protected override void CalculateDrawingCoordinats()
        {
            firstDrawingX = x1;
            secondDrawingX = x2;
            firstDrawingY = y1;
            secondDrawingY = y2;

            int firstSide = Math.Abs(firstDrawingX - secondDrawingX);
            int secondSide = Math.Abs(firstDrawingY - secondDrawingY);

            int length = (int)Math.Sqrt(firstSide * firstSide + secondSide * secondSide);

            if (length != 0)
            {
                int additionalSecondSide = secondSide * width / length;

                if((firstDrawingY < secondDrawingY && firstDrawingX > secondDrawingX) 
                    || (firstDrawingX < secondDrawingX && secondDrawingY < firstDrawingY))
                {
                    thirdDrawingX = firstDrawingX + additionalSecondSide;
                    fourthDrawingX = secondDrawingX + additionalSecondSide;
                    fifthDrawingX = firstDrawingX - additionalSecondSide;
                    sixthDrawingX = secondDrawingX - additionalSecondSide;
                }
                else
                {
                    thirdDrawingX = firstDrawingX - additionalSecondSide;
                    fourthDrawingX = secondDrawingX - additionalSecondSide;
                    fifthDrawingX = firstDrawingX + additionalSecondSide;
                    sixthDrawingX = secondDrawingX + additionalSecondSide;
                }

                int additionalFirstSide = firstSide * width / length;

                thirdDrawingY = firstDrawingY + additionalFirstSide;
                fourthDrawingY = secondDrawingY + additionalFirstSide;
                fifthDrawingY = firstDrawingY - additionalFirstSide;
                sixthDrawingY = secondDrawingY - additionalFirstSide; 
            }
            else
            {
                thirdDrawingX = firstDrawingX;
                thirdDrawingY = firstDrawingY;

                fourthDrawingX = secondDrawingX;
                fourthDrawingY = secondDrawingY;

                fifthDrawingY = thirdDrawingY;
                sixthDrawingY = fourthDrawingY;
            }
        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.FillQuad(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, fourthDrawingX, fourthDrawingY, thirdDrawingX, thirdDrawingY, color);
            canvas.FillQuad(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, sixthDrawingX, sixthDrawingY, fifthDrawingX, fifthDrawingY, color);

        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            => new Line(x1, y1, x2, y2, color, fillColor, width);
        
    }
    public class DottedLine : Line
    {
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            canvas.DrawLineDotted(firstDrawingX, firstDrawingY, secondDrawingX, secondDrawingY, 10, 10, color);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            => new DottedLine(x1, y1, x2, y2, color, fillColor, width);
        
        public DottedLine(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            : base(x1, y1, x2, y2, color, fillColor, width)
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
        public Ellipse(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            : base(x1, y1, x2, y2, color, fillColor, width)
        {

        }
        public override void Draw(WriteableBitmap canvas)
        {
            CalculateDrawingCoordinats();
            int actualWidth = width;
            while (actualWidth >= 0)
            {
                canvas.DrawEllipse(firstDrawingX - actualWidth, firstDrawingY - actualWidth, secondDrawingX + actualWidth, secondDrawingY + actualWidth, color);
                actualWidth--;
            }
            canvas.FillEllipse(firstDrawingX + 1, firstDrawingY + 1, secondDrawingX, secondDrawingY, fillColor);
        }
        public override Figure GetFigure(int x1, int y1, int x2, int y2, Color color, Color fillColor, int width)
            => new Ellipse(x1, y1, x2, y2, color, fillColor, width);
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
        private List<int> points;
        public Pencil()
        {

        }
        public Pencil(int x, int y, Color color, int width)
        {
            this.width = width;
            this.color = color;
            points = new List<int>();
            points.Add(x);
            points.Add(y);
        }
        public override Instrument CreateInstrument(int x, int y, Color color, int width)
        {
            return new Pencil(x, y, color, width);
        }
        public override void Draw(WriteableBitmap canvas)
        {
            using (canvas.GetBitmapContext())
            {
                /*  for (int i = 2; i < points.Count; i += 2)
                  {
                      canvas.FillQuad(points[i - 2], points[i - 1], points[i - 2] + width, points[i - 1] + width,
                            points[i] + width, points[i + 1] + width, points[i], points[i + 1], color);
                      //DrawPixelEllipse(canvas, points[i - 2], points[i - 1], 20, color);    
                  }*/

               /* List<double> newPoints;
                for (int i = 2; i < points.Count; i += 2)
                {
                    newPoints = Interpolate(points[i], points[i + 1], points[i - 2], points[i - 1], 20);
                    for (int j = 2; j < newPoints.Count; j += 2)
                    {
                        //Ellipse ellipse = new Ellipse((int)newPoints[j], (int)newPoints[j + 1], (int)newPoints[j - 2], (int)newPoints[j - 1], color, color, 1);
                        //canvas.DrawLine((int)newPoints[j], (int)newPoints[j + 1], (int)newPoints[j - 2], (int)newPoints[j - 1], color);
                        //ellipse.Draw(canvas);
                        //DrawPixelEllipse(canvas, (int)newPoints[j - 2], (int)newPoints[j - 1], width, color);
                     *//*   canvas.FillQuad(points[j - 2], points[j - 1], points[j - 2] + width, points[j - 1] + width,
                          points[j] + width, points[j + 1] + width, points[j], points[j + 1], color);*/
                       /* canvas.FillQuad((int)newPoints[j - 2], (int)newPoints[j - 1], (int)newPoints[j - 2] + width, (int)newPoints[j - 1],
                            (int)newPoints[j] + width, (int)newPoints[j + 1], (int)newPoints[j], (int)newPoints[j + 1], color);*//*
                    }

                }*/
                if(width < 4)
                {
                    canvas.DrawPolyline(points.ToArray(), color);
                }
                else {
                    for (int i = 2; i < points.Count; i += 2)
                    {
                        canvas.FillEllipseCentered(points[i - 2], points[i - 1], width, width, color);
                    }
                }
            }
        }
        public override void Change(int mouseX, int mouseY)
        {

            List<int> newPoints =  Interpolate(points[points.Count - 2], points[points.Count - 1], mouseX, mouseY, width)
                                                                .ConvertAll<int>(new Converter<double, int>((value) => (int)value));
            points.InsertRange(points.Count, newPoints);
        }


        private List<double> Interpolate(int x1, int y1, int x2, int y2, int width)
        {
            List<double> result = new List<double>();
            double firstSide = Math.Abs(x1 - x2);
            double secondSide = Math.Abs(y1 - y2);

            double length = Math.Sqrt(firstSide * firstSide + secondSide * secondSide);
            double pointsCount = Math.Ceiling(2 * (double)length / (width));

            firstSide = firstSide / pointsCount;
            secondSide = secondSide / pointsCount;

            result.Add(x1);
            result.Add(y1);
            double prevX = x1;
            double prevY = y1;

            for (int i = 0; i < pointsCount; i++)
            {
                // 1 четверть
                if (x1 < x2 && y1 > y2)
                {
                    prevX = prevX + firstSide;
                    prevY = prevY - secondSide;
                }
                // 2 четверть
                else if (x1 > x2 && y1 > y2)
                {
                    prevX = prevX - firstSide;
                    prevY = prevY - secondSide;
                }
                // 3 четверть
                else if (x1 > x2 && y1 < y2)
                {
                    prevX = prevX - firstSide;
                    prevY = prevY + secondSide;
                }
                // 4 четверть
                else if (x1 < x2 && y1 < y2)
                {
                    prevX = prevX + firstSide;
                    prevY = prevY + secondSide;
                }
                result.Add(prevX);
                result.Add(prevY);
            }
            result.Add(x2);
            result.Add(y2);

            return result;
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

