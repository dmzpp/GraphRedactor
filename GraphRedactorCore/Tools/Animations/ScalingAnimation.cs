using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools.Animations
{
    internal class ScalingAnimation : Animation
    {
        public double Speed { get => _speed; set => _speed = value; }
        private double _speed = 0;
        private double _maxScale = 0;
        private double _minScale = 0;

        private bool isIncrease = true;
        public override void Tick()
        {
            if (isIncrease) {
                _drawable.AnimationScale += _speed;
            }
            else
            {
                _drawable.AnimationScale -= _speed;
            }

            if(_drawable.AnimationScale > _maxScale)
            {
                isIncrease = false;
            }
            else if(_drawable.AnimationScale < _minScale)
            {
                isIncrease = true;
            }
        }

        public ScalingAnimation(DrawableElement drawable)
        {
            _drawable = drawable;
        }
        public ScalingAnimation(DrawableElement drawable, double speed)
        {
            _drawable = drawable;
            _maxScale = (_drawable.AnimationScale * speed / 10) + _drawable.AnimationScale;
            _minScale = _drawable.AnimationScale;
            _speed = speed / 100;
        }
    }
}
