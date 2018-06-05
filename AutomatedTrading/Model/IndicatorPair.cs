using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public class IndicatorPair : IComparable<IndicatorPair>, IComparable
    {
        public LengthIndicator<decimal> ShortIndicator;
        public LengthIndicator<decimal> LongIndicator;
        public decimal LastResult { get; set; }
        public int Orders { get; set; }
        public int PositiveOrderPct { get; set; }

        public IndicatorPair(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            this.ShortIndicator = shortIndicator;
            this.LongIndicator = longIndicator;
            this.LastResult = decimal.MinValue;
            this.Orders = int.MinValue;
            this.PositiveOrderPct = int.MinValue;
        }

        public int CompareTo(IndicatorPair other)
        {
            if(this.LastResult > other.LastResult)
            {
                return -1;
            }

            if (this.LastResult < other.LastResult)
            {
                return 1;
            } 

            return 0;
        }

        public int CompareTo(object other)
        {
            return this.CompareTo((IndicatorPair) other);
        }
    }
}
