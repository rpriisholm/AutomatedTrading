using StockSharp.Algo.Candles;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharp.Model
{
    public class Candle : TimeFrameCandle, IComparable
    {
        public string SecurityID { get; set; }

        public int CompareTo(object other)
        {
            return this.OpenTime.CompareTo(((Candle) other).OpenTime);
        }
    }
}
