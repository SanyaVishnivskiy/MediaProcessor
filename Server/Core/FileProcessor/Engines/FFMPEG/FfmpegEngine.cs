using FFmpeg.NET;
using FileProcessor.Composition;
using System;
using System.Threading.Tasks;

namespace FileProcessor.Engines.FFMPEG
{
    public interface IFFmpegEngine
    {
        Task Execute(string arguments);
    }

    public class FFmpegEngine : IFFmpegEngine
    {
        private readonly ProcessingConfiguration _config;

        public FFmpegEngine(ProcessingConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public Task Execute(string arguments)
        {
            var engine = new Engine(_config.Actions.FfmpegPath);
            return engine.ExecuteAsync(arguments);
        }
    }
}
