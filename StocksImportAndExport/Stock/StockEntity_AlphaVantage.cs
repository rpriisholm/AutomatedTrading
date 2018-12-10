using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericTypes;
using System.Globalization;

namespace Stocks
{
    public class StockEntity_AlphaVantage
    {
        private static readonly StockEntity_AlphaVantage Instance = new StockEntity_AlphaVantage();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public double Low { get; set; }
        public double High { get; set; }

        public StockEntity_AlphaVantage() { }

        public StockEntity_AlphaVantage(DateTime startTime, DateTime endTime, double start, double end, double low, double high)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Start = start;
            this.End = end;
            this.Low = low;
            this.High = high;
        }
    }
}

