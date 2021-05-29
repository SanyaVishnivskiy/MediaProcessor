using FileProcessor.Actions.Base;
using System;
using System.Threading.Tasks;

namespace FileProcessor.Actions.Trim
{
    public class TrimActionHandler : IActionHandler
    {
        public async Task<ActionHandlerResult> Handle(IAction action)
        {
            if (action is null)
                return ActionHandlerResult.Failed();

            if (!(action is TrimAction))
                return ActionHandlerResult.Failed();

            //validate

            try
            {
                return await Handle(action as TrimAction);
            }
            catch (Exception e)
            {
                return ActionHandlerResult.Failed(e);
            }
        }

        private async Task<ActionHandlerResult> Handle(TrimAction action)
        {
            var conversion = await Xabe.FFmpeg.FFmpeg.Conversions.FromSnippet
                .Split(
                    action.InputPath,
                    action.OutputPath,
                    action.Start,
                    action.Duration);

            var result = await conversion.Start();

            return ActionHandlerResult.Successful(action.OutputPath);
        }
    }
}
