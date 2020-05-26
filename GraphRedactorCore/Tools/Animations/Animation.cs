using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore.Tools.Animations
{
    internal abstract class Animation
    {
        protected DrawableElement _drawable;
        public abstract void Tick();
    }
}
