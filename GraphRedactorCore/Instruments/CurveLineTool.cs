using GraphRedactorCore.Figures;

namespace GraphRedactorCore.Instruments
{
    internal class CurveLineTool : Tool
    {
        private PolyLine polyLine = null;
        private int linesCount = 0;

        public override void NextPhase(ToolUsingArgs args)
        {
            linesCount++;
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            polyLine = new PolyLine(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.Width, args.GraphGlobalData.ViewPort);
            args.GraphGlobalData.Drawables.AddLast(polyLine);
            linesCount = 1;
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            Update(args.GraphGlobalData);
            polyLine = null;
            linesCount = 0;
        }

        public override void Use(ToolUsingArgs args)
        {
            polyLine.ChangeLastPoint(args.Point, true, linesCount);
            Update(args.GraphGlobalData);
        }
        private void Update(GraphGlobalData graphGlobalData)
        {
            graphGlobalData.Drawables.RemoveLast();
            graphGlobalData.Drawables.AddLast(polyLine);
        }
    }
}
