using GenericTypes;
using Newtonsoft.Json.Linq;
using Stocks.Stocks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public abstract class AStock
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int IntervalSec { get; set; }
        public List<AStock> StockEntities { get; set; }
        public List<ValueHist> ValueHistory { get; set; }
    }

    public class Stocks_AlphaVantage : AStock
    {
        public Stocks_AlphaVantage()
        {

        }

        public static Stocks_AlphaVantage GetInstance(string key, string symbol, string name, int intervalSec)
        {
            Stocks_AlphaVantage stocks = new Stocks_AlphaVantage();
            stocks.Symbol = symbol;
            stocks.Name = name;
            stocks.IntervalSec = intervalSec;

            return stocks;
        }
    }
}
