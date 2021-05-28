using System.IO;

namespace Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return char.ToLower(value[0]) + value.Substring(1);
        }

        public static string EnsureFileNameHasExtension(this string value, string extensionIfNotExist)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var extension = Path.GetExtension(value);
            if (!string.IsNullOrEmpty(extension))
            {
                return value;
            }

            return value + extensionIfNotExist;
        }
    }
}
