﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphRedactorCore.Figures;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphRedactorCore.Instruments
{
    internal class ZoomAreaTool : Tool
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
            rectangle = null;
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            rectangle = new Rectangle(args.Point, Colors.Gray, Colors.Transparent, 2, args.GraphGlobalData.ViewPort);
            args.GraphGlobalData.Drawables.AddLast(rectangle);
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
            foreach (var drawable in args.GraphGlobalData.Drawables)
            {
                drawable.Draw(args.GraphGlobalData.Bitmap);
            }


            if (rectangle == null)
            {
               // args.GraphGlobalData.Drawables.RemoveLast();
            }
        }

        public override void Use(ToolUsingArgs args)
        {

            if (rectangle == null)
            {
                throw new NullReferenceException("Работа инструмента не начата");
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
