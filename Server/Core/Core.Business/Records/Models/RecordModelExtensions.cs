using Core.Common.Media;
using System.IO;

namespace Core.Business.Records.Models
{
    public static class RecordModelExtensions
    {
        private static readonly IExtensionToMediaTypeMapper _mapper = new ExtensionToMediaTypeMapper();

        public static void TrySetPreview(this RecordModel model)
        {
            var extension = Path.GetExtension(model.File?.RelativePath);
            var types = _mapper.Map(extension);
            if (types.Contains(MediaType.Image))
            {
                model.Preview = model.File;
            }
            // TODO: if audio, set predifined image
        }
    }
}
