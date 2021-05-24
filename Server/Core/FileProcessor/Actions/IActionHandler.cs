using FileProcessor.Actions.Base;
using System;
using System.Threading.Tasks;

namespace FileProcessor.Actions
{
    public interface IActionHandler
    {
        Task<ActionHandlerResult> Handle(IAction action);
    }

    public class ActionHandlerResult
    {
        public bool Success { get; }
        public bool CreateNewRecord { get; }
        public Exception Exception { get; }
        public string ResultFilePath { get; }

        public ActionHandlerResult(
            bool success,
            Exception exception,
            string resultFilePath,
            bool createNewRecord = true)
        {
            Success = success;
            Exception = exception;
            ResultFilePath = resultFilePath;
            CreateNewRecord = createNewRecord;
        }

        public static ActionHandlerResult Successful(string resultFilePath, bool createNewRecord = true)
        {
            return new ActionHandlerResult(true, null, resultFilePath, createNewRecord);
        }

        public static ActionHandlerResult Failed(Exception exception = null)
        {
            return new ActionHandlerResult(false, exception, null, false);
        }
    }
}
