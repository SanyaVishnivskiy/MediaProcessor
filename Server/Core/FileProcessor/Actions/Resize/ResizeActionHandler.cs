using FileProcessor.Actions.Base;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace FileProcessor.Actions.Resize
{
    public class ResizeActionHandler : IActionHandler
    {
        public async Task<ActionHandlerResult> Handle(IAction action)
        {
            if (action is null)
                return ActionHandlerResult.Failed();

            if (!(action is ResizeAction))
                return ActionHandlerResult.Failed();

            //validate

            try
            {
                return await Handle(action as ResizeAction);
            }
            catch (Exception e)
            {
                return ActionHandlerResult.Failed(e);
            }
        }

        private async Task<ActionHandlerResult> Handle(ResizeAction action)
        {
            var conversion = await FFmpeg.Conversions.FromSnippet
                .ChangeSize(
                    action.InputPath,
                    action.OutputPath,
                    action.Width,
                    action.Height);

            var result = await conversion.Start();

            return ActionHandlerResult.Successful(action.OutputPath);
        }
    }
}
