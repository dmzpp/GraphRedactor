using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace GraphRedactorCore.Instruments
{
    internal class ZoomTool : Tool
    {
        private WriteableBitmap bitmapCopy = null;
        private readonly int toolScale = 2;


        public override void NextPhase(ToolUsingArgs args)
        {

        }

        public override void StartUsing(ToolUsingArgs args)
        {
            args.GraphGlobalData.ViewPort.Calculate(args.Point, toolScale);
            args.GraphGlobalData.Bitmap.Clear();
            foreach(var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }
            bitmapCopy = args.GraphGlobalData.Bitmap.Clone();

            WriteableBitmap cropedBitmap = args.GraphGlobalData.Bitmap.Crop(
                (int)args.GraphGlobalData.ViewPort.firstPoint.X,
                (int)args.GraphGlobalData.ViewPort.firstPoint.Y,
                (int)(args.GraphGlobalData.ViewPort.secondPoint.X - args.GraphGlobalData.ViewPort.firstPoint.X),
                (int)(args.GraphGlobalData.ViewPort.secondPoint.Y - args.GraphGlobalData.ViewPort.firstPoint.Y));
            args.GraphGlobalData.Bitmap.Clear();
            args.GraphGlobalData.Bitmap.Blit(
                new System.Windows.Rect(0, 0, args.GraphGlobalData.Bitmap.Width, args.GraphGlobalData.Bitmap.Height),
                cropedBitmap,
                new System.Windows.Rect(0, 0, cropedBitmap.Width, cropedBitmap.Height));

        }

        public override void StopUsing(ToolUsingArgs args)
        {
            args.GraphGlobalData.ViewPort.Scale = 1;
            args.GraphGlobalData.Bitmap = bitmapCopy.Clone();
            args.GraphGlobalData.ViewPort.firstPoint.X = 0;
            args.GraphGlobalData.ViewPort.firstPoint.Y = 0;
            args.GraphGlobalData.ViewPort.secondPoint.X = args.GraphGlobalData.Bitmap.Width;
            args.GraphGlobalData.ViewPort.secondPoint.Y = args.GraphGlobalData.Bitmap.Height;

            args.GraphGlobalData.Bitmap.Clear();
            foreach(var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }
        }

        public override void Use(ToolUsingArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
