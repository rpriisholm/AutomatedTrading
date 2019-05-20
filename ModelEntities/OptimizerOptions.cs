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

        public static bool IsSellEnabled = false;
        public static bool IsBuyEnabled = true;
        public string EnabledPairs = null; 

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
        //public bool IsSellEnabled { get; set; }
        //public bool IsBuyEnabled { get; set; }
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
            //bool isSellEnabled,
            //bool isBuyEnabled,
            int minOrders,
            int maxOrders,
            decimal minProfitPct,
            decimal loseLimitMin,
            string enabledPairs
            )
        {
            this.RecursiveTests = recursiveTests;
            this.NrOfTestValues = nrOfTestValues;
            //this.IsSellEnabled = isSellEnabled;
            //this.IsBuyEnabled = isBuyEnabled;
            this.MinOrders = minOrders;
            this.MaxOrders = maxOrders;
            this.MinProfitPct = minProfitPct;
            this.LoseLimitMin = loseLimitMin;
            this.EnabledPairs = enabledPairs;
        }

        /* USED for locating best matches */
        public static List<OptimizerOptions> GetInstances(TickPeriod tickPeriod)
        {
            List<OptimizerOptions> list = null;
            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    list = new List<OptimizerOptions>();

                    // AvgIndicatorMin: 21 - MinResult: 31.00 - MinOrders: 21 - MaxOrders: 21 - LoseLimitMin: -0.36
                    //AvgIndicatorMin 47%
                    int recursiveTests = 2;
                    int nrOfTestValues = 90;
                    int minOrders = 21;
                    int maxOrders = 21;
                    int minProfitPct = 31;
                    decimal loseLimitMin = -0.36m;
                    OptimizerOptions optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator21
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 19 - MinResult: 44.00 - MinOrders: 17 - MaxOrders: 17 - LoseLimitMin: -0.16
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 17;
                    maxOrders = 17;
                    minProfitPct = 44;
                    loseLimitMin = -0.16m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator19
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 21 - MinResult: 29.00 - MinOrders: 20 - MaxOrders: 20 - LoseLimitMin: -0.29
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 20;
                    maxOrders = 20;
                    minProfitPct = 29;
                    loseLimitMin = -0.29m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator21
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 17 - MinResult: 83.00 - MinOrders: 5 - MaxOrders: 5 - LoseLimitMin: -0.20
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 5;
                    maxOrders = 5;
                    minProfitPct = 83;
                    loseLimitMin = -0.20m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator17
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 19 - MinResult: 35.00 - MinOrders: 18 - MaxOrders: 18 - LoseLimitMin: -0.13
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 18;
                    maxOrders = 18;
                    minProfitPct = 35;
                    loseLimitMin = -0.13m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator19
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 21 - MinResult: 30.00 - MinOrders: 23 - MaxOrders: 23 - LoseLimitMin: -0.14
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 23;
                    maxOrders = 23;
                    minProfitPct = 30;
                    loseLimitMin = -0.14m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator21
                        );

                    list.Add(optimizerOptions);

                    // AvgIndicatorMin: 19 - MinResult: 25.00 - MinOrders: 22 - MaxOrders: 22 - LoseLimitMin: -0.11
                    recursiveTests = 2;
                    nrOfTestValues = 90;
                    minOrders = 22;
                    maxOrders = 22;
                    minProfitPct = 25;
                    loseLimitMin = -0.11m;
                    optimizerOptions = new OptimizerOptions(
                        recursiveTests,
                        nrOfTestValues,
                        //IsSellEnabled,
                        //IsBuyEnabled,
                        minOrders,
                        maxOrders,
                        minProfitPct,
                        loseLimitMin,
                        ValueCollections.PermanentValues.EnabledPairs_AvgIndicator19
                        );

                    list.Add(optimizerOptions);

                    break;
            }
            
            return list;
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
