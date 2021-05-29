using Core.Common.Media;
using Core.Common.Models.Configurations;
using FileProcessor.Actions;
using FileProcessor.Actions.Crop;
using FileProcessor.Actions.Preview;
using FileProcessor.Actions.Resize;
using FileProcessor.Actions.Trim;
using FileProcessor.Actions.Unknown;
using FileProcessor.Engines.FFMPEG;
using FileProcessor.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace FileProcessor.Composition
{
    public static class ActionsComposition
    {
        public static void ComposeActions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var processingConfig = GetProcessingConfig(services, configuration);
            Directory.CreateDirectory(processingConfig.Actions.BaseFilePath);

            ComposeFfmpeg(services, processingConfig);
            ComposeActions(services, processingConfig);
            ComposeHandlers(services);
        }

        private static void ComposeHandlers(IServiceCollection services)
        {
            services.AddTransient<ResizeActionHandler>();
            services.AddTransient<CropActionHandler>();
            services.AddTransient<TrimActionHandler>();
            services.AddTransient<GeneratePreviewActionHandler>();
            services.AddTransient<UnknownActionHandler>();
        }

        private static ProcessingConfiguration GetProcessingConfig(
            IServiceCollection services,
            IConfiguration configuration)
        {
            var processingConfig = new ProcessingConfiguration();
            configuration.GetSection("Processing").Bind(processingConfig);
            services.AddSingleton(processingConfig);

            return processingConfig;
        }

        private static void ComposeActions(IServiceCollection services, ProcessingConfiguration config)
        {
            services.AddTransient<IActionsFileStore, LocalFileStoreAdapter>(
                x => new LocalFileStoreAdapter(
                    new LocalFileStoreOptions { BaseFilePath = config.Actions.BaseFilePath}
            ));

            services.AddTransient<ILocalRecordsComponent, LocalRecordComponent>();
            services.AddTransient<IExtensionToMediaTypeMapper, ExtensionToMediaTypeMapper>();
            services.AddTransient<IActionsMappings, ActionsMappings>();
            services.AddTransient<IActionsHandlerFactory, ActionsHandlerFactory>();
            services.AddTransient<IActionsProcessorFacade, ActionsProcessorFacade>();
            services.AddTransient<IJsonActionsParser, JsonActionsParser>();
        }

        private static void ComposeFfmpeg(IServiceCollection services, ProcessingConfiguration processingConfig)
        {
            var ffmpegDiretory = Path.GetDirectoryName(processingConfig.Actions.FfmpegPath);
            Xabe.FFmpeg.FFmpeg.SetExecutablesPath(ffmpegDiretory);
            services.AddTransient<IFFmpegEngine, FFmpegEngine>();
        }
    }
}
