using Newtonsoft.Json.Linq;
using System;

namespace Core.Common.Extensions
{
    public static class JObjectExtensions
    {
        public static T GetPropertyValueCaseInsensetive<T>(this JToken obj, string propName)
        {
            if (obj == null)
                return default;

            var value = TryGetJobjectValue<T>(obj, propName.ToCamelCase());
            if (value != null)
            {
                return value;
            }

            return TryGetJobjectValue<T>(obj, propName.ToPascalCase());
        }

        private static T TryGetJobjectValue<T>(JToken obj, string prop)
        {
            try
            {
                return obj[prop].Value<T>();
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}
