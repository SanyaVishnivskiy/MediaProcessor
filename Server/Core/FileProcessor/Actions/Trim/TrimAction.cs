using Core.Common.Extensions;
using FileProcessor.Actions.Base;
using System;
using System.IO;

namespace FileProcessor.Actions.Trim
{
    public class TrimAction : IAction
    {
        public ActionType Type { get; set; } = ActionType.Trim;
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Duration => End.Add(-Start);
        public TimeSpan End { get; set; }

        public string GenerateOuputFileName()
        {
            return !string.IsNullOrEmpty(OutputPath)
                ? OutputPath.EnsureFileNameHasExtension(Path.GetExtension(InputPath))
                : Guid.NewGuid().ToString() + $"_trimmed_{Start}x{End}"
                    + Path.GetExtension(InputPath);
        }
    }
}
