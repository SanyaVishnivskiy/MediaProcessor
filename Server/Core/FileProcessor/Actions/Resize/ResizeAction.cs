using FileProcessor.Actions.Base;

namespace FileProcessor.Actions.Resize
{
    public class ResizeAction : IAction
    {
        public ActionType Type { get; set; } = ActionType.Resize;
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
