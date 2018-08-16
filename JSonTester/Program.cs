
using Stocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSonTester
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
            /*
            public static void Main(string[] args)
            {
                //AlphaVantage - Just Open It In A Browser - Easier
                //PrintJson($"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=NVDA&outputsize=full&apikey=11EJMYL88Q4DW5BT");
                var stocks = Services.ExtractDaily_AlphaVantage("NVDA", Services.ColloctorType.Compact);
                Console.ReadKey();
            }

            //OR Use http://json2csharp.com/
            public static void PrintJson(string url)
            {
                var web = new WebClient();
                string json = web.DownloadString(url);

                var jsonObj = JObject.Parse(json);
                foreach(JToken jt in jsonObj.Children())
                {
                    int indexBracket = jt.ToString().IndexOf('{');
                    string type = jt.ToString().Substring(1, indexBracket-4);
                    string values = jt.ToString().Substring(indexBracket);

                    Console.WriteLine(type);
                    string[] valuesArray = values.Split('\n');
                    int nrOfLinesMax = 25;

                    for (int i = 0; i < valuesArray.Length && i < nrOfLinesMax; i++)
                    {
                        Console.WriteLine(valuesArray[i]);
                    }

                    if(valuesArray.Length > (nrOfLinesMax + 3))
                    {
                        Console.WriteLine("...");
                    }
                    Console.WriteLine();
                }

                //PrintValues(jsonObj.Children());
                Console.ReadLine();
            }

            public static void PrintValues(IJEnumerable<JToken> enumerable)
            {
                foreach (var value in enumerable)
                {
                    Console.WriteLine(value);
                    Console.ReadKey();
                    if (value.HasValues)
                    {
                        PrintValues(value.Values());
                    }

                }
            }
            */
        }
}