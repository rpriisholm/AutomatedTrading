using GenericTypes;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Stocks
{
    public class JSON
    {
        public static string GetQuotedContent(string input)
        {
            int index = input.IndexOf('"');
            string content = input.Substring(index+1);
            index = content.IndexOf('"');
            content = content.Substring(0, index);

            return content;
        }

        public static string GetNextQuotedContent(string input)
        {
            string content = input.Substring(AfterIndexFirstQuot(input));
            return GetQuotedContent(content);
        }

        private static int AfterIndexFirstQuot(string input)
        {
            int index = input.IndexOf('"');
            input = input.Substring(index+1);
            index += input.IndexOf('"');

            return index + 2;
        }
    }
}
