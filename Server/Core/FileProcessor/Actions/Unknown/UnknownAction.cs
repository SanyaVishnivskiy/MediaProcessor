using FileProcessor.Actions.Base;
using System;

namespace FileProcessor.Actions.Unknown
{
    public class UnknownAction : IAction
    {
        public ActionType Type => ActionType.Unknown;

        public string InputPath { get; set; }

        public string OutputPath { get; set; }

        public string GenerateOuputFileName()
        {
            return Guid.NewGuid().ToString() + "_unknown";
        }
    }
}
