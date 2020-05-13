using GraphRedactorCore.Brushes;
using GraphRedactorCore.Figures;
using GraphRedactorCore.Pens;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Windows;
using Xceed.Wpf.Toolkit.Primitives;
using YamlDotNet.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NodeDeserializers;
using System.Reflection;
using System.Linq;
using System.Windows.Media;

namespace GraphRedactorCore
{
    public class GraphRedactor
    {
        private readonly GraphData graphData;
        public ToolPicker ToolPicker { get; }

        public GraphRedactor(int width, int height, DrawingCanvas drawingCanvas)
        {
            graphData = new GraphData(width, height, drawingCanvas);
            ToolPicker = new ToolPicker();

            BrushPicker.AddBrush(new LinesBrush());
            BrushPicker.AddBrush(new SolidBrush());
            BrushPicker.AddBrush(new CrossBrush());
            BrushPicker.AddBrush(new SecondLinesBrush());

            PenPicker.AddPen(new DashPen());
            PenPicker.AddPen(new SolidPen());
            PenPicker.AddPen(new DashDotDotPen());
        }

        public void Render()
        {
            graphData.canvas.Render(graphData.drawables, graphData.viewPorts.Last());
        }

        public void PreviousScale()
        {
            graphData.viewPorts.RemoveLast();
        }

        public void Resize(double width, double height)
        {
            graphData.viewPorts.First().secondPoint.X = width;
            graphData.viewPorts.First().secondPoint.Y = height;
        }

        public void MouseMove(Point point)
        {
            ToolPicker.GetTool().MouseMove(point, graphData);
        }

        public void MouseLeftButtonUp(Point point)
        {
            ToolPicker.GetTool().MouseLeftButtonUp(point, graphData);
        }

        public void MouseRightButtonUp(Point point)
        {
            ToolPicker.GetTool().MouseRightButtonUp(point, graphData);
        }

        public void MouseLeftButtonDown(Point point)
        {
            ToolPicker.GetTool().MouseLeftButtonDown(point, graphData);
        }

        public void SaveFile(string path)
        {
            var serializer = new SerializerBuilder();
            serializer.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults);
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Build().Serialize(writer, graphData.drawables);
            }
        }

        public void OpenFile(string path)
        {
            foreach(var list in graphData.drawables)
            {
                list.Value.Clear();
            }
            var deserializer = new Deserializer();
            var serializer = new Serializer();
            using (StreamReader reader = new StreamReader(path))
            {
                var data = deserializer.Deserialize<Dictionary<Type, LinkedList<object>>>(reader);
                foreach(var drawableType in data)
                {
                    var genericMethodInfo = deserializer.GetType().GetMethods().Single(method => 
                            method.Name == nameof(deserializer.Deserialize) && 
                            method.IsGenericMethod && 
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(string));

                    var genericMethod = genericMethodInfo.MakeGenericMethod(drawableType.Key);
                    foreach(var obj in drawableType.Value)
                    {
                        var str = serializer.Serialize(obj);
                        var result = genericMethod.Invoke(deserializer, new object[] { str });
                        graphData.drawables[result.GetType()].AddLast((IDrawable)result);
                    }
                }
            }
        }
    }
}
