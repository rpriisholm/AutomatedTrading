using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Import
{
    public static class Other
    {
        public static void DownloadFile(string url, string path, string start)
        {
            WebClient client = new WebClient();
            string content = start + client.DownloadString(url);
            File.WriteAllText(path, content);
        }

        public static void DownloadFile(string url, string path)
        {
            WebClient client = new WebClient();
            client.DownloadFile(url, path);
        }

        public static CsvContainer DownloadCSV(string url)
        {
            WebClient client = new WebClient();
            string data = client.DownloadString(url);
            return new CsvContainer(data);
        }

        public static string Download(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        public static CsvContainer JsonUrlToCSV(string url)
        {
            WebClient client = new WebClient();
            string data = client.DownloadString(url);
            var jsonContent = (JArray)JsonConvert.DeserializeObject(data);
            var csv = ServiceStack.Text.CsvSerializer.SerializeToCsv(jsonContent);
            return new CsvContainer(csv);
        }

        public class CsvContainer
        {
            public List<string> Headers = new List<string>();
            private Dictionary<string, List<string>> HeaderAndRows = new Dictionary<string, List<string>>();

            public List<string> this[string header]
            {
                get
                {
                    return this.HeaderAndRows[header.ToLower()];
                }

                set
                {
                    this.HeaderAndRows[header.ToLower()] = value;
                }
            }

            public CsvContainer(string data)
            {
                string[] rows = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                List<string> headers = new List<string>();
                foreach (string header in rows[0].Split(','))
                {
                    headers.Add(header);
                    this.HeaderAndRows[header.ToLower()] = new List<string>();
                }
                this.Headers = headers;

                for (int i = 1; i < rows.Length; i++)
                {
                    string[] cells = rows[i].Split(',');

                    //Add Row
                    for (int j = 0; j < headers.Count; j++)
                    {
                        this.HeaderAndRows[headers[j].ToLower()].Add(cells[j]);
                    }
                }

            }
        }

        public static StreamReader GetStreamReader(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader streamReader = new StreamReader(stream);
            /*** Read Example ***
            
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // do stuff
                }
            */
            return streamReader;
        }

    }
}
