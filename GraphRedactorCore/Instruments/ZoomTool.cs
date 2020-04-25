using GraphRedactorCore.Figures;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Instruments
{
    internal class ZoomTool : Tool
    {
        private Rectangle rectangle = null;
        private enum States
        {
            stretching,
            none
        }
        private States currentState;
        public ZoomTool()
        {
            currentState = States.none;
        }

        public override void NextPhase(ToolUsingArgs args)
        {
            return;
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            if (currentState != States.stretching)
            {
                rectangle = new Rectangle(args.Point, Colors.DarkOrange, Colors.Transparent, 2, args.GraphGlobalData);
                args.GraphGlobalData.Drawables.AddLast(rectangle);
                currentState = States.stretching;
            }
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            if (rectangle == null)
            {
                return;
            }
            args.GraphGlobalData.Drawables.RemoveLast();
            if (rectangle.firstCoord.X == rectangle.secondCoord.X && rectangle.firstCoord.Y == rectangle.secondCoord.Y)
            {
                args.GraphGlobalData.PushViewPort(args.GraphGlobalData.ViewPort.Calculate(rectangle.firstCoord));
            }
            else
            {
                args.GraphGlobalData.ViewPort.Calculate(rectangle.firstDrawingCoord, rectangle.secondDrawingCoord);
            }
            args.GraphGlobalData.Bitmap.Clear();
            args.GraphGlobalData.Bitmap.DrawRectangle(
                   (int)args.GraphGlobalData.ViewPort.firstPoint.X,
                   (int)args.GraphGlobalData.ViewPort.firstPoint.Y,
                   (int)args.GraphGlobalData.ViewPort.secondPoint.X,
                   (int)args.GraphGlobalData.ViewPort.secondPoint.Y,
                   Colors.Red);
            foreach (var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }
            
            rectangle = null;
            currentState = States.none;
        }

        public override void Use(ToolUsingArgs args)
        {
            if (rectangle == null)
            {
                return;
            }
            rectangle.ChangeLastPoint(args.Point);
            Update(args.GraphGlobalData);
        }

        private void Update(GraphGlobalData graphGlobalData)
        {
            graphGlobalData.Drawables.RemoveLast();
            graphGlobalData.Drawables.AddLast(rectangle);
        }
    }
}
