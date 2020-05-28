using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools.Animations
{
    internal class MovingAnimation : Animation
    {
        public double Speed { get => _speed; set => _speed = value; }
        private double _speed = 0;
        private double _ticks = 0;
        public override void Tick()
        {
            _ticks = (_ticks + 1) % 360;
            _drawable.OffsetX = Math.Sin(_ticks / Math.PI) * _speed;
            _drawable.OffsetY = Math.Cos(_ticks / Math.PI) * _speed;
        }

        public MovingAnimation(DrawableElement drawable)
        {
            _drawable = drawable;
        }

        public MovingAnimation(DrawableElement drawable, double speed)
        {
            _drawable = drawable;
            _speed = speed;
        }
    }
}
