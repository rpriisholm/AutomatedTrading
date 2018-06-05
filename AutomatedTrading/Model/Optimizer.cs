using SandS.Algorithm.Library.SortNamespace;
using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StockSolution.Model.OptimizerOptions;

namespace StockSolution.Model
{
    public class Optimizer
    {
        private int _RecursiveTests = 2;
        public int RecursiveTests
        {
            get { return _RecursiveTests; }
            set { this._RecursiveTests = value; }
        }
        private int _DivideValue = 2;
        public int DivideValue
        {
            get { return _DivideValue; }
            set { this._DivideValue = value; }
        }

        public decimal _LoseLimitConstant = -0.1m;
        public decimal LoseLimitConstant
        {
            get { return _LoseLimitConstant; }
            set { this._LoseLimitConstant = value; }
        }

        public OptimizerOptions FindBestOptions(OptimizerOptions optimizerOptions, List<Candle> candles, int nrOfTestValues, int leverage)
        {
            if (candles.Count < (optimizerOptions.IndicatorLength.Max + nrOfTestValues * optimizerOptions.RecursiveTests))
            {
                return null;
            }

            List<Candle> initialCandles = candles.GetRange(candles.Count - (1 + optimizerOptions.IndicatorLength.Max + nrOfTestValues * optimizerOptions.RecursiveTests), optimizerOptions.IndicatorLength.Max);
            List<IndicatorPair> indicatorPairs = InitializeIndicatorPairs(initialCandles, optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);

            for (int recursiveTests = 0; recursiveTests < optimizerOptions.RecursiveTests; recursiveTests++)
            {
                //bool raceCondition = false;
                Parallel.ForEach(indicatorPairs, new ParallelOptions { MaxDegreeOfParallelism = 16 }, indicatorPair =>
                //foreach (IndicatorPair indicatorPair in indicatorPairs)
                {
                    int initialMoney = 100000;
                    int orderLimit = initialMoney / 10;
                    int maxInvestedPct = 80;

                    List<Candle> currentCandles = candles.GetRange(candles.Count - (1 + nrOfTestValues * (optimizerOptions.RecursiveTests - recursiveTests)), nrOfTestValues);

                    EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, leverage, maxInvestedPct);
                    StrategyGeneric strategyGeneric = new StrategyGeneric(emulationConnection, "TestID", indicatorPair.LongIndicator, indicatorPair.ShortIndicator, optimizerOptions.IsSellEnabled, optimizerOptions.IsBuyEnabled, optimizerOptions.LoseLimitConstant);

                    strategyGeneric.Start();
                    //Process Candles
                    for (int i = 0; i < currentCandles.Count; i++)
                    {
                        strategyGeneric.ProcessCandle(currentCandles[i]);
                    }
                    strategyGeneric.Stop();
                    //Set LastResult
                    indicatorPair.LastResult = strategyGeneric.ConnectionSecurityIDProfit() / orderLimit * 100;
                    //SET ORDERS OG POSITIVE ORDER PCT
                    indicatorPair.PositiveOrderPct = (int)strategyGeneric.AllPositiveOrdersPct();
                    indicatorPair.Orders = strategyGeneric.OrderCount;
                }
                );

                //Missing Recursive AND FILTER
                List<IndicatorPair> filteredIndicatorPairs = new List<IndicatorPair>();
                for (int i = 0; i < indicatorPairs.Count; i++)
                {
                    if (optimizerOptions.MinOrders <= indicatorPairs[i].Orders && optimizerOptions.MinProfitPct <= indicatorPairs[i].LastResult && optimizerOptions.PositiveOrderPct <= indicatorPairs[i].PositiveOrderPct)
                    {
                        filteredIndicatorPairs.Add(indicatorPairs[i]);
                    }
                }

                indicatorPairs = filteredIndicatorPairs;
            }

            optimizerOptions.BestIndicatorPair = indicatorPairs.Count > 0 ? SortingAlgorithm.MergeSort(indicatorPairs)[0] : null;
            return optimizerOptions;
        }


        //public int RecursiveTests { get; set; }

        private LengthIndicator<decimal> InitializeIndicator(ref List<Candle> initialCandles, LengthIndicator<decimal> lengthIndicator)
        {
            foreach (Candle candle in initialCandles)
            {
                lengthIndicator.Process(candle.ClosePrice);
            }

            return lengthIndicator;
        }

        private List<IndicatorPair> InitializeIndicatorPairs(List<Candle> initialCandles, int minIndicatorLength, int maxIndicatorLength, int interval)
        {
            List<IndicatorPair> indicatorPairs = new List<IndicatorPair>();
            List<LengthIndicator<decimal>> indicators = new List<LengthIndicator<decimal>>();

            int differentIndicators = 0;
            for (int length = minIndicatorLength; length < maxIndicatorLength; length += interval)
            {
                //Should WORK
                indicators.Add(new SimpleMovingAverage() { Length = length });
                //TEST
                indicators.Add(new SmoothedMovingAverage() { Length = length });
                indicators.Add(new ExponentialMovingAverage() { Length = length });
                indicators.Add(new Highest() { Length = length });
                indicators.Add(new HullMovingAverage() { Length = length });
                indicators.Add(new JurikMovingAverage() { Length = length });
                indicators.Add(new KaufmannAdaptiveMovingAverage() { Length = length });
                indicators.Add(new LinearReg() { Length = length });
                indicators.Add(new Lowest() { Length = length });
                indicators.Add(new MeanDeviation() { Length = length });
                indicators.Add(new Momentum() { Length = length });

                if (length == minIndicatorLength)
                {
                    differentIndicators = indicators.Count;
                }
            }

            foreach (LengthIndicator<decimal> indicator in indicators)
            {
                InitializeIndicator(ref initialCandles, indicator);
            }

            //All: 4, 8, 12, 16
            //All: 4 (skip same), 8, 12, 16

            for (int i = 0; i < indicators.Count; i += differentIndicators)
            {
                int shortIndicator = i;

                while (shortIndicator < (i + differentIndicators))
                {
                    int longIndicator = i;
                    while (longIndicator < indicators.Count)
                    {
                        if (shortIndicator != longIndicator)
                        {
                            if (!IsIndicatorPairDisabled(indicators[shortIndicator], indicators[longIndicator]))
                            {
                                LengthIndicator<decimal> shortIndicatorClone = indicators[shortIndicator].Clone() as LengthIndicator<decimal>;
                                LengthIndicator<decimal> longIndicatorClone = indicators[longIndicator].Clone() as LengthIndicator<decimal>;
                                indicatorPairs.Add(new IndicatorPair(shortIndicatorClone, longIndicatorClone));
                            }

                        }
                        longIndicator++;
                    }
                    shortIndicator++;
                }
            }


            /*       
            baseIndicators[i].Add(new LinearRegSlope() { Length = i });
            baseIndicators[i].Add(new IchimokuChinkouLine() { Length = i });
            baseIndicators[i].Add(new DoubleExponentialMovingAverage() { Length = i });
            baseIndicators[i].Add(new DetrendedPriceOscillator() { Length = i });
            baseIndicators[i].Add(new CommodityChannelIndex() { Length = i });
            baseIndicators[i].Add(new ChandeMomentumOscillator() { Length = i });
            baseIndicators[i].Add(new AlligatorLine() { Length = i });
            baseIndicators[i].Add(new MoneyFlowIndex() { Length = i });
            baseIndicators[i].Add(new NickRypockTrailingReverse() { Length = i });
            baseIndicators[i].Add(new OptimalTracking() { Length = i });
            baseIndicators[i].Add(new RelativeStrengthIndex() { Length = i });
            baseIndicators[i].Add(new RelativeVigorIndexAverage() { Length = i });
            baseIndicators[i].Add(new RelativeVigorIndexSignal() { Length = i });
            baseIndicators[i].Add(new RSquared() { Length = i });
            baseIndicators[i].Add(new StandardDeviation() { Length = i });
            baseIndicators[i].Add(new StandardError() { Length = i });
            baseIndicators[i].Add(new StochasticK() { Length = i });
            baseIndicators[i].Add(new Sum() { Length = i });
            baseIndicators[i].Add(new TripleExponentialMovingAverage() { Length = i });
            baseIndicators[i].Add(new VerticalHorizontalFilter() { Length = i });
            baseIndicators[i].Add(new Vidya() { Length = i });
            baseIndicators[i].Add(new VolumeWeightedMovingAverage() { Length = i });
            baseIndicators[i].Add(new WeightedMovingAverage() { Length = i });
            baseIndicators[i].Add(new WilliamsR() { Length = i });
            */

            return indicatorPairs;
        }

        private bool IsIndicatorPairDisabled(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            StringReader strReader = new StringReader(this._DisabledPairs);
            string indicatorPair = shortIndicator.ToString().Split(' ')[0] + " - " + longIndicator.ToString().Split(' ')[0];

            bool isDisabled = false;
            while (strReader.Peek() >= 0)
            {
                string disabledIndicatorPair = strReader.ReadLine();
                if (indicatorPair.Equals(disabledIndicatorPair))
                {
                    isDisabled = true;
                    break;
                }
            }

            return isDisabled;
        }

        private readonly string _DisabledPairs = @"Highest - LinearReg
MeanDeviation - Momentum
EMA - HMA
HMA - JMA
Momentum - Momentum
SMMA - JMA
KAMA - EMA
LinearReg - JMA
KAMA - SMA
KAMA - HMA
SMMA - SMA
EMA - SMA
Highest - HMA
HMA - LinearReg
LinearReg - HMA
HMA - HMA
Highest - Highest
KAMA - Lowest
KAMA - Highest
MeanDeviation - MeanDeviation
SMA - JMA
EMA - JMA
SMA - LinearReg
HMA - Lowest
SMMA - KAMA
SMA - HMA
LinearReg - Highest
JMA - HMA
EMA - LinearReg
Lowest - LinearReg
Highest - LinearReg
JMA - HMA
MeanDeviation - Momentum
EMA - HMA
HMA - JMA
Highest - HMA
Momentum - Momentum
KAMA - EMA
LinearReg - JMA
JMA - LinearReg
KAMA - LinearReg
SMA - EMA
KAMA - SMA
KAMA - HMA
SMMA - SMA
HMA - HMA
SMMA - Lowest
HMA - LinearReg
Highest - Highest
EMA - SMA
KAMA - Highest
MeanDeviation - MeanDeviation
SMA - JMA
EMA - JMA
HMA - Lowest
Lowest - LinearReg
SMA - HMA
LinearReg - Highest
EMA - LinearReg
Highest - LinearReg
JMA - HMA
MeanDeviation - Momentum
EMA - HMA
HMA - JMA
Highest - HMA
MeanDeviation - MeanDeviation
KAMA - EMA
Momentum - Momentum
LinearReg - JMA
JMA - LinearReg
KAMA - LinearReg
Highest - KAMA
SMA - EMA
KAMA - SMA
Lowest - JMA
SMMA - SMA
HMA - HMA
SMMA - Lowest
LinearReg - Lowest
Lowest - HMA
Highest - Highest
EMA - SMA
KAMA - HMA
SMA - JMA
EMA - JMA
SMA - LinearReg
Lowest - LinearReg
SMA - HMA
JMA - EMA
LinearReg - Highest
Highest - SMA
SMMA - HMA
Highest - LinearReg
JMA - HMA
MeanDeviation - Momentum
EMA - HMA
HMA - JMA
Highest - HMA
MeanDeviation - MeanDeviation
SMMA - JMA
KAMA - EMA
Momentum - Momentum
LinearReg - LinearReg
LinearReg - JMA
JMA - LinearReg
KAMA - LinearReg
Highest - KAMA
SMA - EMA
KAMA - SMA
SMMA - SMA
HMA - HMA
Lowest - HMA
LinearReg - SMA
Highest - SMA
Highest - Highest
EMA - SMA
Lowest - LinearReg
KAMA - HMA
SMA - JMA
EMA - JMA
SMA - LinearReg
HMA - Lowest
LinearReg - Lowest
SMA - HMA
Lowest - SMA
LinearReg - Highest
Highest - LinearReg
JMA - HMA
MeanDeviation - Momentum
EMA - HMA
HMA - JMA
Highest - HMA
MeanDeviation - MeanDeviation
SMMA - JMA
Momentum - Momentum
LinearReg - JMA
JMA - LinearReg
KAMA - LinearReg
SMA - EMA
KAMA - SMA
Lowest - LinearReg
SMMA - SMA
LinearReg - Lowest
HMA - HMA
Lowest - HMA
LinearReg - SMA
Highest - SMA
EMA - SMA
SMA - JMA
SMA - LinearReg
HMA - Lowest
SMMA - HMA
SMA - HMA
Lowest - SMA
Momentum - MeanDeviation
EMA - JMA
KAMA - HMA
EMA - LinearReg
Highest - LinearReg
EMA - HMA
HMA - JMA
Highest - HMA
MeanDeviation - MeanDeviation
SMMA - JMA
Momentum - Momentum
LinearReg - JMA
JMA - LinearReg
KAMA - LinearReg
SMA - EMA
KAMA - SMA
Lowest - LinearReg
SMMA - SMA
LinearReg - Lowest
HMA - LinearReg
Lowest - HMA
LinearReg - SMA
Highest - SMA
EMA - SMA
SMA - JMA
SMA - LinearReg
HMA - Lowest
SMMA - HMA
SMA - HMA
Lowest - SMA
Momentum - MeanDeviation
EMA - JMA
KAMA - HMA
HMA - EMA
";

    }
}
