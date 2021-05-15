using System;
using System.Collections.Generic;
using System.Linq;
using MimeTypeMapper = MimeTypeMap.List.MimeTypeMap;

namespace FileProcessor.Actions.Base
{
    public enum MediaType
    {
        Other = 0,
        Audio = 1,
        Video = 2,
        Image = 3,
        Gif = 4,
    }

    public class ExtensionToMediaTypeMapper
    {
        public List<MediaType> Map(string extension)
        {
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
