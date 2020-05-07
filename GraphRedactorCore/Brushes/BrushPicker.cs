using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Brushes
{
    internal static class BrushPicker
    {
        internal static readonly Dictionary<Type, ICustomBrush> brushes;
        private static Type _currentBrush;
        static BrushPicker()
        {
            brushes = new Dictionary<Type, ICustomBrush>();
            _currentBrush = typeof(RectangleBrush);
        }

        public static Type CurrentType()
        {
            return _currentBrush;
        }
        public static void AddBrush(ICustomBrush brush)
        {
            brushes.Add(brush.GetType(), brush);
        }

        public static void RemoveBrush(ICustomBrush brush)
        {
            brushes.Remove(brush.GetType());
        }

        public static void SetBrush(Type toolType)
        {
            _currentBrush = toolType;
        }

        public static ICustomBrush GetBrush()
        {
            if (!brushes.ContainsKey(_currentBrush))
            {
                throw new Exception($"{_currentBrush.Name} is not exist in that collection");
            }
            return brushes[_currentBrush];
        }
        public static ICustomBrush GetBrush(Type brushType)
        {
            if (!brushes.ContainsKey(brushType))
            {
                throw new Exception($"{brushType.Name} is not exist in that collection");
            }
            return brushes[brushType];
        }

    }
}
