using SandS.Algorithm.Library.SortNamespace;
using StockSharp.Algo.Indicators;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StockSolution.Services;
using TickEnum;
using Stocks.Service;
using CsvHelper;

namespace StockSolution.Services
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

        public decimal _LoseLimitConstant = -0.12m;
        public decimal LoseLimitConstant
        {
            get { return _LoseLimitConstant; }
            set { this._LoseLimitConstant = value; }
        }

        //Might Need Dual Run To Check Test Values - Ex. Every 30-45 or 90
        public void SimulationIndicatorPairsAndSave(int nrOfRuns, int nrOfCandles, int minIndicatorLength, int maxIndicatorLength, int indicatorInterval, bool isSellEnabled, bool isBuyEnabled)
        {
            string fullPath = ImportAndExport.GetFullPath(TickPeriod.DailySimulation);
            int minCandles = (maxIndicatorLength + nrOfCandles * nrOfRuns);
            Dictionary<SecurityInfo, List<IndicatorPair>> symbolsAndIndicatorPairs = new Dictionary<SecurityInfo, List<IndicatorPair>>();

            DateTimeOffset dateMayNotBeOlderThan = DateTimeOffset.UtcNow.AddDays(-(nrOfRuns * nrOfCandles * 1.7 + maxIndicatorLength));
            DateTime startTime = DateTime.Now;
            IList<SecurityInfo> securityInfos = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, dateMayNotBeOlderThan.DateTime, startTime);

            //Remove Candle Values If To Few
            for (int i = 0; i < securityInfos.Count; i++)
            {
                if (securityInfos[i].Candles.Count < minCandles)
                {
                    securityInfos.RemoveAt(i);
                    i -= 1;
                }
            }

            //Initiate Symbols
            foreach (SecurityInfo securityInfo in securityInfos)
            {
                List<Candle> initialCandles = securityInfo.Candles.GetRange(minCandles - 1, maxIndicatorLength);

                //Create Indicators 
                List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, minIndicatorLength, maxIndicatorLength, indicatorInterval);
                symbolsAndIndicatorPairs[securityInfo] = indicatorPairs;
            }

            #region Simulate
            //Run Simulation
            for (int i = 0; i > nrOfRuns; i++)
            {
                StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\StrategyResults\Strategies_"+ (nrOfRuns-i) + "_" + nrOfCandles + ".csv");
                CsvWriter csvWriter = new CsvWriter(streamWriter);

                csvWriter.WriteField("SecurityId");
                csvWriter.WriteField("ShortIndicator");
                csvWriter.WriteField("LongIndicator");
                csvWriter.WriteField("LoseLimitMin");
                csvWriter.WriteField("Orders");
                csvWriter.WriteField("PositiveOrderPct");
                csvWriter.WriteField("LastResult");
                csvWriter.NextRecord();

                foreach (SecurityInfo securityInfo in securityInfos)
                {
                    //Find Candles
                    if (securityInfo.Candles.Count < minCandles)
                    {
                        throw new InvalidDataException();
                    }

                    //Simulate Strategies/IndicatorPairs
                    for (int j = 0; j < symbolsAndIndicatorPairs[securityInfo].Count; j++)
                    {

                        List<IndicatorPair> indicatorPairs = symbolsAndIndicatorPairs[securityInfo];

                        int index = securityInfo.Candles.Count - (1 + i * nrOfCandles);
                        List<Candle> simulateCandles = securityInfo.Candles.GetRange(index, nrOfCandles);

                        //Simulation
                        SimulateIndicatorPairs(ref indicatorPairs, simulateCandles, isSellEnabled, isBuyEnabled);

                        //Update Indicator Pairs
                        symbolsAndIndicatorPairs[securityInfo] = indicatorPairs;
                    }

                    //Write To CSV File
                    foreach(IndicatorPair indicatorPair in symbolsAndIndicatorPairs[securityInfo])
                    {
                        csvWriter.WriteField(securityInfo.SecurityID);
                        csvWriter.WriteField(indicatorPair.ShortIndicator);
                        csvWriter.WriteField(indicatorPair.LongIndicator);
                        csvWriter.WriteField(indicatorPair.LoseLimitMin);
                        csvWriter.WriteField(indicatorPair.Orders);
                        csvWriter.WriteField(indicatorPair.PositiveOrderPct);
                        csvWriter.WriteField(indicatorPair.LastResult);
                        csvWriter.NextRecord();
                    }

                    csvWriter.Flush();
                    //SAVE CURRENT INDICATORPAIRS - VALUES
                }
                
            }
            #endregion
        }


        // Simulate Indicator Pairs No loseLimitConstant
        public void SimulateIndicatorPairs(ref List<IndicatorPair> indicatorPairs, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled)
        {
            //IGNORES loseLimitConstant
            SimulateIndicatorPairs(ref indicatorPairs, simulateCandles, isSellEnabled, isBuyEnabled, -10000m);
        }

        // Simulate IndicatorPairs
        private void SimulateIndicatorPairs(ref List<IndicatorPair> indicatorPairs, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant)
        {
            Parallel.ForEach(indicatorPairs, indicatorPair =>
            //foreach (IndicatorPair indicatorPair in indicatorPairs)
            {
                {
                    int initialMoney = 100000;
                    int orderLimit = initialMoney / 10;
                    int maxInvestedPct = 80;
                    SecurityInfo securityInfo = new SecurityInfo() { SecurityID = "TestID" };

                    EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, maxInvestedPct);
                    StrategyGeneric strategyGeneric = new StrategyGeneric(emulationConnection, securityInfo, indicatorPair.LongIndicator, indicatorPair.ShortIndicator, isSellEnabled, isBuyEnabled, loseLimitConstant);

                    strategyGeneric.Start();
                    //Process Candles
                    for (int i = 0; i < simulateCandles.Count; i++)
                    {
                        strategyGeneric.ProcessCandle(simulateCandles[i]);
                    }
                    strategyGeneric.Stop();

                    //Set LastResult
                    indicatorPair.LastResult = strategyGeneric.ConnectionSecurityIDProfit() / orderLimit * 100;
                    indicatorPair.LoseLimitMin = strategyGeneric.LoseLimitMin;
                    indicatorPair.Orders = strategyGeneric.OrderCount;
                    indicatorPair.PositiveOrderPct = (int)strategyGeneric.AllPositiveOrdersPct();
                }
            });
        }

        private List<IndicatorPair> CreateIndicatorPairs(List<Candle> initialCandles, int minIndicatorLength, int maxIndicatorLength, int interval)
        {
            int differentIndicators;
            List<LengthIndicator<decimal>> indicators = CreateIndicators(out differentIndicators, minIndicatorLength, maxIndicatorLength, interval);
            List<IndicatorPair> indicatorPairs = new List<IndicatorPair>();

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
                                LengthIndicator shortIndicatorAdapter = new LengthIndicator(shortIndicatorClone);
                                LengthIndicator longIndicatorAdapter = new LengthIndicator(longIndicatorClone);

                                indicatorPairs.Add(new IndicatorPair(shortIndicatorAdapter, longIndicatorAdapter));
                            }

                        }
                        longIndicator++;
                    }
                    shortIndicator++;
                }
            }
            return indicatorPairs;
        }

        private List<LengthIndicator<decimal>> CreateIndicators(out int differentIndicators, int minIndicatorLength, int maxIndicatorLength, int interval)
        {
            List<LengthIndicator<decimal>> indicators = new List<LengthIndicator<decimal>>();
            differentIndicators = 0;

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

            return indicators;
        }

        public OptimizerOptions FindBestOptions(OptimizerOptions optimizerOptions, List<Candle> candles, int nrOfTestValues, int leverage)
        {
            int minCandles = (optimizerOptions.IndicatorLength.Max + nrOfTestValues * optimizerOptions.RecursiveTests);
            if (candles.Count < minCandles)
            {
                throw new InvalidDataException();
            }
            int initIndex = candles.Count - (optimizerOptions.IndicatorLength.Max + nrOfTestValues * optimizerOptions.RecursiveTests);
            List<Candle> initialCandles = candles.GetRange(initIndex, optimizerOptions.IndicatorLength.Max);
            List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);

            //START SIMULATION - RUN X TIMES
            for (int recursiveTests = 0; recursiveTests < optimizerOptions.RecursiveTests; recursiveTests++)
            {
                List<Candle> currentCandles = candles.GetRange(candles.Count - (1 + nrOfTestValues * (optimizerOptions.RecursiveTests - recursiveTests)), nrOfTestValues);

                //Simulate IndicatorPairs With CurrentCandles
                SimulateIndicatorPairs(ref indicatorPairs, currentCandles, optimizerOptions.IsSellEnabled, optimizerOptions.IsBuyEnabled, optimizerOptions.LoseLimitConstant);

                //Missing Recursive AND FILTER USING CHOOSEN SETTINGS
                List<IndicatorPair> filteredIndicatorPairs = new List<IndicatorPair>();
                for (int i = 0; i < indicatorPairs.Count; i++)
                {
                    if (optimizerOptions.MinOrders <= indicatorPairs[i].Orders && optimizerOptions.MinProfitPct <= indicatorPairs[i].LastResult && optimizerOptions.PositiveOrderPct <= indicatorPairs[i].PositiveOrderPct)
                    {
                        filteredIndicatorPairs.Add(indicatorPairs[i]);
                    }
                }

                //Replace Found Pairs Whith Filtered Pairs - Reduces Test Time
                indicatorPairs = filteredIndicatorPairs;
            }

            //FIND BestIndicatorPair 
            optimizerOptions.BestIndicatorPair = indicatorPairs.Count > 0 ? SortingAlgorithm.MergeSort(indicatorPairs)[0] : null;
            return optimizerOptions;
        }

        private LengthIndicator<decimal> InitializeIndicator(ref List<Candle> initialCandles, LengthIndicator<decimal> lengthIndicator)
        {
            foreach (Candle candle in initialCandles)
            {
                lengthIndicator.Process(candle.ClosePrice);
            }

            return lengthIndicator;
        }

        public LengthIndicator FindIndicator(string indicator, int minIndicatorLength, int maxIndicatorLength, int interval)
        {
            LengthIndicator<decimal> lengthIndicator = null;
            int differentIndicators;
            List<LengthIndicator<decimal>> indicators = CreateIndicators(out differentIndicators, minIndicatorLength, maxIndicatorLength, interval);

            foreach (LengthIndicator<decimal> currentIndicator in indicators)
            {
                if (currentIndicator.ToString().Equals(indicator))
                {
                    lengthIndicator = currentIndicator;
                    break;
                }
            }

            return new LengthIndicator(lengthIndicator);
        }

        private bool IsIndicatorPairDisabled(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            StringReader strReader = new StringReader(this._DisabledPairs);
            string indicatorPair = shortIndicator.ToString() + " - " + longIndicator.ToString();

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

        private readonly string _DisabledPairs = @"
Momentum 28 - Momentum 36
SMA 8 - JMA 28
MeanDeviation 40 - Momentum 40
Lowest 4 - LinearReg 20
HMA 8 - LinearReg 8
Momentum 24 - Momentum 28
SMA 4 - HMA 12
JMA 16 - LinearReg 16
JMA 4 - LinearReg 12
LinearReg 12 - JMA 12
MeanDeviation 12 - Momentum 16
MeanDeviation 4 - Momentum 20
MeanDeviation 20 - Momentum 48
SMMA 4 - EMA 4
JMA 12 - HMA 16
SMA 12 - JMA 16
HMA 8 - JMA 20
SMA 4 - EMA 4
MeanDeviation 20 - Momentum 40
MeanDeviation 12 - Momentum 40
Highest 8 - LinearReg 8
JMA 12 - LinearReg 12
HMA 12 - LinearReg 16
MeanDeviation 16 - Momentum 32
KAMA 4 - JMA 16
SMMA 4 - LinearReg 8
Momentum 20 - Momentum 24
Momentum 4 - Momentum 16
SMA 4 - SMA 8
Momentum 36 - Momentum 40
Momentum 4 - MeanDeviation 20
SMMA 4 - JMA 12
JMA 12 - LinearReg 16
MeanDeviation 20 - Momentum 20
KAMA 4 - JMA 4
LinearReg 20 - JMA 20
Highest 8 - HMA 8
MeanDeviation 4 - Momentum 12
MeanDeviation 16 - Momentum 48
MeanDeviation 8 - Momentum 56
JMA 8 - SMA 8
LinearReg 4 - HMA 20
MeanDeviation 16 - Momentum 52
LinearReg 12 - HMA 12
MeanDeviation 8 - Momentum 16
JMA 8 - HMA 8
SMA 4 - HMA 16
LinearReg 8 - Lowest 8
Highest 4 - SMA 8
SMMA 4 - Lowest 4
JMA 4 - LinearReg 4
MeanDeviation 16 - Momentum 16
EMA 4 - JMA 20
MeanDeviation 4 - Momentum 36
Lowest 4 - KAMA 28
EMA 8 - LinearReg 8
Momentum 4 - Momentum 8
Lowest 4 - JMA 20
SMA 8 - LinearReg 8
KAMA 4 - JMA 8
Momentum 4 - MeanDeviation 8
HMA 12 - JMA 12
HMA 4 - HMA 8
KAMA 4 - HMA 4
HMA 16 - LinearReg 16
Momentum 4 - Momentum 24
MeanDeviation 24 - Momentum 32
SMA 12 - LinearReg 12
MeanDeviation 12 - Momentum 20
MeanDeviation 4 - Momentum 4
EMA 4 - JMA 4
SMA 4 - HMA 4
LinearReg 16 - HMA 16
JMA 8 - LinearReg 12
HMA 8 - Highest 16
Momentum 4 - Momentum 56
SMMA 4 - SMA 8
MeanDeviation 24 - Momentum 24
HMA 8 - JMA 8
KAMA 4 - EMA 4
KAMA 4 - LinearReg 12
SMA 12 - EMA 12
MeanDeviation 8 - Momentum 12
LinearReg 4 - HMA 12
SMA 4 - LinearReg 12
Highest 4 - LinearReg 16
Momentum 4 - Momentum 12
HMA 16 - JMA 16
MeanDeviation 48 - Momentum 60
Lowest 8 - JMA 16
HMA 4 - Lowest 4
HMA 4 - LinearReg 16
MeanDeviation 24 - Momentum 56
SMMA 4 - HMA 4
SMA 12 - HMA 16
KAMA 8 - EMA 8
Highest 4 - LinearReg 12
Highest 4 - HMA 4
KAMA 4 - Highest 4
MeanDeviation 28 - Momentum 52
SMA 12 - LinearReg 16
MeanDeviation 8 - MeanDeviation 12
SMMA 8 - HMA 8
KAMA 4 - LinearReg 4
MeanDeviation 36 - Momentum 36
Momentum 12 - Momentum 24
MeanDeviation 28 - Momentum 44
LinearReg 8 - HMA 8
Momentum 20 - Momentum 36
Highest 4 - LinearReg 8
MeanDeviation 56 - Momentum 56
LinearReg 12 - LinearReg 16
Highest 4 - LinearReg 4
MeanDeviation 4 - Momentum 24
Momentum 8 - Momentum 16
MeanDeviation 8 - Momentum 20
JMA 12 - HMA 12
SMA 8 - HMA 8
Highest 4 - HMA 16
MeanDeviation 36 - Momentum 44
EMA 4 - HMA 12
MeanDeviation 8 - Momentum 8
Momentum 8 - Momentum 32
SMA 4 - LinearReg 4
MeanDeviation 4 - Momentum 8
Momentum 48 - Momentum 56
EMA 8 - JMA 12
Momentum 8 - MeanDeviation 20
MeanDeviation 20 - Momentum 36
Lowest 4 - JMA 24
Momentum 28 - Momentum 32
MeanDeviation 8 - Momentum 32
JMA 4 - HMA 4
Lowest 4 - JMA 12
SMA 4 - HMA 8
MeanDeviation 16 - Momentum 28
MeanDeviation 28 - Momentum 60
EMA 4 - LinearReg 8
SMMA 4 - JMA 4
MeanDeviation 8 - Momentum 36
Lowest 4 - KAMA 20
Momentum 4 - Momentum 60
SMMA 4 - KAMA 4
MeanDeviation 44 - Momentum 60
SMA 4 - JMA 8
HMA 8 - LinearReg 16
MeanDeviation 4 - Momentum 56
MeanDeviation 32 - Momentum 36
Highest 4 - HMA 12
MeanDeviation 20 - Momentum 60
EMA 4 - HMA 4
LinearReg 4 - Lowest 4
MeanDeviation 48 - Momentum 56
KAMA 4 - SMMA 4
MeanDeviation 4 - Momentum 16
Momentum 4 - Momentum 28
MeanDeviation 32 - Momentum 60
HMA 4 - LinearReg 4
Momentum 44 - Momentum 48
LinearReg 8 - LinearReg 12
HMA 8 - LinearReg 12
MeanDeviation 36 - Momentum 52
SMA 4 - KAMA 4
MeanDeviation 24 - Momentum 40
MeanDeviation 4 - Momentum 32
JMA 8 - LinearReg 8
MeanDeviation 12 - Momentum 36
EMA 8 - SMA 8
MeanDeviation 4 - Momentum 40
MeanDeviation 4 - MeanDeviation 12
SMA 8 - JMA 20
MeanDeviation 8 - Momentum 52
Momentum 4 - Momentum 32
Lowest 4 - HMA 40
SMMA 8 - JMA 8
Momentum 32 - Momentum 36
Lowest 4 - LinearReg 4
MeanDeviation 24 - Momentum 52
MeanDeviation 32 - Momentum 48
Momentum 32 - Momentum 40
MeanDeviation 4 - Momentum 60
SMA 4 - JMA 16
MeanDeviation 12 - Momentum 32
MeanDeviation 16 - MeanDeviation 20
Momentum 16 - Momentum 32
MeanDeviation 4 - MeanDeviation 8
MeanDeviation 12 - Momentum 44
Momentum 56 - Momentum 60
Momentum 40 - Momentum 44
MeanDeviation 12 - MeanDeviation 16
MeanDeviation 40 - Momentum 60
SMMA 4 - Highest 4
MeanDeviation 8 - Momentum 48
KAMA 4 - JMA 32
SMA 4 - LinearReg 8
MeanDeviation 16 - Momentum 60
SMA 8 - EMA 8
MeanDeviation 28 - Momentum 32
LinearReg 20 - HMA 24
JMA 24 - LinearReg 24
MeanDeviation 12 - Momentum 12
Momentum 20 - Momentum 28
SMMA 4 - HMA 12
MeanDeviation 12 - Momentum 28
JMA 16 - LinearReg 20
MeanDeviation 12 - Momentum 52
LinearReg 16 - HMA 20
EMA 4 - JMA 12
SMMA 4 - JMA 16
HMA 24 - LinearReg 24
Lowest 8 - JMA 20
MeanDeviation 44 - Momentum 52
Momentum 24 - Momentum 36
Momentum 8 - MeanDeviation 8
EMA 8 - LinearReg 12
LinearReg 20 - JMA 24
JMA 4 - HMA 16
SMMA 4 - JMA 8
MeanDeviation 52 - Momentum 56
Lowest 4 - JMA 16
KAMA 4 - SMA 4
Momentum 8 - Momentum 56
EMA 8 - HMA 8
Lowest 4 - HMA 8
Momentum 24 - Momentum 32
MeanDeviation 12 - Momentum 56
Lowest 4 - JMA 28
Momentum 8 - Momentum 36
EMA 8 - JMA 8
JMA 8 - HMA 16
Momentum 16 - Momentum 20
Momentum 48 - Momentum 52
MeanDeviation 8 - Momentum 44
SMA 16 - EMA 16
Lowest 8 - JMA 36
HMA 8 - JMA 36
Momentum 44 - Momentum 60
Momentum 16 - Momentum 44
Lowest 4 - JMA 32
Momentum 40 - Momentum 48
Momentum 16 - Momentum 52
HMA 8 - HMA 16
Momentum 48 - Momentum 60
Momentum 32 - Momentum 52
Momentum 8 - Momentum 12
EMA 4 - HMA 16
Lowest 4 - JMA 40
LinearReg 12 - JMA 24
LinearReg 12 - JMA 20
MeanDeviation 40 - Momentum 52
MeanDeviation 4 - Momentum 52
Momentum 36 - Momentum 48
MeanDeviation 24 - Momentum 60
Momentum 36 - Momentum 44
KAMA 8 - SMA 12
Momentum 16 - Momentum 24
Lowest 12 - JMA 56
Momentum 32 - Momentum 44
JMA 12 - LinearReg 20
HMA 8 - JMA 16
Lowest 8 - JMA 48
KAMA 8 - LinearReg 8
EMA 8 - JMA 20
MeanDeviation 8 - Momentum 60
Highest 4 - JMA 12
HMA 4 - HMA 12
Lowest 8 - JMA 32
HMA 8 - HMA 12
MeanDeviation 28 - Momentum 40
Lowest 4 - HMA 20
EMA 4 - SMA 4
HMA 4 - JMA 12
Momentum 12 - Momentum 20
Lowest 4 - EMA 8
Momentum 52 - Momentum 56
SMA 8 - HMA 12
SMMA 4 - SMA 4
MeanDeviation 48 - Momentum 52
KAMA 8 - LinearReg 12
LinearReg 4 - Highest 4
LinearReg 16 - JMA 16
Momentum 24 - Momentum 44
LinearReg 4 - LinearReg 8
MeanDeviation 56 - Momentum 60
MeanDeviation 36 - Momentum 56
Momentum 8 - Momentum 28
KAMA 8 - HMA 8
KAMA 4 - LinearReg 16
Momentum 8 - Momentum 24
Momentum 24 - Momentum 40
Lowest 4 - JMA 52
Momentum 8 - Momentum 52
SMA 8 - JMA 36
Highest 8 - Highest 12
Lowest 4 - KAMA 8
KAMA 8 - JMA 20
SMA 4 - EMA 8
MeanDeviation 16 - Momentum 44
MeanDeviation 28 - Momentum 36
Lowest 4 - SMA 12
KAMA 8 - JMA 8
SMMA 8 - LinearReg 8
MeanDeviation 32 - Momentum 52
LinearReg 4 - HMA 16
SMA 4 - SMMA 4
Momentum 12 - Momentum 36
MeanDeviation 28 - Momentum 48
Momentum 4 - MeanDeviation 4
SMMA 4 - HMA 8
HMA 8 - SMA 8
EMA 8 - HMA 12
Lowest 4 - LinearReg 8
Lowest 4 - LinearReg 24
MeanDeviation 8 - MeanDeviation 16
HMA 16 - JMA 20
Momentum 52 - Momentum 60
HMA 12 - LinearReg 20
Lowest 8 - JMA 40
MeanDeviation 48 - Momentum 48
MeanDeviation 24 - Momentum 28
Momentum 4 - Momentum 20
HMA 4 - Lowest 8
LinearReg 20 - JMA 28
HMA 4 - LinearReg 8
HMA 4 - JMA 20
HMA 20 - JMA 20
JMA 8 - EMA 8
HMA 4 - EMA 4
Lowest 4 - LinearReg 32
Momentum 8 - Momentum 20
Highest 4 - LinearReg 24
Lowest 8 - HMA 8
Highest 4 - SMA 12
KAMA 4 - HMA 12
HMA 20 - LinearReg 24
Highest 4 - Highest 8
LinearReg 16 - JMA 20
Lowest 8 - LinearReg 12
Lowest 4 - SMA 8
KAMA 8 - EMA 12
MeanDeviation 20 - MeanDeviation 24
MeanDeviation 24 - Momentum 48
KAMA 4 - EMA 8
Lowest 4 - JMA 56
Momentum 16 - Momentum 36
MeanDeviation 8 - Momentum 40
Momentum 4 - Momentum 36
SMMA 4 - JMA 20
MeanDeviation 20 - Momentum 24
SMA 8 - LinearReg 20
LinearReg 24 - JMA 24
JMA 4 - LinearReg 16
MeanDeviation 40 - Momentum 48
SMMA 4 - HMA 16
KAMA 4 - JMA 12
MeanDeviation 32 - Momentum 40
Momentum 8 - MeanDeviation 16
Momentum 8 - Momentum 48
KAMA 4 - HMA 8
Highest 4 - EMA 12
Momentum 40 - Momentum 52
EMA 4 - JMA 16
LinearReg 12 - JMA 16
Lowest 4 - KAMA 4
MeanDeviation 40 - Momentum 56
Lowest 8 - LinearReg 8
MeanDeviation 12 - Momentum 48
Momentum 40 - Momentum 60
";

    }
}
