using System;
using System.Windows;

namespace GraphRedactorCore.Instruments
{
    internal class HandTool : Tool
    {
        private Point coursorPoint;

        public override void NextPhase(ToolUsingArgs args)
        {
            throw new NotImplementedException();
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            coursorPoint = args.Point;
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            return;
        }

        public override void Use(ToolUsingArgs args)
        {
            var distanceX = coursorPoint.X - args.Point.X;
            var distanceY = coursorPoint.Y - args.Point.Y;

            if (args.GraphGlobalData.ViewPort == args.GraphGlobalData.FirstViewPort)
            {
                return;
            }

            if (args.GraphGlobalData.ViewPort.firstPoint.X + distanceX > 0)
            {
                args.GraphGlobalData.ViewPort.firstPoint.X += distanceX;
            }
            if (args.GraphGlobalData.ViewPort.secondPoint.X + distanceX < args.GraphGlobalData.Bitmap.Width)
            {
                args.GraphGlobalData.ViewPort.secondPoint.X += distanceX;
            }
            if (args.GraphGlobalData.ViewPort.firstPoint.Y + distanceY > 0)
            {
                args.GraphGlobalData.ViewPort.firstPoint.Y += distanceY;
            }
            if (args.GraphGlobalData.ViewPort.secondPoint.Y + distanceY < args.GraphGlobalData.Bitmap.Height)
            {
                args.GraphGlobalData.ViewPort.secondPoint.Y += distanceY;
            }

            coursorPoint = args.Point;
        }
    }
}
