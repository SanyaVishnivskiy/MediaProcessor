using FileProcessor.Actions.Base;

namespace FileProcessor.Actions
{
    public interface IJsonActionsParser
    {
        ActionsRequest Parse(string json);
    }
}
