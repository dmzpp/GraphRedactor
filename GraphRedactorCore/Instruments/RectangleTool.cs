using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GraphRedactorCore.Figures;

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
            rectangle = new Rectangle(args.Point, args.ToolsArgs.FirstColor, args.ToolsArgs.SecondColor, args.ToolsArgs.Width);
            args.GraphGlobalData.Drawables.AddLast(rectangle);
        }

        public override void StopUsing(ToolUsingArgs args)
        {
            Update(args.GraphGlobalData);
            rectangle = null;
        }

        public override void Use(ToolUsingArgs args)
        {
            if(rectangle == null)
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
