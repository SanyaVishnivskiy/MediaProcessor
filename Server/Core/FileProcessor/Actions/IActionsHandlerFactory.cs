using FileProcessor.Actions.Base;
using System;

namespace FileProcessor.Actions
{
    public interface IActionsHandlerFactory
    {
        IActionHandler Create(IAction actions);
    }

    public class ActionsHandlerFactory : IActionsHandlerFactory
    {
        private readonly IServiceProvider _container;
        private readonly IActionsMappings _mappings;

        public ActionsHandlerFactory(
            IServiceProvider container,
            IActionsMappings mappings)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
        }

        public IActionHandler Create(IAction action)
        {
            var type = action?.Type ?? ActionType.Unknown;
            var handlerType = _mappings.GetHandlerType(type);
            return (IActionHandler)_container.GetService(handlerType);
        }
    }
}