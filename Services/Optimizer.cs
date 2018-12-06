using CsvHelper;
using SandS.Algorithm.Library.SortNamespace;
using Stocks.Service;
using StockSharp.Algo.Indicators;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TickEnum;

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

        /* Simulate IndicatorPairs And Save
         * Might Need Dual Run To Check Test Values - Ex. Every 30-45 or 90
        public void SimulationIndicatorPairsAndSave(int nrOfRuns, int nrOfCandles, int minIndicatorLength, int maxIndicatorLength, int indicatorInterval, bool isSellEnabled, bool isBuyEnabled)
        {
            string fullPath = ImportAndExport.GetFullPath(TickPeriod.DailySimulation);
            int minCandles = (maxIndicatorLength + nrOfCandles * nrOfRuns);
            DateTimeOffset dateMayNotBeOlderThan = DateTimeOffset.UtcNow.AddDays(-(nrOfRuns * nrOfCandles * 1.7 + maxIndicatorLength));
            DateTime startTime = DateTime.Now;
            //IList<SecurityInfo> securityInfos = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, dateMayNotBeOlderThan.DateTime, startTime);

            //Run Simulation
            for (int i = 0; i < nrOfRuns; i++)
            {
                string path = @"C:\StockHistory\StrategyResults\Strategies_" + (nrOfRuns - i) + "_" + nrOfCandles + ".csv";
                StreamWriter streamWriter = new StreamWriter(path, false);
                CsvWriter csvWriter = new CsvWriter(streamWriter);

                csvWriter.WriteField("SecurityId");
                csvWriter.WriteField("ShortIndicator");
                csvWriter.WriteField("LongIndicator");
                csvWriter.WriteField("LoseLimit");
                csvWriter.WriteField("LoseLimitMin");
                csvWriter.WriteField("Orders");
                csvWriter.WriteField("PositiveOrderPct");
                csvWriter.WriteField("LastResult");
                csvWriter.WriteField("ClosePrice");
                csvWriter.WriteField("CloseTime");
                csvWriter.NextRecord();
                csvWriter.Flush();
                streamWriter.Flush();
                streamWriter.Close();
            }

            foreach (string securityID in LoaderService.GetSecurityIDs(fullPath))
            {
                SecurityInfo securityInfo = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, securityID, startTime, dateMayNotBeOlderThan.DateTime);

                if (securityInfo.Candles != null)
                {
                    if (securityInfo.Candles.Count >= minCandles)
                    {
                        for (int i = 0; i < nrOfRuns; i++)
                        {
                            string path = @"C:\StockHistory\StrategyResults\Strategies_" + (nrOfRuns - i) + "_" + nrOfCandles + ".csv";
                            StreamWriter streamWriter = new StreamWriter(path, true);
                            CsvWriter csvWriter = new CsvWriter(streamWriter);

                            List<Candle> initialCandles = securityInfo.Candles.GetRange(i * nrOfCandles, maxIndicatorLength);
                            //List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, minIndicatorLength, maxIndicatorLength, indicatorInterval);
                            List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, minIndicatorLength);
                            //Create Indicators 
                            int index = i * nrOfCandles + maxIndicatorLength;
                            List<Candle> simulateCandles = securityInfo.Candles.GetRange(index, nrOfCandles);

                            //Simulation
                            SimulateIndicatorPairs(ref indicatorPairs, simulateCandles, isSellEnabled, isBuyEnabled);

                            //Parallel.ForEach(indicatorPairs, indicatorPair =>
                            foreach (IndicatorPair indicatorPair in indicatorPairs)
                            {
                                //Write TO CSV File
                                csvWriter.WriteField(securityInfo.SecurityID);
                                csvWriter.WriteField(indicatorPair.ShortIndicator);
                                csvWriter.WriteField(indicatorPair.LongIndicator);
                                csvWriter.WriteField(indicatorPair.LoseLimit);
                                csvWriter.WriteField(indicatorPair.LoseLimitMin);
                                csvWriter.WriteField(indicatorPair.Orders);
                                csvWriter.WriteField(indicatorPair.PositiveOrderPct);
                                csvWriter.WriteField(indicatorPair.LastResult);
                                csvWriter.WriteField(simulateCandles[0].ClosePrice);
                                csvWriter.WriteField(simulateCandles[0].CloseTime);

                                csvWriter.NextRecord();
                            }

                            csvWriter.Flush();
                            streamWriter.Flush();
                            streamWriter.Close();
                            //);
                        }
                    }
                }
            }
        }
        */

        /* Simulate Indicator Pairs No loseLimitConstant
        public void SimulateIndicatorPairs(ref List<IndicatorPair> indicatorPairs, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled)
        {
            //IGNORES loseLimitConstant
            SimulateIndicatorPairs(ref indicatorPairs, simulateCandles, isSellEnabled, isBuyEnabled, -10000m);
        }
        */ 

        // Simulate IndicatorPairs
        /* TODO 06/12/2018 */
        private void SimulateIndicatorPairs(ref List<IndicatorPair> indicatorPairs, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled, bool isRealTime)
        {
            Parallel.ForEach(indicatorPairs, indicatorPair =>
            //foreach (IndicatorPair indicatorPair in indicatorPairs)
            //for(int i = 0; i < indicatorPairs.Count; i++)
            {
                int initialMoney = 100000;
                int orderLimit = initialMoney / 10;
                int maxInvestedPct = 80;
                SecurityInfo securityInfo = new SecurityInfo() { SecurityID = "TestID" };

                EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, maxInvestedPct);
                StrategyGeneric strategyGeneric = null;

                if (isRealTime)
                {
                    strategyGeneric = new StrategyGeneric(emulationConnection, securityInfo, indicatorPair.LongIndicator, indicatorPair.ShortIndicator, isSellEnabled, isBuyEnabled, indicatorPair.LoseLimit);
                }
                else
                {
                    strategyGeneric = new StrategyGeneric(emulationConnection, securityInfo, indicatorPair.LongIndicator, indicatorPair.ShortIndicator, isSellEnabled, isBuyEnabled, indicatorPair.LoseLimitMin);
                }

                strategyGeneric.Start();
                //Process Candles
                foreach (Candle candle in simulateCandles)
                {
                    strategyGeneric.ProcessCandle(candle);
                }
                strategyGeneric.Stop();

                //Set LastResult
                indicatorPair.LastResult = strategyGeneric.ConnectionSecurityIDProfit() / orderLimit * 100;
                indicatorPair.LoseLimitMin = strategyGeneric.LoseLimitMin;
                indicatorPair.Orders = strategyGeneric.OrderCount;
                indicatorPair.PositiveOrderPct = (int)strategyGeneric.AllPositiveOrdersPct();
            }
            );
        }

        /* CREATEINDICATORSOLD
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
                            if (IsIndicatorPairEnabled(indicators[shortIndicator], indicators[longIndicator]))
                            {
                                LengthIndicator<decimal> shortIndicatorClone = indicators[shortIndicator].Clone() as LengthIndicator<decimal>;
                                LengthIndicator<decimal> longIndicatorClone = indicators[longIndicator].Clone() as LengthIndicator<decimal>;
                                Entity.Models.LengthIndicator shortIndicatorAdapter = new LengthIndicator(shortIndicatorClone);
                                Entity.Models.LengthIndicator longIndicatorAdapter = new LengthIndicator(longIndicatorClone);

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
        */

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

        /* TODO 06/12/2018 */
        public OptimizerOptions FindBestOptions(OptimizerOptions optimizerOptions, List<Candle> candles, int leverage)
        {
            int minCandles = (this.GetMaxIndicatorLength() + optimizerOptions.NrOfTestValues * optimizerOptions.RecursiveTests);
            if (candles.Count < minCandles)
            {
                throw new InvalidDataException();
            }
            int initIndex = candles.Count - (this.GetMaxIndicatorLength() + optimizerOptions.NrOfTestValues * optimizerOptions.RecursiveTests);
            List<Candle> initialCandles = candles.GetRange(initIndex, this.GetMaxIndicatorLength());
            //List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);
            List<IndicatorPair> indicatorPairs = this.CreateIndicatorPairs(initialCandles, optimizerOptions.LoseLimitMin);

            //START SIMULATION - RUN X TIMES
            for (int recursiveTests = 0; recursiveTests < optimizerOptions.RecursiveTests; recursiveTests++)
            {
                List<Candle> currentCandles = candles.GetRange(candles.Count - (1 + optimizerOptions.NrOfTestValues * (optimizerOptions.RecursiveTests - recursiveTests)), optimizerOptions.NrOfTestValues);
                bool isReal = false;

                /* TODO 06/12/2018 */
                //Simulate IndicatorPairs With CurrentCandles
                SimulateIndicatorPairs(ref indicatorPairs, currentCandles, optimizerOptions.IsSellEnabled, optimizerOptions.IsBuyEnabled, isReal);

                //Missing Recursive AND FILTER USING CHOOSEN SETTINGS
                List<IndicatorPair> filteredIndicatorPairs = new List<IndicatorPair>();
                for (int i = 0; i < indicatorPairs.Count; i++)
                {
                    if (optimizerOptions.MinOrders <= indicatorPairs[i].Orders &&
                        optimizerOptions.MaxOrders >= indicatorPairs[i].Orders &&
                        optimizerOptions.MinProfitPct <= indicatorPairs[i].LastResult &&
                        optimizerOptions.LoseLimitMin <= indicatorPairs[i].LoseLimit)
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

        private bool IsIndicatorPairEnabled(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            StringReader strReader = new StringReader(this._EnabledPairs);
            string indicatorPair = shortIndicator.ToString() + " - " + longIndicator.ToString();

            bool isEnabled = false;
            while (strReader.Peek() >= 0)
            {
                string enabledIndicatorPair = strReader.ReadLine();
                if (indicatorPair.Equals(enabledIndicatorPair))
                {
                    isEnabled = true;
                    break;
                }
            }

            return isEnabled;
        }


        private int _MaxIndicatorLength = -1;

        public int GetMaxIndicatorLength()
        {
            if (_MaxIndicatorLength <= 0)
            {
                StringReader strReader = new StringReader(this._EnabledPairs);
                while (strReader.Peek() >= 0)
                {
                    string enabledIndicatorPair = strReader.ReadLine();
                    string[] numbers = Regex.Split(enabledIndicatorPair, @"\D+");

                    foreach (string number in numbers)
                    {
                        int length = int.Parse(number);
                        if (length > this._MaxIndicatorLength)
                        {
                            this._MaxIndicatorLength = length;
                        }
                    }
                }
            }

            return this._MaxIndicatorLength;
        }


        private List<LengthIndicator<decimal>> _ShortIndicators = new List<LengthIndicator<decimal>>();
        private List<LengthIndicator<decimal>> _LongIndicators = new List<LengthIndicator<decimal>>();
        private List<decimal> _LoseLimits = new List<decimal>();

        public List<IndicatorPair> CreateIndicatorPairs(List<Candle> initialCandles, decimal loseLimitMin)
        {
            List<IndicatorPair> indicatorPairs = new List<IndicatorPair>();

            //Singleton
            if (_ShortIndicators.Count <= 0)
            {
                StringReader strReader = new StringReader(this._EnabledPairs);

                while (strReader.Peek() >= 0)
                {
                    string enabledIndicatorPair = strReader.ReadLine();

                    if (enabledIndicatorPair.Length > 0)
                    {
                        string[] indicators = enabledIndicatorPair.Split('-');

                        indicators[0] = indicators[0].TrimEnd();
                        indicators[1] = indicators[1].TrimStart();

                        LengthIndicator<decimal> shortIndicator = null;
                        LengthIndicator<decimal> longIndicator = null;
                        decimal loseLimit = 0;

                        string[] indicator = indicators[0].Split(' ');

                        switch (indicator[0])
                        {
                            case "SMA":
                                shortIndicator = new SimpleMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "SMMA":
                                shortIndicator = new SmoothedMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "EMA":
                                shortIndicator = new ExponentialMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Highest":
                                shortIndicator = new Highest() { Length = int.Parse(indicator[1]) };
                                break;
                            case "HMA":
                                shortIndicator = new HullMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "JMA":
                                shortIndicator = new JurikMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "KAMA":
                                shortIndicator = new KaufmannAdaptiveMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "LinearReg":
                                shortIndicator = new LinearReg() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Lowest":
                                shortIndicator = new Lowest() { Length = int.Parse(indicator[1]) };
                                break;
                            case "MeanDeviation":
                                shortIndicator = new MeanDeviation() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Momentum":
                                shortIndicator = new Momentum() { Length = int.Parse(indicator[1]) };
                                break;
                        }

                        indicator = indicators[1].Split(' ');
                        switch (indicator[0])
                        {
                            case "SMA":
                                longIndicator = new SimpleMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "SMMA":
                                longIndicator = new SmoothedMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "EMA":
                                longIndicator = new ExponentialMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Highest":
                                longIndicator = new Highest() { Length = int.Parse(indicator[1]) };
                                break;
                            case "HMA":
                                longIndicator = new HullMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "JMA":
                                longIndicator = new JurikMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "KAMA":
                                longIndicator = new KaufmannAdaptiveMovingAverage() { Length = int.Parse(indicator[1]) };
                                break;
                            case "LinearReg":
                                longIndicator = new LinearReg() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Lowest":
                                longIndicator = new Lowest() { Length = int.Parse(indicator[1]) };
                                break;
                            case "MeanDeviation":
                                longIndicator = new MeanDeviation() { Length = int.Parse(indicator[1]) };
                                break;
                            case "Momentum":
                                longIndicator = new Momentum() { Length = int.Parse(indicator[1]) };
                                break;
                        }

                        loseLimit = -(decimal.Parse(indicators[2]));

                        if (!(shortIndicator != null))
                        {
                            throw new ArgumentNullException();
                        }

                        if (!(longIndicator != null))
                        {
                            throw new ArgumentNullException();
                        }

                        if (loseLimit == 0)
                        {
                            throw new ArgumentNullException();
                        }

                        _LoseLimits.Add(loseLimit);
                        _ShortIndicators.Add(shortIndicator);
                        _LongIndicators.Add(longIndicator);
                    }
                }
            }

            // Clone Empty Indicators And Create Pairs
            for (int i = 0; i < _ShortIndicators.Count; i++)
            {
                LengthIndicator<decimal> shortIndicatorClone = _ShortIndicators[i].Clone() as LengthIndicator<decimal>;
                LengthIndicator<decimal> longIndicatorClone = _LongIndicators[i].Clone() as LengthIndicator<decimal>;
                Entity.Models.LengthIndicator shortIndicatorAdapter = new LengthIndicator(shortIndicatorClone);
                Entity.Models.LengthIndicator longIndicatorAdapter = new LengthIndicator(longIndicatorClone);
                // Init candles
                for (i = initialCandles.Count - shortIndicatorAdapter.Indicator.Length; i < initialCandles.Count; i++)
                {
                    shortIndicatorAdapter.Indicator.Process(initialCandles[i].ClosePrice);
                }
                // Init candles
                for (i = initialCandles.Count - longIndicatorAdapter.Indicator.Length; i < initialCandles.Count; i++)
                {
                    longIndicatorAdapter.Indicator.Process(initialCandles[i].ClosePrice);
                }

                indicatorPairs.Add(new IndicatorPair(shortIndicatorAdapter, longIndicatorAdapter, _LoseLimits[i], loseLimitMin));
            }

            return indicatorPairs;
        }



        /*
         * From SQL
         * ShortIndicator - LongIndicator 
         */
        private readonly string _EnabledPairs = @"
SMMA 12 - JMA 20 - 0.16
KAMA 12 - JMA 12 - 0.13
SMA 8 - LinearReg 10 - 0.13
KAMA 6 - LinearReg 10 - 0.13
MeanDeviation 14 - Momentum 24 - 0.19
LinearReg 2 - JMA 6 - 0.11
MeanDeviation 4 - Momentum 42 - 0.24
SMMA 4 - HMA 6 - 0.1
MeanDeviation 4 - Momentum 44 - 0.24
LinearReg 32 - JMA 46 - 0.11
LinearReg 28 - JMA 46 - 0.11
Highest 2 - LinearReg 6 - 0.17
SMA 6 - JMA 12 - 0.1
EMA 6 - HMA 6 - 0.12
Highest 16 - LinearReg 18 - 0.16
Highest 14 - LinearReg 18 - 0.16
LinearReg 2 - JMA 4 - 0.12
SMA 10 - LinearReg 14 - 0.14
Highest 18 - LinearReg 18 - 0.16
MeanDeviation 8 - Momentum 30 - 0.22
SMA 10 - LinearReg 16 - 0.15
LinearReg 26 - JMA 38 - 0.11
KAMA 2 - JMA 8 - 0.13
LinearReg 20 - JMA 22 - 0.11
MeanDeviation 14 - Momentum 22 - 0.16
SMMA 8 - JMA 22 - 0.11
MeanDeviation 10 - Momentum 28 - 0.22
LinearReg 28 - JMA 44 - 0.11
MeanDeviation 4 - Momentum 46 - 0.24
LinearReg 24 - JMA 34 - 0.11
MeanDeviation 18 - Momentum 18 - 0.15
SMMA 10 - EMA 12 - 0.2
SMMA 12 - Lowest 14 - 0.2
SMMA 10 - JMA 20 - 0.15
LinearReg 30 - JMA 46 - 0.11
MeanDeviation 6 - Momentum 34 - 0.24
SMA 8 - LinearReg 16 - 0.15
KAMA 4 - JMA 10 - 0.1
SMMA 14 - JMA 18 - 0.15
SMA 10 - JMA 14 - 0.11
SMA 16 - JMA 16 - 0.14
LinearReg 28 - JMA 42 - 0.11
SMA 6 - LinearReg 10 - 0.13
MeanDeviation 16 - Momentum 18 - 0.12
EMA 6 - LinearReg 8 - 0.11
EMA 4 - JMA 6 - 0.12
MeanDeviation 12 - Momentum 24 - 0.19
Highest 12 - LinearReg 18 - 0.16
LinearReg 32 - JMA 44 - 0.11
SMMA 8 - EMA 8 - 0.16
MeanDeviation 10 - Momentum 26 - 0.22
SMA 2 - JMA 4 - 0.16
Highest 8 - LinearReg 20 - 0.2
LinearReg 22 - JMA 28 - 0.11
SMA 12 - JMA 14 - 0.11
SMMA 8 - LinearReg 10 - 0.16
EMA 6 - JMA 8 - 0.14
MeanDeviation 10 - Momentum 20 - 0.22
MeanDeviation 4 - Momentum 38 - 0.23
LinearReg 30 - JMA 44 - 0.11
MeanDeviation 8 - Momentum 28 - 0.22
LinearReg 26 - JMA 36 - 0.11
MeanDeviation 12 - Momentum 22 - 0.16
SMMA 12 - JMA 18 - 0.16
SMMA 6 - JMA 14 - 0.16
SMA 14 - JMA 16 - 0.12
SMA 8 - JMA 14 - 0.09
KAMA 10 - JMA 12 - 0.14
Highest 10 - LinearReg 18 - 0.19
LinearReg 24 - JMA 32 - 0.12
SMMA 10 - LinearReg 10 - 0.16
KAMA 8 - JMA 10 - 0.16
SMMA 14 - JMA 16 - 0.15
SMA 6 - HMA 6 - 0.09
LinearReg 34 - JMA 46 - 0.11
EMA 4 - LinearReg 6 - 0.13
SMMA 6 - HMA 6 - 0.12
LinearReg 28 - JMA 40 - 0.11
MeanDeviation 6 - Momentum 30 - 0.25
MeanDeviation 14 - Momentum 14 - 0.12
EMA 10 - JMA 10 - 0.14
MeanDeviation 10 - Momentum 22 - 0.23
MeanDeviation 10 - Momentum 24 - 0.22
LinearReg 30 - JMA 42 - 0.11
SMMA 8 - JMA 20 - 0.11
LinearReg 32 - JMA 42 - 0.11
Highest 6 - LinearReg 18 - 0.19
KAMA 6 - JMA 10 - 0.2
MeanDeviation 14 - Momentum 18 - 0.15
KAMA 8 - LinearReg 10 - 0.14
SMA 14 - JMA 14 - 0.13
MeanDeviation 8 - Momentum 26 - 0.22
MeanDeviation 10 - Momentum 12 - 0.22
LinearReg 22 - JMA 26 - 0.11
SMMA 12 - JMA 16 - 0.16
MeanDeviation 6 - Momentum 32 - 0.24
SMA 12 - JMA 12 - 0.12
MeanDeviation 2 - Momentum 42 - 0.2
LinearReg 26 - JMA 34 - 0.11
SMA 2 - HMA 4 - 0.14
SMMA 10 - JMA 18 - 0.16
MeanDeviation 2 - Momentum 44 - 0.2
SMA 6 - JMA 10 - 0.1
LinearReg 34 - JMA 44 - 0.12
SMMA 2 - LinearReg 4 - 0.11
EMA 8 - JMA 10 - 0.17
SMMA 8 - EMA 10 - 0.21
KAMA 10 - JMA 10 - 0.16
Highest 2 - HMA 6 - 0.12
MeanDeviation 12 - Momentum 14 - 0.12
LinearReg 24 - JMA 30 - 0.12
LinearReg 22 - Highest 24 - 0.11
SMMA 14 - JMA 14 - 0.14
LinearReg 22 - Highest 28 - 0.11
LinearReg 22 - Highest 30 - 0.11
MeanDeviation 8 - Momentum 22 - 0.23
LinearReg 22 - Highest 32 - 0.11
LinearReg 32 - JMA 40 - 0.11
LinearReg 22 - Highest 26 - 0.11
LinearReg 22 - Highest 22 - 0.11
LinearReg 22 - Highest 34 - 0.11
LinearReg 22 - Highest 36 - 0.11
SMA 10 - JMA 12 - 0.11
LinearReg 22 - Highest 38 - 0.11
LinearReg 22 - Highest 46 - 0.11
LinearReg 22 - Highest 40 - 0.11
LinearReg 22 - Highest 44 - 0.11
LinearReg 22 - Highest 42 - 0.11
LinearReg 28 - JMA 38 - 0.11
MeanDeviation 8 - Momentum 24 - 0.22
Highest 8 - LinearReg 18 - 0.19
KAMA 2 - JMA 6 - 0.15
MeanDeviation 2 - Momentum 46 - 0.19
MeanDeviation 12 - Momentum 18 - 0.15
MeanDeviation 14 - Momentum 16 - 0.15
MeanDeviation 6 - Momentum 28 - 0.24
SMMA 10 - Lowest 12 - 0.23
LinearReg 34 - JMA 42 - 0.12
MeanDeviation 2 - Momentum 40 - 0.18
MeanDeviation 10 - Momentum 18 - 0.22
SMMA 6 - EMA 6 - 0.14
LinearReg 30 - JMA 40 - 0.11
MeanDeviation 8 - Momentum 18 - 0.22
MeanDeviation 6 - Momentum 20 - 0.22
SMMA 12 - JMA 14 - 0.16
LinearReg 32 - Highest 46 - 0.11
LinearReg 32 - Highest 44 - 0.11
LinearReg 32 - Highest 42 - 0.11
LinearReg 32 - Highest 40 - 0.11
LinearReg 32 - Highest 38 - 0.11
LinearReg 32 - Highest 32 - 0.11
LinearReg 32 - Highest 34 - 0.11
LinearReg 32 - Highest 36 - 0.11
KAMA 4 - HMA 6 - 0.13
KAMA 4 - LinearReg 8 - 0.13
SMA 10 - JMA 10 - 0.11
LinearReg 22 - JMA 24 - 0.11
SMA 8 - JMA 12 - 0.09
MeanDeviation 6 - Momentum 12 - 0.13
LinearReg 26 - JMA 32 - 0.11
KAMA 6 - JMA 8 - 0.2
Highest 6 - HMA 8 - 0.12
LinearReg 32 - JMA 38 - 0.12
MeanDeviation 6 - Momentum 22 - 0.22
SMMA 10 - JMA 16 - 0.21
MeanDeviation 6 - Momentum 26 - 0.23
MeanDeviation 4 - Momentum 34 - 0.24
MeanDeviation 10 - Momentum 14 - 0.23
LinearReg 36 - JMA 46 - 0.13
SMMA 4 - JMA 10 - 0.15
KAMA 2 - LinearReg 6 - 0.13
MeanDeviation 10 - Momentum 10 - 0.15
SMMA 8 - JMA 18 - 0.18
KAMA 6 - HMA 6 - 0.11
LinearReg 28 - JMA 36 - 0.11
LinearReg 24 - JMA 28 - 0.11
LinearReg 34 - JMA 40 - 0.11
SMMA 2 - JMA 4 - 0.11
Highest 4 - LinearReg 14 - 0.15
MeanDeviation 2 - Momentum 38 - 0.19
SMMA 6 - JMA 12 - 0.14
SMMA 6 - LinearReg 8 - 0.14
LinearReg 26 - Highest 32 - 0.11
LinearReg 26 - Highest 30 - 0.11
LinearReg 26 - Highest 34 - 0.11
LinearReg 30 - JMA 38 - 0.11
LinearReg 26 - Highest 36 - 0.11
LinearReg 26 - Highest 40 - 0.11
MeanDeviation 8 - Momentum 10 - 0.17
LinearReg 26 - Highest 28 - 0.11
LinearReg 26 - Highest 42 - 0.11
LinearReg 26 - Highest 26 - 0.11
LinearReg 26 - Highest 38 - 0.11
LinearReg 26 - Highest 46 - 0.11
LinearReg 26 - Highest 44 - 0.11
KAMA 6 - LinearReg 8 - 0.13
Highest 4 - HMA 8 - 0.12
LinearReg 34 - Highest 46 - 0.11
LinearReg 34 - Highest 44 - 0.11
LinearReg 34 - Highest 40 - 0.11
LinearReg 34 - Highest 42 - 0.11
LinearReg 34 - Highest 38 - 0.11
LinearReg 34 - Highest 34 - 0.11
LinearReg 34 - Highest 36 - 0.11
LinearReg 22 - JMA 22 - 0.12
SMMA 12 - JMA 12 - 0.16
LinearReg 32 - JMA 36 - 0.12
EMA 6 - LinearReg 6 - 0.13
KAMA 6 - JMA 6 - 0.11
LinearReg 36 - JMA 44 - 0.13
LinearReg 28 - Highest 32 - 0.11
LinearReg 28 - Highest 40 - 0.11
LinearReg 28 - Highest 30 - 0.11
MeanDeviation 6 - Momentum 24 - 0.24
LinearReg 28 - Highest 34 - 0.11
LinearReg 28 - Highest 38 - 0.11
LinearReg 28 - Highest 36 - 0.11
LinearReg 28 - Highest 42 - 0.11
SMA 4 - JMA 6 - 0.1
LinearReg 28 - Highest 46 - 0.11
LinearReg 26 - JMA 30 - 0.12
LinearReg 28 - Highest 28 - 0.11
LinearReg 28 - Highest 44 - 0.11
MeanDeviation 8 - Momentum 14 - 0.23
KAMA 6 - LinearReg 6 - 0.11
EMA 6 - JMA 6 - 0.12
LinearReg 34 - JMA 38 - 0.12
MeanDeviation 6 - Momentum 18 - 0.22
SMMA 6 - EMA 8 - 0.16
SMMA 10 - JMA 14 - 0.21
KAMA 2 - HMA 4 - 0.11
MeanDeviation 10 - Momentum 16 - 0.22
LinearReg 30 - JMA 36 - 0.11
MeanDeviation 4 - Momentum 30 - 0.23
MeanDeviation 2 - Momentum 36 - 0.19
MeanDeviation 6 - Momentum 10 - 0.15
LinearReg 28 - JMA 34 - 0.11
SMA 6 - JMA 8 - 0.13
LinearReg 24 - Highest 24 - 0.13
LinearReg 24 - Highest 30 - 0.13
LinearReg 24 - Highest 26 - 0.13
LinearReg 24 - Highest 28 - 0.13
LinearReg 24 - Highest 32 - 0.13
LinearReg 24 - Highest 34 - 0.13
SMA 6 - LinearReg 8 - 0.09
LinearReg 24 - JMA 26 - 0.11
LinearReg 24 - Highest 40 - 0.11
LinearReg 24 - Highest 36 - 0.13
LinearReg 24 - Highest 42 - 0.11
LinearReg 38 - JMA 46 - 0.14
LinearReg 24 - Highest 38 - 0.13
LinearReg 24 - Highest 46 - 0.11
LinearReg 24 - Highest 44 - 0.11
SMMA 6 - Lowest 8 - 0.21
SMMA 8 - JMA 16 - 0.14
SMMA 4 - LinearReg 6 - 0.13
LinearReg 36 - JMA 42 - 0.13
LinearReg 30 - Highest 32 - 0.11
LinearReg 30 - Highest 36 - 0.11
LinearReg 30 - Highest 34 - 0.11
LinearReg 30 - Highest 30 - 0.11
LinearReg 30 - Highest 42 - 0.11
LinearReg 30 - Highest 40 - 0.11
LinearReg 30 - Highest 46 - 0.11
LinearReg 30 - Highest 38 - 0.11
LinearReg 30 - Highest 44 - 0.11
MeanDeviation 6 - Momentum 6 - 0.11
Highest 16 - LinearReg 16 - 0.21
MeanDeviation 4 - Momentum 26 - 0.23
MeanDeviation 2 - Momentum 34 - 0.2
Highest 14 - LinearReg 16 - 0.21
MeanDeviation 2 - Momentum 8 - 0.1
KAMA 2 - SMA 2 - 0.11
LinearReg 32 - JMA 34 - 0.13
SMA 6 - LinearReg 6 - 0.11
LinearReg 26 - JMA 28 - 0.12
MeanDeviation 6 - Momentum 14 - 0.22
Highest 12 - LinearReg 16 - 0.21
LinearReg 34 - JMA 36 - 0.13
MeanDeviation 6 - Momentum 8 - 0.14
EMA 4 - LinearReg 4 - 0.11
MeanDeviation 4 - Momentum 28 - 0.23
SMMA 4 - Lowest 6 - 0.19
LinearReg 28 - JMA 32 - 0.12
SMA 4 - LinearReg 6 - 0.11
Highest 10 - LinearReg 16 - 0.21
LinearReg 36 - JMA 40 - 0.14
LinearReg 30 - JMA 34 - 0.13
SMMA 6 - JMA 10 - 0.13
LinearReg 38 - JMA 44 - 0.14
MeanDeviation 4 - Momentum 22 - 0.23
KAMA 4 - JMA 6 - 0.16
SMMA 10 - JMA 12 - 0.17
LinearReg 34 - JMA 34 - 0.13
SMMA 6 - LinearReg 6 - 0.14
LinearReg 32 - JMA 32 - 0.13
MeanDeviation 2 - Momentum 6 - 0.1
MeanDeviation 4 - Momentum 6 - 0.11
SMA 8 - JMA 10 - 0.1
MeanDeviation 6 - Momentum 16 - 0.22
KAMA 2 - Lowest 2 - 0.13
LinearReg 26 - JMA 26 - 0.12
SMMA 8 - Lowest 10 - 0.21
LinearReg 40 - JMA 46 - 0.16
MeanDeviation 4 - Momentum 10 - 0.15
LinearReg 36 - JMA 38 - 0.13
Highest 6 - HMA 6 - 0.12
LinearReg 38 - JMA 42 - 0.14
MeanDeviation 4 - Momentum 18 - 0.22
LinearReg 28 - JMA 30 - 0.12
LinearReg 36 - Highest 36 - 0.13
LinearReg 36 - Highest 44 - 0.13
LinearReg 36 - Highest 46 - 0.13
LinearReg 36 - Highest 40 - 0.13
SMMA 14 - Lowest 14 - 0.26
LinearReg 36 - Highest 42 - 0.13
LinearReg 36 - Highest 38 - 0.13
MeanDeviation 2 - Momentum 2 - 0.15
Highest 4 - HMA 6 - 0.12
SMA 6 - JMA 6 - 0.12
Highest 6 - LinearReg 16 - 0.21
LinearReg 30 - JMA 32 - 0.13
SMA 8 - JMA 8 - 0.1
MeanDeviation 2 - Momentum 32 - 0.18
Highest 8 - LinearReg 16 - 0.21
Highest 2 - HMA 4 - 0.15
MeanDeviation 2 - Momentum 30 - 0.19
MeanDeviation 2 - Momentum 10 - 0.15
SMMA 10 - JMA 10 - 0.16
MeanDeviation 2 - Momentum 20 - 0.14
SMA 2 - LinearReg 4 - 0.12
SMMA 8 - JMA 14 - 0.14
SMMA 6 - JMA 8 - 0.14
LinearReg 36 - JMA 36 - 0.14
LinearReg 38 - JMA 40 - 0.14
MeanDeviation 2 - Momentum 26 - 0.18
LinearReg 40 - JMA 44 - 0.14
SMMA 4 - EMA 6 - 0.13
LinearReg 28 - JMA 28 - 0.13
KAMA 4 - LinearReg 6 - 0.13
MeanDeviation 2 - Momentum 24 - 0.16
LinearReg 30 - JMA 30 - 0.13
SMMA 6 - JMA 6 - 0.13
MeanDeviation 2 - Momentum 28 - 0.19
MeanDeviation 4 - Momentum 14 - 0.22
LinearReg 38 - JMA 38 - 0.15
MeanDeviation 2 - Momentum 22 - 0.15
LinearReg 40 - JMA 42 - 0.16
JMA 2 - HMA 2 - 0.15
Highest 2 - HMA 2 - 0.15
LinearReg 2 - HMA 2 - 0.15
Lowest 2 - HMA 2 - 0.15
SMA 2 - HMA 2 - 0.15
Lowest 2 - LinearReg 2 - 0.15
SMA 2 - LinearReg 2 - 0.15
SMA 4 - JMA 4 - 0.11
Highest 2 - LinearReg 4 - 0.17
MeanDeviation 2 - Momentum 18 - 0.19
SMMA 8 - JMA 12 - 0.14
LinearReg 42 - JMA 46 - 0.16
LinearReg 38 - Highest 40 - 0.14
LinearReg 38 - Highest 42 - 0.14
LinearReg 38 - Highest 38 - 0.14
LinearReg 38 - Highest 44 - 0.14
LinearReg 38 - Highest 46 - 0.14
KAMA 2 - SMMA 2 - 0.14
SMMA 4 - LinearReg 4 - 0.12
KAMA 2 - JMA 4 - 0.12
SMMA 4 - JMA 6 - 0.12
MeanDeviation 2 - Momentum 12 - 0.14
KAMA 2 - LinearReg 4 - 0.11
SMMA 12 - Lowest 12 - 0.2
LinearReg 40 - JMA 40 - 0.15
LinearReg 42 - JMA 44 - 0.16
MeanDeviation 2 - Momentum 14 - 0.13
LinearReg 44 - JMA 46 - 0.16
EMA 4 - Lowest 4 - 0.18
MeanDeviation 2 - Momentum 4 - 0.11
SMMA 8 - JMA 10 - 0.14
LinearReg 42 - JMA 42 - 0.16
LinearReg 40 - Highest 40 - 0.16
LinearReg 40 - Highest 42 - 0.16
LinearReg 40 - Highest 44 - 0.16
LinearReg 40 - Highest 46 - 0.16
LinearReg 46 - JMA 46 - 0.17
LinearReg 44 - JMA 44 - 0.16
MeanDeviation 2 - Momentum 16 - 0.14
SMA 2 - JMA 2 - 0.16
Highest 4 - LinearReg 10 - 0.15
Highest 10 - LinearReg 14 - 0.22
EMA 2 - HMA 2 - 0.15
SMMA 10 - Lowest 10 - 0.23
Highest 8 - LinearReg 12 - 0.22
Highest 8 - LinearReg 14 - 0.22
Highest 6 - LinearReg 14 - 0.22
KAMA 2 - EMA 2 - 0.15
Highest 6 - LinearReg 12 - 0.22
SMMA 2 - EMA 2 - 0.12
KAMA 2 - JMA 2 - 0.12
EMA 2 - LinearReg 2 - 0.15
LinearReg 42 - Highest 44 - 0.16
LinearReg 42 - Highest 42 - 0.16
LinearReg 42 - Highest 46 - 0.16
KAMA 2 - LinearReg 2 - 0.14
EMA 2 - JMA 2 - 0.16
SMMA 2 - HMA 2 - 0.15
Highest 12 - LinearReg 14 - 0.22
Highest 14 - LinearReg 14 - 0.22
Highest 4 - LinearReg 6 - 0.17
Highest 6 - LinearReg 6 - 0.21
KAMA 2 - HMA 2 - 0.15
Highest 12 - LinearReg 12 - 0.2
Highest 10 - LinearReg 12 - 0.2
SMMA 2 - LinearReg 2 - 0.12
SMMA 2 - Lowest 2 - 0.16
Highest 6 - LinearReg 10 - 0.21
LinearReg 44 - Highest 46 - 0.16
LinearReg 44 - Highest 44 - 0.16
SMMA 6 - Lowest 6 - 0.21
SMMA 8 - Lowest 8 - 0.23
SMMA 2 - JMA 2 - 0.15
Highest 10 - LinearReg 10 - 0.21
Highest 8 - LinearReg 10 - 0.21
Highest 6 - LinearReg 8 - 0.21
LinearReg 46 - Highest 46 - 0.16
EMA 2 - Lowest 2 - 0.18

";

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
