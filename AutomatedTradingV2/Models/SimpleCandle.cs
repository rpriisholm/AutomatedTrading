using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Models
{
    public class SimpleCandle
    {
        public TimeSpan TimeFrame { get; set; }
        public DateTime CloseTime { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal LastPrice { get; set; }

        public int CompareTo(object other)
        {
            return this.OpenTime.CompareTo(((SimpleCandle)other).OpenTime);
        }
    }
}
