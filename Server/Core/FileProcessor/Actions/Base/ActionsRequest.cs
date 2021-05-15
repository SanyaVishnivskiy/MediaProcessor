using System.Collections.Generic;

namespace FileProcessor.Actions.Base
{
    public class ActionsRequest
    {
        public string RecordId { get; set; }
        public List<IAction> Actions { get; set; }
    }
}
