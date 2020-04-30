using System;
using System.Collections.Generic;
using GraphRedactorCore.Tools;

namespace GraphRedactorCore
{
    public class ToolPicker
    {
        private readonly Dictionary<Type, ITool> _tools;
        private Type _currentTool;
        public ToolPicker()
        {
            _tools = new Dictionary<Type, ITool>();
            _currentTool = typeof(RectangleTool);
        }

        public Type CurrentType()
        {
            return _currentTool;
        }
        internal void AddTool(ITool tool)
        {
            _tools.Add(tool.GetType(), tool);
        }

        internal void RemoveTool(ITool tool)
        {
            _tools.Remove(tool.GetType());
        }

        public void SetTool(Type toolType)
        {
            _currentTool = toolType;
        }

        public ITool GetTool()
        {
            if (!_tools.ContainsKey(_currentTool))
            {
                throw new Exception($"{_currentTool.Name} is not exist in that collection");
            }
            return _tools[_currentTool];
        }
    }
}