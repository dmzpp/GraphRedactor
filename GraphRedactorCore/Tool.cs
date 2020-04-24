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
