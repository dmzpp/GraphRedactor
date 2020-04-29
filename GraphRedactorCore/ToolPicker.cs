using System;
using System.Collections.Generic;
using GraphRedactorCore.Tools;

namespace GraphRedactorCore
{
    public class ToolPicker
    {
        private readonly Dictionary<Type, ITool> tools;
        private Type currentTool;
        public ToolPicker()
        {
            tools = new Dictionary<Type, ITool>();
            currentTool = typeof(RectangleTool);
        }

        public Type CurrentType()
        {
            return currentTool;
        }
        internal void AddTool(ITool tool)
        {
            tools.Add(tool.GetType(), tool);
        }

        internal void RemoveTool(ITool tool)
        {
            tools.Remove(tool.GetType());
        }

        public void SetTool(Type toolType)
        {
            currentTool = toolType;
        }

        public ITool GetTool()
        {
            if (!tools.ContainsKey(currentTool))
            {
                throw new Exception($"{currentTool.Name} is not exist in that collection");
            }
            return tools[currentTool];
        }
    }
}