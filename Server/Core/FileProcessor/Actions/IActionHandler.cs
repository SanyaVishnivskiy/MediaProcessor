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
        public Exception Exception { get; }
        public string ResultFilePath { get; }

        public ActionHandlerResult(
            bool success,
            Exception exception,
            string resultFilePath)
        {
            Success = success;
            Exception = exception;
            ResultFilePath = resultFilePath;
        }

        public static ActionHandlerResult Successful(string resultFilePath)
        {
            return new ActionHandlerResult(true, null, resultFilePath);
        }

        public static ActionHandlerResult Failed(Exception exception = null)
        {
            return new ActionHandlerResult(false, exception, null);
        }
    }
}
