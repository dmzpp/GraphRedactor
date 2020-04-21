using GraphRedactorCore.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ellipse = new Ellipse(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.SecondColor, args.ToolsArgs.Width);
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
                throw new NullReferenceException("Работа инструмента не начата");
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
