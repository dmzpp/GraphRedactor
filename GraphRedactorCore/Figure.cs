﻿using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;

namespace GraphRedactorCore
{
    public abstract class Figure : IDrawable
    {
        protected int width;
        protected Color fillColor;
        protected Color contourColor;

        public abstract void Draw(WriteableBitmap bitmap);
        public abstract void AddPoint(Point point);
        public virtual bool StopEditng()
        {
            return false;
        }

    }
}
