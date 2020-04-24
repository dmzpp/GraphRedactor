using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphRedactorCore.Figures;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Instruments
{
    internal class ZoomTool : Tool
    {
        private Rectangle rectangle = null;
        private WriteableBitmap bitmapCopy = null;

        public override void NextPhase(ToolUsingArgs args)
        {
            args.GraphGlobalData.Drawables.RemoveLast();
            args.GraphGlobalData.ViewPort.Calculate(rectangle.firstCoord, rectangle.secondCoord);
            args.GraphGlobalData.Bitmap.Clear();
            foreach (var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }
            bitmapCopy = args.GraphGlobalData.Bitmap.Clone();
            rectangle = null;
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            rectangle = new Rectangle(args.Point, Colors.DarkOrange, Colors.Transparent, 2, args.GraphGlobalData.ViewPort);
            args.GraphGlobalData.Drawables.AddLast(rectangle);
        }

        public override void StopUsing(ToolUsingArgs args)
        {

            args.GraphGlobalData.Drawables.RemoveLast();
            if(rectangle.firstCoord.X == rectangle.secondCoord.X && rectangle.firstCoord.Y == rectangle.secondCoord.Y)
            {
                args.GraphGlobalData.ViewPort.Calculate(rectangle.firstCoord);
            }
            else
            {
                args.GraphGlobalData.ViewPort.Calculate(rectangle.firstCoord, rectangle.secondCoord);
            }
            args.GraphGlobalData.Bitmap.Clear();
            foreach (var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }
            bitmapCopy = args.GraphGlobalData.Bitmap.Clone();

        }


        public override void Use(ToolUsingArgs args)
        {

            if (rectangle == null)
            {
                //throw new NullReferenceException("Работа инструмента не начата");
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
