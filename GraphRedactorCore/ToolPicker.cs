using GraphRedactorCore.Instruments;
using System.Collections.Generic;

namespace GraphRedactorCore
{
    public class ToolPicker
    {
        private readonly Dictionary<Tools, Tool> tools;
        public Tools CurrentToolType { get; set; }
        internal Tool CurrentTool
        {
            get => tools[CurrentToolType];
        }

        public enum Tools
        {
            Rectangle,
            Pencil,
            Ellipse,
            CurveLine,
            Zoom,
            HandTool,
            Line
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
                [Tools.HandTool] = new HandTool(),
                [Tools.Line] = new LineTool()
            };
            CurrentToolType = Tools.Rectangle;
        }
    }
}
