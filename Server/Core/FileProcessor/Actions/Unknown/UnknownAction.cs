using FileProcessor.Actions.Base;

namespace FileProcessor.Actions.Unknown
{
    public class UnknownAction : IAction
    {
        public ActionType Type => ActionType.Unknown;

        public string InputPath { get; set; }

        public string OutputPath { get; set; }
    }
}
