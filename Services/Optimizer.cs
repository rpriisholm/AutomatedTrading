using CsvHelper;
using SandS.Algorithm.Library.SortNamespace;
using Services;
using Stocks.Service;
using StockSharp.Algo.Indicators;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public static IndicatorPair SimulateIndicatorPair(IndicatorPair indicatorPair, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled, decimal loseLimit)
        {
            int initialMoney = 100000;
            int orderLimit = initialMoney / 10;
            int maxInvestedPct = 80;
            SecurityInfo securityInfo = new SecurityInfo() { SecurityID = "TestID" };

            if (!(indicatorPair != null))
            {
                Debug.WriteLine("IndicatorPair Should Never Be Null");
                throw new ArgumentNullException();
            }

            EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, maxInvestedPct);
            StrategyGeneric strategyGeneric = null;

            strategyGeneric = new StrategyGeneric(emulationConnection, securityInfo, indicatorPair, isSellEnabled, isBuyEnabled, loseLimit);
            indicatorPair.StrategyBasic = strategyGeneric;

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
            indicatorPair.OrdersCount = strategyGeneric.OrderCount;

            return indicatorPair;
        }

        // Simulate IndicatorPairs
        /* TODO 06/12/2018 */
        public static void SimulateIndicatorPairs(ref List<IndicatorPair> indicatorPairs, List<Candle> simulateCandles, bool isSellEnabled, bool isBuyEnabled, decimal loseLimit)
        {
            //Parallel.ForEach(indicatorPairs, indicatorPair =>
            foreach (IndicatorPair indicatorPair in indicatorPairs)
            //for(int i = 0; i < indicatorPairs.Count; i++)
            {
                int initialMoney = 100000;
                int orderLimit = initialMoney / 10;
                int maxInvestedPct = 80;
                SecurityInfo securityInfo = new SecurityInfo() { SecurityID = "TestID" };

                if (!(indicatorPair != null))
                {
                    Debug.WriteLine("IndicatorPair Should Never Be Null");
                    throw new ArgumentNullException();
                }

                EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, maxInvestedPct);
                StrategyGeneric strategyGeneric = null;

                strategyGeneric = new StrategyGeneric(emulationConnection, securityInfo, indicatorPair, isSellEnabled, isBuyEnabled, loseLimit);
                indicatorPair.StrategyBasic = strategyGeneric;

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
                indicatorPair.OrdersCount = strategyGeneric.OrderCount;
            }
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

        /* Legacy
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
    */

        /* TODO 06/12/2018 */
        public OptimizerOptions FindBestOptions(OptimizerOptions optimizerOptions, List<Candle> candles, int leverage)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            int minCandles = (this.GetMaxIndicatorLength(optimizerOptions.EnabledPairs) + optimizerOptions.NrOfTestValues * optimizerOptions.RecursiveTests);
            if (candles.Count < minCandles)
            {
                throw new InvalidDataException();
            }
            int initIndex = candles.Count - (this.GetMaxIndicatorLength(optimizerOptions.EnabledPairs) + optimizerOptions.NrOfTestValues * optimizerOptions.RecursiveTests);
            List<Candle> initialCandles = candles.GetRange(initIndex, this.GetMaxIndicatorLength(optimizerOptions.EnabledPairs));
            //List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles, optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);
            Optimizer.CreateIndicatorPairsInstance = new Optimizer.CreateIndicatorPairsClass(optimizerOptions.EnabledPairs);
            List<IndicatorPair> indicatorPairs = Optimizer.CreateIndicatorPairs(initialCandles);

            //START SIMULATION - RUN X TIMES
            for (int recursiveTests = 0; recursiveTests < optimizerOptions.RecursiveTests; recursiveTests++)
            {
                List<Candle> currentCandles = candles.GetRange(candles.Count - (1 + optimizerOptions.NrOfTestValues * (optimizerOptions.RecursiveTests - recursiveTests)), optimizerOptions.NrOfTestValues);

                /* TODO 06/12/2018 */
                //Simulate IndicatorPairs With CurrentCandles

                decimal loseLimitMax = -1m;
                Optimizer.SimulateIndicatorPairs(ref indicatorPairs, currentCandles, OptimizerOptions.IsSellEnabled, OptimizerOptions.IsBuyEnabled, loseLimitMax);

                //Missing Recursive AND FILTER USING CHOOSEN SETTINGS
                List<IndicatorPair> filteredIndicatorPairs = new List<IndicatorPair>();
                for (int i = 0; i < indicatorPairs.Count; i++)
                {
                    bool isLargerOrEqualToMinOrders = indicatorPairs[i].OrdersCount >= optimizerOptions.MinOrders;
                    bool isLessOrEqualToMaxOrders = indicatorPairs[i].OrdersCount <= optimizerOptions.MaxOrders;
                    bool isLargerThanLastResult = indicatorPairs[i].LastResult >= optimizerOptions.MinProfitPct;
                    bool isLargerThanLoseLimitMin = indicatorPairs[i].LoseLimitMin >= optimizerOptions.LoseLimitMin;


                    if (isLargerOrEqualToMinOrders &&
                        isLessOrEqualToMaxOrders &&
                        isLargerThanLastResult &&
                        isLargerThanLoseLimitMin)
                    {
                        filteredIndicatorPairs.Add(indicatorPairs[i]);

                        if (false)
                        {
                            string path = @"C:\StockHistory\Real\Simulation_Filtered.csv";
                            bool fileExist = File.Exists(path);

                            StreamWriter streamWriter = new StreamWriter(path, fileExist);
                            CsvWriter csvWriter = new CsvWriter(streamWriter);

                            if (!fileExist)
                            {
                                csvWriter.WriteField("ShortIndicator");
                                csvWriter.WriteField("LongIndicator");
                                csvWriter.WriteField("Orders");
                                csvWriter.WriteField("LastResult");
                                csvWriter.WriteField("LoseLimitMin");
                                csvWriter.WriteField("RecursiveTests");
                                csvWriter.NextRecord();
                                csvWriter.Flush();
                            }

                            csvWriter.WriteField(indicatorPairs[i].ShortIndicator);
                            csvWriter.WriteField(indicatorPairs[i].LongIndicator);
                            csvWriter.WriteField(indicatorPairs[i].OrdersCount);
                            csvWriter.WriteField(indicatorPairs[i].LastResult);
                            csvWriter.WriteField(indicatorPairs[i].LoseLimitMin);
                            csvWriter.WriteField(recursiveTests);
                            csvWriter.NextRecord();
                            csvWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    /*
                    if (true)
                    {
                        string path = @"C:\StockHistory\Real\Simulation_Filtered.csv";
                        bool fileExist = File.Exists(path);

                        StreamWriter streamWriter = new StreamWriter(path, fileExist);
                        CsvWriter csvWriter = new CsvWriter(streamWriter);

                        
                        if (!fileExist)
                        {
                            csvWriter.WriteField("ShortIndicator");
                            csvWriter.WriteField("LongIndicator");
                            csvWriter.WriteField("Orders");
                            csvWriter.WriteField("LastResult");
                            csvWriter.WriteField("LoseLimitMin");
                            csvWriter.WriteField("RecursiveTests");
                            csvWriter.WriteField("isLargerOrEqualToMinOrders");
                            csvWriter.WriteField("isLessOrEqualToMaxOrders");
                            csvWriter.WriteField("isLargerThanLastResult");
                            csvWriter.WriteField("isLargerThanLoseLimitMin");
                            csvWriter.NextRecord();
                            csvWriter.Flush();
                        }

                        csvWriter.WriteField(indicatorPairs[i].ShortIndicator);
                        csvWriter.WriteField(indicatorPairs[i].LongIndicator);
                        csvWriter.WriteField(indicatorPairs[i].OrdersCount);
                        csvWriter.WriteField(indicatorPairs[i].LastResult);
                        csvWriter.WriteField(indicatorPairs[i].LoseLimitMin);
                        csvWriter.WriteField(recursiveTests);
                        csvWriter.WriteField(isLargerOrEqualToMinOrders);
                        csvWriter.WriteField(isLessOrEqualToMaxOrders);
                        csvWriter.WriteField(isLargerThanLastResult);
                        csvWriter.WriteField(isLargerThanLoseLimitMin);
                        csvWriter.NextRecord();
                        csvWriter.Flush();
                        streamWriter.Close();
                        
                    }*/
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

        public IndicatorPair FindIndicator(string enabledPairs, string shortIndicator, string longIndicator, decimal loseLimit, List<Candle> initialCandles)
        {
            //List<LengthIndicator<decimal>> indicators = CreateIndicators(out differentIndicators, minIndicatorLength, maxIndicatorLength, interval);
            Optimizer.CreateIndicatorPairsInstance = new Optimizer.CreateIndicatorPairsClass(enabledPairs);
            List<IndicatorPair> indicatorPairs = CreateIndicatorPairs(initialCandles);
            IndicatorPair resultIndicatorPair = null;

            if (loseLimit < 0)
            {
                loseLimit = -loseLimit;
            }

            string indicatorPairString = shortIndicator.Trim() + " - " + longIndicator.Trim() + " - " + loseLimit.ToString(new CultureInfo("en-US"));

            foreach (IndicatorPair currentIndicatorPair in indicatorPairs)
            {
                string currentIndicatorPairString = currentIndicatorPair.ToString();
                if (currentIndicatorPairString.Equals(indicatorPairString))
                {
                    resultIndicatorPair = currentIndicatorPair;
                    break;
                }
            }

            return resultIndicatorPair;
        }


        private bool IsIndicatorPairDisabled(LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            StringReader strReader = new StringReader(Optimizer._DisabledPairs);
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


        private bool IsIndicatorPairEnabled(string enabledPairs, LengthIndicator<decimal> shortIndicator, LengthIndicator<decimal> longIndicator)
        {
            //StringReader strReader = new StringReader(Optimizer._EnabledPairs);
            StringReader strReader = new StringReader(enabledPairs);
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

        public int GetMaxIndicatorLength(string enabledPairs)
        {
            if (_MaxIndicatorLength <= 0)
            {
                //StringReader strReader = new StringReader(Optimizer._EnabledPairs);
                StringReader strReader = new StringReader(enabledPairs);
                while (strReader.Peek() >= 0)
                {
                    string enabledIndicatorPair = strReader.ReadLine();
                    if (!string.IsNullOrEmpty(enabledIndicatorPair))
                    {
                        string[] numbers = Regex.Split(enabledIndicatorPair, @"\D+");

                        int length = int.Parse(numbers[1]);
                        if (length > this._MaxIndicatorLength)
                        {
                            this._MaxIndicatorLength = length;
                        }

                        length = int.Parse(numbers[2]);
                        if (length > this._MaxIndicatorLength)
                        {
                            this._MaxIndicatorLength = length;
                        }
                    }
                }
            }

            return this._MaxIndicatorLength;
        }

        /*
        public static List<IndicatorPair> CreateIndicatorPairs(List<Candle> initialCandles)
        {
            return CreateIndicatorPairs(initialCandles, Optimizer._EnabledPairs);
        }
        */

        private static readonly object _lock = new object();


        public class CreateIndicatorPairsClass
        {
            private ConcurrentDictionary<int, LengthIndicator<decimal>> ShortIndicators = new ConcurrentDictionary<int, LengthIndicator<decimal>>();
            private ConcurrentDictionary<int, LengthIndicator<decimal>> LongIndicators = new ConcurrentDictionary<int, LengthIndicator<decimal>>();
            private ConcurrentDictionary<int, decimal> LoseLimits = new ConcurrentDictionary<int, decimal>();

            public CreateIndicatorPairsClass(string enabledPairs)
            {
                //Singleton
                StringReader strReader = new StringReader(enabledPairs);
                int index = 0;
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

                        loseLimit = -(decimal.Parse(indicators[2], new CultureInfo("en-US")));

                        if (!(shortIndicator != null))
                        {
                            throw new ArgumentNullException();
                        }

                        if (!(longIndicator != null))
                        {
                            throw new ArgumentNullException();
                        }

                        /*
                        if (loseLimit == 0)
                        {
                            throw new ArgumentNullException();
                        }*/

                        LoseLimits[index] = loseLimit;
                        ShortIndicators[index] = shortIndicator;
                        LongIndicators[index] = longIndicator;
                        index = index + 1;
                    }
                }


            }

            public List<IndicatorPair> CopyAndInitializeIndicatorPairs(List<Candle> initialCandles)
            {
                List<IndicatorPair> indicatorPairs = new List<IndicatorPair>();

                for (int i = 0; i < ShortIndicators.Count; i++)
                {
                    LengthIndicator<decimal> shortIndicatorClone = ShortIndicators[i].Clone() as LengthIndicator<decimal>;
                    LengthIndicator<decimal> longIndicatorClone = LongIndicators[i].Clone() as LengthIndicator<decimal>;
                    Entity.Models.LengthIndicator shortIndicatorAdapter = new LengthIndicator(shortIndicatorClone);
                    Entity.Models.LengthIndicator longIndicatorAdapter = new LengthIndicator(longIndicatorClone);

                    if (initialCandles.Count >= shortIndicatorAdapter.Indicator.Length)
                    {
                        // Init candles
                        for (int j = initialCandles.Count - shortIndicatorAdapter.Indicator.Length; j < initialCandles.Count; j++)
                        {
                            shortIndicatorAdapter.Indicator.Process(initialCandles[j].ClosePrice);
                        }
                        // Init candles
                        for (int j = initialCandles.Count - longIndicatorAdapter.Indicator.Length; j < initialCandles.Count; j++)
                        {
                            longIndicatorAdapter.Indicator.Process(initialCandles[j].ClosePrice);
                        }
                    }
                    decimal loseLimit = LoseLimits[i];
                    indicatorPairs.Add(new IndicatorPair(shortIndicatorAdapter, longIndicatorAdapter, loseLimit));
                }

                return indicatorPairs;
            }
        }

        public static CreateIndicatorPairsClass CreateIndicatorPairsInstance = null;


        public static List<IndicatorPair> CreateIndicatorPairs(List<Candle> initialCandles)
        {
            return CreateIndicatorPairsInstance.CopyAndInitializeIndicatorPairs(initialCandles);
        }



        /*
         * From SQL
         * ShortIndicator - LongIndicator 
         */
        //public static readonly string _EnabledPairs = ValueCollections.PermanentValues.EnabledPairs;

        public static readonly string _TestPairs = Permanent.PermanentValues.TestPairs;

        public static readonly string _DisabledPairs = @"";

    }
}
