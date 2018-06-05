using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Import
{
    public static class CSV
    {
        public static Dictionary<string, List<string>> SplitCSV(string url)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            string fileList = GetCSV(url);
            fileList = fileList.Replace("\r", "");

            string[] splitted = fileList.Split('\n');
            List<string> names = new List<string>();

            //Collect Names For Dictionary
            foreach (string name in splitted[0].Split(','))
            {
                result[name.Trim('"')] = new List<string>();
                names.Add(name.Trim('"'));
            }

            //If Empty Remove Last
            if(names[names.Count-1].Length == 0)
            {
                result.Remove(names[names.Count - 1]);
                names.Remove(names[names.Count - 1]);
            }


            // If Last Value Is Empty
            int valuesCount = splitted.Length;

            if (splitted[valuesCount - 1].Length == 0)
            {
                valuesCount -= 1;
            }

            //Colect Values

                for (int i = 1; i < valuesCount; i++)
                {
                    string[] values = splitted[i].Split(',');

                    for (int j = 0; j < names.Count; j++)
                    {
                        result[names[j]].Add(values[j].Trim('"'));
                    }
                }

            return result;
        }

        private static string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }
    }
}
