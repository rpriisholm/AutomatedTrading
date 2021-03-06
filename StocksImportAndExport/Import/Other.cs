﻿using Newtonsoft.Json;
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
            return DownloadCSV(url, ',');
        }

        public static CsvContainer DownloadCSV(string url, char splitChar)
        {
            WebClient client = new WebClient();
            string data = client.DownloadString(url);
            return new CsvContainer(data, splitChar);
        }

        public static CsvContainer DownloadCSV(string url, char splitChar, char extraTrim, int skipLines)
        {
            WebClient client = new WebClient();
            string data = client.DownloadString(url);
            data = DeleteLines(data, skipLines);
            CsvContainer csv = new CsvContainer(data, splitChar, extraTrim);
            return csv;
        }

        public static string Download(string url)
        {
            WebClient client = new WebClient();
            string download = client.DownloadString(url);
            return client.DownloadString(url); ;
        }

        private static string DeleteLines(string s, int linesToRemove)
        {
            return s.Split(Environment.NewLine.ToCharArray(),
                           linesToRemove + 1
                ).Skip(linesToRemove)
                .FirstOrDefault();
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
                    string header2 = header.ToLower();
                    return this.HeaderAndRows[header2];
                }

                set
                {
                    this.HeaderAndRows[header.ToLower()] = value;
                }
            }

            public CsvContainer(string data) : this(data, ',') { }

            public CsvContainer(string data, char splitChar, char extraTrim) : this(data, splitChar)
            {
                int keyCount = this.Headers.Count;

                for (int i = 0; i < keyCount; i++)
                {
                    this.HeaderAndRows[Headers[0].Trim(extraTrim)] = new List<string>();
                    this.Headers.Add(Headers[0].Trim(extraTrim));

                    for(int j = 0; j < this[Headers[0]].Count; j++)
                    {
                        this.HeaderAndRows[this.Headers[0].Trim(extraTrim)].Add(this.HeaderAndRows[this.Headers[0]][j].Trim(extraTrim));
                        
                    }
                    this.HeaderAndRows.Remove(this.Headers[0]);
                    this.Headers.RemoveAt(0);
                }
            }

            public CsvContainer(string data, char splitChar)
            {
                string[] rows = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                List<string> headers = new List<string>();
                int startIndex = 0;
                foreach (string row in rows)
                {
                    string[] rowsArr = row.Split(splitChar);
                    
                    if(rowsArr.Length > 1)
                    {
                        foreach (string header in rowsArr)
                        {
                            if (header != null && header.Length != 0)
                            {
                                headers.Add(header);
                                this.HeaderAndRows[header.ToLower()] = new List<string>();
                                this.Headers.Add(header.ToLower());
                            }
                        }
                        break;
                    }
                    else
                    {
                        startIndex += 1;
                    }
                }
                //this.Headers = headers;

                for (int i = startIndex + 1; i < rows.Length; i++)
                {
                    string[] cells = rows[i].Split(splitChar);

                    if(cells.Length == headers.Count)
                    {
                        //Add Row
                        for (int j = 0; j < headers.Count; j++)
                        {
                            this.HeaderAndRows[headers[j].ToLower()].Add(cells[j]);
                        }
                    }
                }
            }

            public T GetCell<T>(string headerSearch, string keySearch, string headerResult) where T : new()
            {
                T result = default(T);
                int index = 0;
                foreach(string key in this[headerSearch])
                {
                    if(key.Equals(keySearch))
                    {
                        result = (T)Convert.ChangeType(key, typeof(T));
                        break;
                    }
                    else
                    {
                        index += 1;
                    }
                }

                return result;
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
