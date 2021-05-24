using FileProcessor.Actions.Base;
using System;
using System.IO;

namespace FileProcessor.Actions.Resize
{
    public class ResizeAction : IAction
    {
        public ActionType Type { get; set; } = ActionType.Resize;
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public string GenerateOuputFileName()
        {
            return !string.IsNullOrEmpty(OutputPath)
                ? OutputPath
                : Guid.NewGuid().ToString() + $"_resized_{Width}x{Height}"
                    + Path.GetExtension(InputPath);
        }
    }
}
