using FileProcessor.Actions.Base;
using System;
using System.Threading.Tasks;

namespace FileProcessor.Actions.Unknown
{
    public class UnknownActionHandler : IActionHandler
    {
        public Task<ActionHandlerResult> Handle(IAction action)
        {
            throw new NotImplementedException();
        }
    }
}
