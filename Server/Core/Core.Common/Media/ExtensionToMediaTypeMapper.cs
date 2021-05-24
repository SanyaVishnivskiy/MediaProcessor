using Core.Common.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using MimeTypeMapper = MimeTypeMap.List.MimeTypeMap;

namespace Core.Common.Media
{
    public interface IExtensionToMediaTypeMapper
    {
        List<MediaType> Map(string extension);
    }

    public class ExtensionToMediaTypeMapper : IExtensionToMediaTypeMapper
    {
        public List<MediaType> Map(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return new List<MediaType>();

            var types = MimeTypeMapper.GetMimeType(extension);
            return types
                .Select(MapMimeType)
                .Distinct()
                .ToList();
        }

        private MediaType MapMimeType(string mimeType)
        {
            var type = mimeType.Split("/")[0];
            if (Enum.TryParse<MediaType>(type, true, out var result))
            {
                return result;
            }

            return MediaType.Other;
        }
    }
}
