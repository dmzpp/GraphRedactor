﻿using GraphRedactorCore.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            polyLine = new PolyLine(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.Width);
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
