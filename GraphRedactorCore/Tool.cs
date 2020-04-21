using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRedactorCore
{
    internal abstract class Tool
    {
        public abstract void Use(ToolUsingArgs args);
        public abstract void StartUsing(ToolUsingArgs args);
        public abstract void NextPhase(ToolUsingArgs args);
        public abstract void StopUsing(ToolUsingArgs args);
    }
}
