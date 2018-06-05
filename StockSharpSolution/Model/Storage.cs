using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharpSolution.Model
{
    public class Storage
    {
        public string StoragePath { get; set; }
        public List<Candle> Candles { get; set; }

        public List<Candle> LoadCandles(DateTime startTime, DateTime stopTime)
        {
            List<Candle> candles = new List<Candle>();
            return null;
        }
    }
}
