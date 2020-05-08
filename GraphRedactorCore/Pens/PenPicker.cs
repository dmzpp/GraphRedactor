using System;
using System.Collections.Generic;

namespace GraphRedactorCore.Pens
{
    internal static class PenPicker
    {
        internal static readonly Dictionary<Type, ICustomPen> pens;
        private static Type _currentPen;
        static PenPicker()
        {
            pens = new Dictionary<Type, ICustomPen>();
            _currentPen = typeof(SolidPen);
        }

        public static Type CurrentType()
        {
            return _currentPen;
        }
        public static void AddPen(ICustomPen pen)
        {
            pens.Add(pen.GetType(), pen);
        }

        public static void RemovePen(ICustomPen pen)
        {
            pens.Remove(pen.GetType());
        }

        public static void SetPen(Type penType)
        {
            _currentPen = penType;
        }

        public static ICustomPen GetPen()
        {
            if (!pens.ContainsKey(_currentPen))
            {
                throw new Exception($"{_currentPen.Name} is not exist in that collection");
            }
            return pens[_currentPen];
        }
        public static ICustomPen GetPen(Type brushType)
        {
            if (!pens.ContainsKey(brushType))
            {
                throw new Exception($"{brushType.Name} is not exist in that collection");
            }
            return pens[brushType];
        }

    }
}
