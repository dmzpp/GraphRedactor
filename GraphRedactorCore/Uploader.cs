using GraphRedactorCore.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace GraphRedactorCore
{
    internal class Uploader
    {
        public void SaveFile(string path, GraphData graphData)
        {
            var serializer = new SerializerBuilder();

            var elements = new Dictionary<Type, LinkedList<DrawableElement>>
            {
                { typeof(Rectangle), new LinkedList<DrawableElement>() },
                { typeof(Ellipse), new LinkedList<DrawableElement>() },
                { typeof(Pie), new LinkedList<DrawableElement>() },
                { typeof(PolyLine), new LinkedList<DrawableElement>() }
            };


            foreach (var drawable in graphData.drawables)
            {
                elements[drawable.GetType()].AddLast(drawable);
            }

            serializer.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults);
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Build().Serialize(writer, elements);
            }
        }
        public void OpenFile(string path, GraphData graphData)
        {
            graphData.drawables.Clear();
            var deserializer = new Deserializer();
            var serializer = new Serializer();
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    var data = deserializer.Deserialize<Dictionary<Type, LinkedList<object>>>(reader);
                    foreach (var drawableType in data)
                    {
                        var genericMethodInfo = deserializer.GetType().GetMethods().Single(method =>
                                method.Name == nameof(deserializer.Deserialize) &&
                                method.IsGenericMethod &&
                                method.GetParameters().Length == 1 &&
                                method.GetParameters()[0].ParameterType == typeof(string));

                        var genericMethod = genericMethodInfo.MakeGenericMethod(drawableType.Key);
                        foreach (var obj in drawableType.Value)
                        {
                            var str = serializer.Serialize(obj);
                            var result = genericMethod.Invoke(deserializer, new object[] { str });
                            graphData.drawables.AddLast((DrawableElement)result);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new FileFormatException("Не удалось прочитать файл");
                }

            }
            graphData.drawables.collection = new LinkedList<DrawableElement>(graphData.drawables.OrderBy(item => item.ZIndex));
        }
    }
}
