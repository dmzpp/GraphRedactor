﻿using GraphRedactorCore.Tools;
using GraphRedactorCore.ToolsParams;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GraphRedactorCore
{
    public class ToolPicker
    {
        private readonly Dictionary<Type, Tool> _tools;
        private Type _currentTool;
        public ToolPicker()
        {
            _tools = new Dictionary<Type, Tool>();
            _currentTool = typeof(RectangleTool);
        }

        public Type CurrentType()
        {
            return _currentTool;
        }
        public void AddTool(Tool tool)
        {
            _tools.Add(tool.GetType(), tool);
        }

        public void RemoveTool(Tool tool)
        {
            _tools.Remove(tool.GetType());
        }

        public void SetTool(Type toolType)
        {
            _currentTool = toolType;
        }

        public Tool GetTool()
        {
            if (!_tools.ContainsKey(_currentTool))
            {
                throw new Exception($"{_currentTool.Name} is not exist in that collection");
            }
            return _tools[_currentTool];
        }

        public void RenderTools(Panel toolPanel, Panel toolArgs)
        {
            toolPanel.Children.Clear();
            foreach (var tool in _tools)
            {
                var toolView = tool.Value.ToolView;
                if (toolView != null)
                {
                    (toolView as Button)?.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) =>
                    {
                        _currentTool = tool.Key;
                        RenderToolArgs(toolArgs);
                    }));
                    toolPanel.Children.Add(toolView);
                }
            }
            RenderToolArgs(toolArgs);
        }
        private void RenderToolArgs(Panel panel)
        {
            var toolArgs = _currentTool.GetProperties();
            panel.Children.Clear();
            foreach (var arg in toolArgs)
            {
                if (arg.PropertyType.IsSubclassOf(typeof(ToolParam)))
                {
                    var view = ((arg.GetValue(GetTool())) as ToolParam)?.ArgView;
                    if (view != null)
                    {
                        panel.Children.Add(view);
                    }
                }
            }
        }
    }
}