using Core.Common.Media;
using FileProcessor.Actions.Base;
using FileProcessor.Actions.Preview;
using FileProcessor.Actions.Resize;
using FileProcessor.Actions.Unknown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessor.Actions
{
    public class ActionsMappings : IActionsMappings
    {
        private const string StarSymbol = "*";

        private static readonly Dictionary<ActionType, ActionMap> _mappings = new Dictionary<ActionType, ActionMap>
        {
            { ActionType.Resize, new ActionMap(
                    new ResizeAction(),
                    typeof(ResizeActionHandler),
                    extensions: new [] { ".jpeg", ".png" },
                    mediaTypes: new MediaType[] { MediaType.Image, MediaType.Video}
                )},
            { ActionType.GeneratePreview, new ActionMap(
                    new GeneratePreviewAction(),
                    typeof(GeneratePreviewActionHandler),
                    mediaTypes: new MediaType[] { MediaType.Video}
                )},
            { ActionType.Unknown, new ActionMap(
                    new UnknownAction(),
                    typeof(UnknownActionHandler),
                    extensions: new [] { StarSymbol }
                )},
        };

        private static ExtensionToMediaTypeMapper _mapper = new ExtensionToMediaTypeMapper();

        public List<IAction> GetPossibleActionsByName(string recordPath)
        {
            var result = new List<IAction>();
            foreach (var mapping in _mappings)
            {
                var extension = GetExtension(recordPath);
                if (IsAllowedAction(mapping.Value, extension))
                {
                    result.Add(mapping.Value.Action);
                }
            }

            result.RemoveAll(x => x.Type == ActionType.Unknown);

            return result;
        }

        private bool IsAllowedAction(ActionMap mapping, string extension)
        {
            return IsAllowedExtension(mapping, extension)
                || IsAllowedMediaType(mapping, extension);
        }

        private bool IsAllowedMediaType(ActionMap map, string extension)
        {
            foreach (var mediaType in _mapper.Map(extension))
            {
                if (map.MediaTypes.Contains(mediaType))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsAllowedExtension(ActionMap map, string extension)
        {
            if (map.Extensions.Contains(extension))
            {
                return true;
            }

            if (map.Extensions.Contains(StarSymbol))
            {
                return true;
            }

            return false;
        }

        private string GetExtension(string recordPath)
        {
            return Path.GetExtension(recordPath);
        }

        public Type GetHandlerType(ActionType type)
        {
            if (_mappings.TryGetValue(type, out var value))
            {
                return value.HandlerType;
            }

            return _mappings[ActionType.Unknown].HandlerType;
        }

        public Type GetActionType(ActionType type)
        {
            if (_mappings.TryGetValue(type, out var value))
            {
                return value.Action.GetType();
            }

            return _mappings[ActionType.Unknown].Action.GetType();
        }

        private class ActionMap
        {
            public IAction Action { get; }
            public Type HandlerType { get; }
            public HashSet<string> Extensions { get; }
            public HashSet<MediaType> MediaTypes { get; }

            public ActionMap(
                IAction action,
                Type handlerType,
                string[] extensions = null,
                MediaType[] mediaTypes = null)
            {
                Action = action;
                HandlerType = handlerType;
                Extensions = extensions?.ToHashSet() ?? new HashSet<string>();
                MediaTypes = mediaTypes?.ToHashSet() ?? new HashSet<MediaType>();
            }
        }
    }
}