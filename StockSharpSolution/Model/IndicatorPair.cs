using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharpSolution.Model
{
    public class IndicatorPair
    {
        public LengthIndicator<decimal> ShortIndicator;
        public LengthIndicator<decimal> LongIndicator;

        public IndicatorPair(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            this.ShortIndicator = shortIndicator;
            this.LongIndicator = longIndicator;
        }
    }
}
