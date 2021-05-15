namespace FileProcessor.Actions.Base
{
    public interface IAction
    {
        public ActionType Type { get; }

        public string InputPath { get; set; }

        public string OutputPath { get; set; }
    }

    public enum ActionType
    {
        Unknown = 0,
        Resize = 1,
    }
}
