using FileProcessor.Actions.Base;
using System;
using System.Collections.Generic;
using MimeTypeMappings = MimeTypeMap.List.MimeTypeMap;

namespace FileProcessor.Actions
{
    public interface IActionsMappings
    {
        List<IAction> GetPossibleActionsByName(string recordPath);
        Type GetHandlerType(ActionType type);
        Type GetActionType(ActionType type);
    }
}