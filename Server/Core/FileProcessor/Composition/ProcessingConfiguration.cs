namespace FileProcessor.Composition
{
    public class ProcessingConfiguration
    {
        public ActionsConfiguration Actions { get; set; }
    }

    public class ActionsConfiguration
    {
        public string FfmpegPath { get; set; }
        public string BaseFilePath { get; set; }
    }
}
