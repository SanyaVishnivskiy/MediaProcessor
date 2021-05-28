using Core.Business.Files.Component.Models;
using Core.Business.Records.Facade;
using Core.Common.Media;
using FileProcessor.Actions.Base;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace FileProcessor.Actions.Preview
{
    public class GeneratePreviewActionHandler : IActionHandler
    {
        private readonly IExtensionToMediaTypeMapper _typeMapper;
        private readonly IRecordsComponentFacade _facade;

        public GeneratePreviewActionHandler(
            IExtensionToMediaTypeMapper typeMapper,
            IRecordsComponentFacade facade)
        {
            _typeMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));
            _facade = facade ?? throw new ArgumentNullException(nameof(facade));
        }

        public async Task<ActionHandlerResult> Handle(IAction action)
        {
            if (action is null)
                return ActionHandlerResult.Failed();

            if (!(action is GeneratePreviewAction))
                return ActionHandlerResult.Failed();

            //validate

            try
            {
                return await Handle(action as GeneratePreviewAction);
            }
            catch (Exception e)
            {
                return ActionHandlerResult.Failed(e);
            }
        }

        private async Task<ActionHandlerResult> Handle(GeneratePreviewAction action)
        {
            EnsureActionOutputIsImage(action);
            var previewPath = await GeneratePreview(action);

            if (previewPath == null)
                return ActionHandlerResult.Successful(action.OutputPath);

            await UpdateRecordPreview(action, previewPath);

            return ActionHandlerResult.Successful(action.OutputPath, createNewRecord: false);
        }

        private async Task UpdateRecordPreview(GeneratePreviewAction action, string previewPath)
        {
            using (var stream = File.OpenRead(previewPath))
            {
                await _facade.SetPreview(action.RecordId, new FileModel
                {
                    Stream = stream,
                    FileName = Path.GetFileName(previewPath)
                });
            }
        }

        private void EnsureActionOutputIsImage(GeneratePreviewAction action)
        {
            action.OutputPath = Path.Combine(
                Path.GetDirectoryName(action.OutputPath),
                Path.GetFileNameWithoutExtension(action.OutputPath) + ".jpg");
        }

        private Task<string> GeneratePreview(GeneratePreviewAction action)
        {
            var mediaType = _typeMapper.Map(Path.GetExtension(action.InputPath));
            if (mediaType.Contains(MediaType.Video) || mediaType.Contains(MediaType.Gif))
            {
                return GenerateForVideo(action);
            }
            //if (mediaType.Contains(MediaType.Image))
            //{
            //    //
            //}

            return Task.FromResult<string>(null);
        }

        private async Task<string> GenerateForVideo(GeneratePreviewAction action)
        {
            var info = await Xabe.FFmpeg.FFmpeg.GetMediaInfo(action.InputPath);
            var snapshotTime = GetSnapshotTime(action, info.VideoStreams.First());

            var conversion = await Xabe.FFmpeg.FFmpeg.Conversions.FromSnippet
               .Snapshot(
                   action.InputPath,
                   action.OutputPath,
                   snapshotTime);

            var result = await conversion.Start();

            return action.OutputPath;
        }

        private TimeSpan GetSnapshotTime(GeneratePreviewAction action, IVideoStream stream)
        {
            if (action.TimeOfSnapshot.HasValue)
            {
                return action.TimeOfSnapshot.Value;
            }

            var snapshotTime = stream.Duration.TotalMilliseconds / 5;
            return TimeSpan.FromMilliseconds(snapshotTime);
        }
    }
}
