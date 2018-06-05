using GenericTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public static partial class Services
    {
        public static Stocks_AlphaVantage ExtractDaily_AlphaVantage(string symbol, ColloctorType type)
        {
            string function = $"TIME_SERIES_DAILY_ADJUSTED";
            return ExtractStocks_AlphaVantage(function, symbol, type);
        }

        public static Stocks_AlphaVantage ExtractStocks_AlphaVantage(string function, string symbol, ColloctorType type)
        {
            Stocks_AlphaVantage stocks = null;
            string outputSize = ""; 
            
            switch(type)
            {
                case (ColloctorType.Full):
                    outputSize = $"full";
                    break;
                case (ColloctorType.Compact):
                    outputSize = $"compact";
                    break;
            }

            WebClient web = new WebClient();
            string url = $"https://www.alphavantage.co/query?function=" + function + $"&symbol=" + symbol + $"&outputsize=" + outputSize + "&apikey=11EJMYL88Q4DW5BT";
            string json = web.DownloadString(url);

            JObject jsonObj = JObject.Parse(json);

            foreach (JToken tk in jsonObj.Children())
            {
                string key = JSON.GetQuotedContent(tk.ToString());
                int intervalSec = 0;

                /*
                if (Stocks_AlphaVantage.ValidateKey(key, ref intervalSec))
                {
                    string name = "";
                    stocks = Stocks_AlphaVantage.GetInstance(key, symbol, name, intervalSec);
                    stocks.StockEntities = InitializeList_AlphaVantage(tk.Children(), intervalSec);
                }
                */
            }

            return stocks;
        }

        //Accepts JToken For Multiple Instances
        private static List<StockEntity_AlphaVantage> InitializeList_AlphaVantage(JEnumerable<JToken> tokens, int intervalSec)
        {
            List<StockEntity_AlphaVantage> seavList = new List<StockEntity_AlphaVantage>();

            foreach (JToken tk1 in tokens)
            {
                foreach (JToken tk2 in tk1.Children())
                {
                    string key = JSON.GetQuotedContent(tk2.ToString());
                    JEnumerable<JToken> values = tk2.Children();
                    Node<JEnumerable<JToken>> node = new Node<JEnumerable<JToken>>(key, values);
                    StockEntity_AlphaVantage seav = Initialize_AlphaVantage(node, intervalSec);
                    seavList.Add(seav);
                }
            }

            return seavList;
        }

        //Accepts JToken For A Single Instance
        private static StockEntity_AlphaVantage Initialize_AlphaVantage(Node<JEnumerable<JToken>> node, int intervalSec)
        {
            StockEntity_AlphaVantage seav = new StockEntity_AlphaVantage();

            seav.StartTime = DateTime.Parse(node.Key);
            seav.EndTime = seav.StartTime.AddSeconds(intervalSec);

            foreach (JToken tk1 in node.Value)
            {
                foreach (JToken tk2 in tk1.Children())
                {
                    string key = JSON.GetQuotedContent(tk2.ToString());
                    string value = JSON.GetNextQuotedContent(tk2.ToString());

                    switch (key)
                    {
                        case ("1. open"):
                            seav.Start = double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                        case ("4. close"):
                            seav.End = double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                        case ("2. high"):
                            seav.High = double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                        case ("3. low"):
                            seav.Low = double.Parse(value, CultureInfo.InvariantCulture);
                            break;
                    }
                }
            }

            return seav;
        }
    }
}
