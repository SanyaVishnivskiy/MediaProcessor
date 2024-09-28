using FileProcessor.Actions.Base;
using FileProcessor.Engines.FFMPEG;
using System;
using System.Threading.Tasks;

namespace FileProcessor.Actions.Crop
{
    public class CropActionHandler : IActionHandler
    {
        private readonly IFFmpegEngine _engine;

        public CropActionHandler(IFFmpegEngine engine)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }

        public async Task<ActionHandlerResult> Handle(IAction action)
        {
            if (action is null)
                return ActionHandlerResult.Failed();

            if (!(action is CropAction))
                return ActionHandlerResult.Failed();

            //validate

            try
            {
                return await Handle(action as CropAction);
            }
            catch (Exception e)
            {
                return ActionHandlerResult.Failed(e);
            }
        }

        private async Task<ActionHandlerResult> Handle(CropAction action)
        {
            var arguments = $"-i {action.InputPath.Escape()} -vf \"crop = {action.Width}:{action.Height}:{action.X}:{action.Y}\" {action.OutputPath.Escape()}";
            await _engine.Execute(arguments);

            return ActionHandlerResult.Successful(action.OutputPath);
        }
    }
}
