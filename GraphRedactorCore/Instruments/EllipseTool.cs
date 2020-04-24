using GraphRedactorCore.Figures;
using System;

namespace GraphRedactorCore.Instruments
{
    internal class EllipseTool : Tool
    {
        private Ellipse ellipse = null;

        public override void NextPhase(ToolUsingArgs args)
        {
            throw new NotImplementedException();
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            ellipse = new Ellipse(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.SecondColor, args.ToolsArgs.Width, args.GraphGlobalData.ViewPort);
            args.GraphGlobalData.Drawables.AddLast(ellipse);
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            Update(args.GraphGlobalData);
            ellipse = null;
        }

        public override void Use(ToolUsingArgs args)
        {
            if (ellipse == null)
            {
                return;
            }
            ellipse.ChangeLastPoint(args.Point);
            Update(args.GraphGlobalData);
        }

        private void Update(GraphGlobalData graphGlobalData)
        {
            graphGlobalData.Drawables.RemoveLast();
            graphGlobalData.Drawables.AddLast(ellipse);
        }
    }
}
