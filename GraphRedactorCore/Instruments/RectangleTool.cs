using GraphRedactorCore.Figures;
using System;

namespace GraphRedactorCore.Instruments
{
    internal class RectangleTool : Tool
    {
        private Rectangle rectangle = null;

        public override void NextPhase(ToolUsingArgs args)
        {
            throw new NotImplementedException("Данная фигура не поддерживает подобной функции");
        }

        public override void StartUsing(ToolUsingArgs args)
        {
            rectangle = new Rectangle(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.SecondColor, args.ToolsArgs.Width, args.GraphGlobalData);
            args.GraphGlobalData.Drawables.AddLast(rectangle);
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            Update(args.GraphGlobalData);
            rectangle = null;
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
            if (graphGlobalData.Drawables.Count == 0)
                return;
            graphGlobalData.Drawables.RemoveLast();
            graphGlobalData.Drawables.AddLast(rectangle);
        }
    }
}
