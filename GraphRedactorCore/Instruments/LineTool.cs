using GraphRedactorCore.Figures;
using System;

namespace GraphRedactorCore.Instruments
{
    internal class LineTool : Tool
    {
        private PolyLine polyLine = null;
        public override void NextPhase(ToolUsingArgs args)
        {
            throw new NotImplementedException();
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            polyLine = new PolyLine(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.Width, args.GraphGlobalData);
            args.GraphGlobalData.Drawables.AddLast(polyLine);
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            Update(args.GraphGlobalData);
            polyLine = null;
        }

        public override void Use(ToolUsingArgs args)
        {
            polyLine.ChangeLastPoint(args.Point, true);
            Update(args.GraphGlobalData);
        }
        private void Update(GraphGlobalData graphGlobalData)
        {
            graphGlobalData.Drawables.RemoveLast();
            graphGlobalData.Drawables.AddLast(polyLine);
        }
    }
}
