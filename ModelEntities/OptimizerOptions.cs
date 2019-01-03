using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace StockSolution.ModelEntities.Models
{
    public class OptimizerOptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private IndicatorPair _BestIndicatorPair = null;

        public virtual IndicatorPair BestIndicatorPair
        {
            get
            {
                if((_BestIndicatorPair != null) == false)
                {
                    BestIndicatorPair = new IndicatorPair(null, null, decimal.MinValue);
                }
                return _BestIndicatorPair;
            }
            set
            {
                _BestIndicatorPair = value;
            }
        }

        public int RecursiveTests { get; set; }
        public int NrOfTestValues { get; set; }
        public bool IsSellEnabled { get; set; }
        public bool IsBuyEnabled { get; set; }
        public int MinOrders { get; set; }
        public int MaxOrders { get; set; }
        public decimal MinProfitPct { get; set; }
        public decimal LoseLimitMin { get; set; }

        private OptimizerOptions()
        {
            // this.BestIndicatorPair = new IndicatorPair(null, null);
        }

        public OptimizerOptions(
            int recursiveTests,
            int nrOfTestValues,
            bool isSellEnabled,
            bool isBuyEnabled,
            int minOrders,
            int maxOrders,
            decimal minProfitPct,
            decimal loseLimitMin
            )
        {
            this.RecursiveTests = recursiveTests;
            this.NrOfTestValues = nrOfTestValues;
            this.IsSellEnabled = isSellEnabled;
            this.IsBuyEnabled = isBuyEnabled;
            this.MinOrders = minOrders;
            this.MaxOrders = maxOrders;
            this.MinProfitPct = minProfitPct;
            this.LoseLimitMin = loseLimitMin;
        }

        public static OptimizerOptions GetInstance(TickPeriod tickPeriod)
        {
            OptimizerOptions optimizerOptions = null;

            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    int recursiveTests = 2;
                    int nrOfTestValues = 90;
                    bool isSellEnabled = false;
                    bool isBuyEnabled = true;
                    int minOrders = 3;
                    int maxOrders = 3;
                    int minProfitPct = 18;
                    decimal loseLimitMin = -0.09m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        isSellEnabled,
                        isBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin);
                    break;
            }
            
            return optimizerOptions;
        }

        /* Legacy
        public static OptimizerOption<int> GetIndicatorLength(TickPeriod tickPeriod)
        {
            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    return new OptimizerOption<int>(4, 4, 64);
            }

            return new OptimizerOption<int>(4, 4, 64);
        }
        */

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
