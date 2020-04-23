using System;
using System.Collections.Generic;
using System.Linq;
using GraphRedactorCore.Instruments;
using System.Text;
using System.Threading.Tasks;
using GraphRedactorCore.Figures;

namespace GraphRedactorCore
{
    public class ToolPicker
    {
        private readonly Dictionary<Tools, Tool> tools;
        public Tools CurrentToolType { get; set; }
        internal Tool CurrentTool {
            get => tools[CurrentToolType];
        }

        public enum Tools
        {
            Rectangle,
            Pencil,
            Ellipse,
            CurveLine,
            Zoom,
            ZoomArea
        }

        public ToolPicker()
        {
            tools = new Dictionary<Tools, Tool>()
            {
                [Tools.Rectangle] = new RectangleTool(),
                [Tools.Pencil] = new Pencil(),
                [Tools.Ellipse] = new EllipseTool(),
                [Tools.CurveLine] = new CurveLineTool(),
                [Tools.Zoom] = new ZoomTool(),
                [Tools.ZoomArea] = new ZoomAreaTool()
            };
            CurrentToolType = Tools.Rectangle;
        }
    }
}
