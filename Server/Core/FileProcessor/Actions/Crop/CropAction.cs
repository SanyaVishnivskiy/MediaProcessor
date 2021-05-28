using Core.Common.Extensions;
using FileProcessor.Actions.Base;
using System;
using System.IO;

namespace FileProcessor.Actions.Crop
{
    public class CropAction : IAction
    {
        public ActionType Type { get; set; } = ActionType.Crop;
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string GenerateOuputFileName()
        {
            return !string.IsNullOrEmpty(OutputPath)
                ? OutputPath.EnsureFileNameHasExtension(Path.GetExtension(InputPath))
                : Guid.NewGuid().ToString() + $"_cropped_{Width}x{Height}"
                    + Path.GetExtension(InputPath);
        }
    }
}
