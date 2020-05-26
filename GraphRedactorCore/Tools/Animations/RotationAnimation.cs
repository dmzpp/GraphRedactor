using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools.Animations
{
    internal class RotationAnimation : Animation
    {
        public double Speed { get => _speed; set => _speed = value; }
        private double _speed;
        public override void Tick()
        {
            _drawable.RotateAngle += _speed;
        }

        public RotationAnimation(DrawableElement drawable)
        {
            _drawable = drawable;
        }
        public RotationAnimation(DrawableElement drawable, double speed)
        {
            _drawable = drawable;
            _speed = speed;
        }
    }
}
