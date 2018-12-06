﻿using System;
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
                    BestIndicatorPair = new IndicatorPair(null, null);
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
        //public int PositiveOrderPct { get; set; }
        //public OptimizerOption<int> MaxOrders { get; set; }
        public int MinProfitPct { get; set; }
        public decimal LoseLimitMin { get; set; }
        public int IndicatorMaxLength { get; set; }
        //public decimal LoseLimitConstant { get; set; }
        //public virtual OptimizerOption<int> IndicatorLength { get; set; }


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
            //int positiveOrderPct,
            //OptimizerOption<int> maxOrders,
            int minProfitPct,
            decimal loseLimitMin
            //decimal loseLimitConstant,
            //int IndicatorMaxLength
            //OptimizerOption<int> indicatorLength
            )
        {
            this.RecursiveTests = recursiveTests;
            this.NrOfTestValues = nrOfTestValues;
            this.IsSellEnabled = isSellEnabled;
            this.IsBuyEnabled = isBuyEnabled;
            this.MinOrders = minOrders;
            this.MaxOrders = maxOrders;
            //this.PositiveOrderPct = positiveOrderPct;
            this.MinProfitPct = minProfitPct;
            this.LoseLimitMin = loseLimitMin;
            //this.LoseLimitConstant = loseLimitConstant;
            //this.IndicatorMaxLength = indicatorLength;
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
                        IsSellEnabled = false,
                        IsBuyEnabled = true,
                        MinOrders = 5,
                        MaxOrders = 17,
                        //MaxOrders = maxOrders,
                        //PositiveOrderPct = 75,
                        MinProfitPct = 14,
                        //LoseLimitMin = -0.14m,
                        //LoseLimitConstant = -0.12m,
                        //IndicatorLength = GetIndicatorLength(tickPeriod)
                    };
                    break;
                    /*
                case TickPeriod.Daily:
                    optimizerOptions = new OptimizerOptions()
                    {
                        RecursiveTests = 2,
                        NrOfTestValues = 90,
                        IsSellEnabled = false,
                        IsBuyEnabled = true,
                        MinOrders = 10,
                        //MaxOrders = maxOrders,
                        PositiveOrderPct = 75,
                        MinProfitPct = 23,
                        LoseLimitConstant = -0.12m,
                        IndicatorLength = GetIndicatorLength(tickPeriod)
                    };
                    break;
                    */
            }
            
            return optimizerOptions;
        }

        public static OptimizerOption<int> GetIndicatorLength(TickPeriod tickPeriod)
        {
            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    return new OptimizerOption<int>(4, 4, 64);
            }

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
