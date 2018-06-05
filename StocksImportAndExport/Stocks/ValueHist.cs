using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Stocks
{
    public class ValueHist
    {
        public DateTime StartTime
        {
            get;
            set;
        }

        public double Low
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public ValueHist(DateTime startTime, double low, double high)
        {
            this.StartTime = startTime;
            this.Low = low;
            this.High = high;
        }
    }
}
