using System.Linq;

namespace Core.Common.Media
{
    public static class StringExtensions
    {
        public static string Escape(this string output)
        {
            if (output == null)
            {
                return output;
            }

            if ((output.Last() == '"' && output.First() == '"') || (output.Last() == '\'' && output.First() == '\''))
            {
                output = output.Substring(1, output.Length - 2);
            }

            output = "\"" + output + "\"";
            return output;
        }
    }
}
