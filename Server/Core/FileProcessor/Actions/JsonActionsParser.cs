using FileProcessor.Actions.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FileProcessor.Actions
{
    public class JsonActionsParser : IJsonActionsParser
    {
        private readonly IActionsMappings _mappings;

        public JsonActionsParser(IActionsMappings mappings)
        {
            _mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
        }

        public ActionsRequest Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
                return new ActionsRequest();

            var parsed = JObject.Parse(json);
            var result = new ActionsRequest();

            ParseRequestInfo(parsed, result);
            ParseActions(parsed, result);

            return result;
        }

        private void ParseRequestInfo(JObject parsed, ActionsRequest result)
        {
            result.RecordId = parsed["recordId"].Value<string>();
        }

        private void ParseActions(JObject parsed, ActionsRequest result)
        {
            var actions = new List<IAction>();
            var jActions = parsed["actions"] as JArray ?? new JArray();

            foreach (var jAction in jActions)
            {
                actions.Add(ParseActions(jAction));
            }

            result.Actions = actions;
        }

        private IAction ParseActions(JToken action)
        {
            var type = ParseType(action);
            var actionType = _mappings.GetActionType(type);
            return (IAction)JsonConvert.DeserializeObject(action.ToString(), actionType);
        }

        private ActionType ParseType(JToken action)
        {
            var type = action["type"].Value<string>();
            if (Enum.TryParse<ActionType>(type, true, out var result))
            {
                return result;
            }

            return ActionType.Unknown;
        }
    }
}
