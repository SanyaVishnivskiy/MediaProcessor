using FileProcessor.Actions.Base;
using System;
using System.IO;

namespace FileProcessor.Actions.Preview
{
    public class GeneratePreviewAction : IAction
    {
        public ActionType Type { get; } = ActionType.GeneratePreview;
        public string RecordId { get; set; }
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public TimeSpan? TimeOfSnapshot { get; set; }

        public string GenerateOuputFileName()
        {
            return Guid.NewGuid().ToString() + $"_preview.jpg";
        }
    }
}
