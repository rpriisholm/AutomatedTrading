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
