using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Model
{
    public enum TickPeriod
    {
        Daily, Daily2, Daily4, Daily8, Hour4, Hour2, Hour, HalfHour, Quarter, FiveMinutes
    }

    public class OptimizerOptions
    {
        public IndicatorPair BestIndicatorPair { get; set; }
        public int RecursiveTests { get; set; }
        public int NrOfTestValues { get; set; }
        public bool IsSellEnabled { get; set; }
        public bool IsBuyEnabled { get; set; }
        public int MinOrders { get; set; }
        public int PositiveOrderPct { get; set; }
        //public OptimizerOption<int> MaxOrders { get; set; }
        public int MinProfitPct { get; set; }
        public decimal LoseLimitConstant { get; set; }
        public OptimizerOption<int> IndicatorLength { get; set; }


        private OptimizerOptions() { }

        public OptimizerOptions(
            int recursiveTests,
            int nrOfTestValues,
            bool isSellEnabled,
            bool isBuyEnabled,
            int minOrders,
            int positiveOrderPct,
            //OptimizerOption<int> maxOrders,
            int minProfitPct,
            decimal loseLimitConstant,
            OptimizerOption<int> indicatorLength
            )
        {
            this.RecursiveTests = recursiveTests;
            this.NrOfTestValues = nrOfTestValues;
            this.IsSellEnabled = isSellEnabled;
            this.IsBuyEnabled = isBuyEnabled;
            this.MinOrders = minOrders;
            this.PositiveOrderPct = positiveOrderPct;
            //this.MaxOrders = maxOrders;
            this.MinProfitPct = minProfitPct;
            this.LoseLimitConstant = loseLimitConstant;
            this.IndicatorLength = indicatorLength;
        }

        public static OptimizerOptions GetInstance(TickPeriod tickPeriod)
        {
            OptimizerOptions optimizerOptions = null;

            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    optimizerOptions = new OptimizerOptions()
                    {
                        RecursiveTests = 2,
                        NrOfTestValues = 90,
                        IsSellEnabled = true,
                        IsBuyEnabled = true,
                        MinOrders = 15,
                        //MaxOrders = maxOrders,
                        PositiveOrderPct = 75,
                        MinProfitPct = 55,
                        LoseLimitConstant = -0.12m,
                        IndicatorLength = GetIndicatorLength(tickPeriod)
                    };
                    break;
            }
            
            return optimizerOptions;
        }

        public static OptimizerOption<int> GetIndicatorLength(TickPeriod tickPeriod)
        {
            return new OptimizerOption<int>(4, 4, 64);
        }

        public class OptimizerOption<T>
        {
            public T IncrementIncrease { get; set; }
            public T Min { get; private set; }
            public T Max { get; private set; }
            public T Best { get; set; }

            public OptimizerOption(T best)
            {
                this.Best = best;
            }

            public OptimizerOption(T incrementIncrease, T min, T max)
            {
                this.IncrementIncrease = incrementIncrease;
                this.Max = max;
                this.Min = min;
                this.Best = ((dynamic)max + (dynamic)min) / 2;
            }
        }
    }
}
