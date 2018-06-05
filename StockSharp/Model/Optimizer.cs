using SandS.Algorithm.Library.SortNamespace;
using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharp.Model
{
    public class Optimizer
    {
        public List<IndicatorPair> GetIndicatorPairs()
        {
            List<IndicatorPair> indicatorPairs = new List<IndicatorPair>();
            List<LengthIndicator<decimal>> indicators = new List<LengthIndicator<decimal>>();
            indicators.Add(new ExponentialMovingAverage());
            indicators.Add(new Highest());
            indicators.Add(new HullMovingAverage());
            indicators.Add(new JurikMovingAverage());
            indicators.Add(new KaufmannAdaptiveMovingAverage());
            indicators.Add(new LinearReg());
            indicators.Add(new Lowest());
            indicators.Add(new MeanDeviation());
            indicators.Add(new Momentum());


            int shortMax = 64;
            int longMax = 64;
            for (int i = 4; i < shortMax; i += 4)
            {
                foreach (var shortIndicator in indicators)
                {
                    for (int j = i; j < longMax; j += 4)
                    {
                        foreach (var longIndicator in indicators)
                        {
                            if (i <= j)
                            {
                                if (shortIndicator.GetType() == longIndicator.GetType() && i == j) { }
                                else
                                {
                                    LengthIndicator<decimal> sIndicator = (LengthIndicator<decimal>)(shortIndicator.Clone());
                                    LengthIndicator<decimal> lIndicator = (LengthIndicator<decimal>)(longIndicator.Clone());

                                    sIndicator.Length = i;
                                    lIndicator.Length = j;
                                    indicatorPairs.Add(new IndicatorPair(sIndicator, lIndicator));
                                }
                            }
                        }
                    }
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
            baseIndicators[i].Add(new SimpleMovingAverage() { Length = i });
            baseIndicators[i].Add(new SmoothedMovingAverage() { Length = i });
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

        public IndicatorPair CurrentBestStrategy(string security, List<Candle> candles, int maxIndicatorLength, int nrOfTestValues)
        {
            if (candles.Count < (maxIndicatorLength + nrOfTestValues * 3))
            {
                return null;
            }

            List<IndicatorPair> indicatorPairs = GetIndicatorPairs();
            List<IndicatorPair> topIndicatorPairs = null;

            List<Candle> initialCandles = candles.GetRange(candles.Count - (1 + maxIndicatorLength + nrOfTestValues * 3), maxIndicatorLength);
            List<Candle> firstTest = candles.GetRange(candles.Count - (1 + nrOfTestValues * 3), nrOfTestValues);
            List<Candle> secondTest = candles.GetRange(candles.Count - (1 + nrOfTestValues * 2), nrOfTestValues);
            List<Candle> thirdTest = candles.GetRange(candles.Count - (1 + nrOfTestValues * 1), nrOfTestValues);

            GoThroughCandles(ref indicatorPairs, ref initialCandles);
            /*
             * * * FIND COMMON PAIR * * *
            topIndicatorPairs = GetTopIndicatorPairs(security, indicatorPairs, firstTest, firstTest.Count / 10);
            topIndicatorPairs = GetTopIndicatorPairs(security, indicatorPairs, secondTest, firstTest.Count / 10);
            topIndicatorPairs = GetTopIndicatorPairs(security, indicatorPairs, thirdTest, firstTest.Count / 10);
            */
            topIndicatorPairs = GetTopIndicatorPairs(security, indicatorPairs, firstTest, firstTest.Count / 10);
            topIndicatorPairs = GetTopIndicatorPairs(security, topIndicatorPairs, secondTest, firstTest.Count / 10);
            IndicatorPair topIndicatorPair = GetTopIndicatorPair(security, topIndicatorPairs, thirdTest);
            return topIndicatorPair;
        }

        public IndicatorPair GetTopIndicatorPair(string securityID, List<IndicatorPair> indicatorPairs, List<Candle> candles)
        {
            return GetTopIndicatorPairs(securityID, indicatorPairs, candles, 1)[0];
        }


        public List<IndicatorPair> GetTopIndicatorPairs(string securityID, List<IndicatorPair> indicatorPairs, List<Candle> candles, int top)
        {
            int initialMoney = 100000;
            int leverage = 1;
            int maxInvestedPct = 80;
            decimal marginSel = 2.0m;
            decimal marginBuy = 2.0m;

            Dictionary<StrategyGeneric, IndicatorPair> strategiesAndIndicators = new Dictionary<StrategyGeneric, IndicatorPair>();

            foreach(IndicatorPair indicatorPair in indicatorPairs)
            {
                EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, initialMoney, leverage, maxInvestedPct);
                StrategyGeneric strategyGeneric = new StrategyGeneric(emulationConnection, securityID, indicatorPair.LongIndicator, indicatorPair.ShortIndicator, marginSel, marginBuy);
                strategiesAndIndicators[strategyGeneric] = indicatorPair;

                strategyGeneric.Start();
                foreach(Candle candle in candles)
                {
                    strategyGeneric.ProcessCandle(candle);
                }
                strategyGeneric.Stop();
            }

            IList<StrategyGeneric> strategies = new List<StrategyGeneric>((strategiesAndIndicators.Keys).GetEnumerator().ToIEnumerable());
            strategies = SortingAlgorithm.MergeSort(strategies);


            List<IndicatorPair> topIndicatorPairs = new List<IndicatorPair>();
            for (int i = 0; i < top && i < strategies.Count; i++)
            {
                topIndicatorPairs.Add(strategiesAndIndicators[strategies[i]]);

            }

            return topIndicatorPairs;
        }

        public void GoThroughCandles(ref List<IndicatorPair> indicatorPairs, ref List<Candle> candles)
        {
            foreach (Candle candle in candles)
            {
                foreach (IndicatorPair indicatorPair in indicatorPairs)
                {
                    indicatorPair.ShortIndicator.Process(candle.ClosePrice);
                    indicatorPair.LongIndicator.Process(candle.ClosePrice);
                }
            }
        }


        /*
        public StrategyGeneric Simulate(List<Candle> candles, LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            StrategyGeneric strategy = new StrategyGeneric();

            return null;
        }
        */
    }
}
