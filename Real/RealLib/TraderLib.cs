using CsvHelper;
using SidesEnum;
using Stocks.Service;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using StockSolution.Services;
using StockSolution.Services.Optimizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TickEnum;

namespace RealLib
{

    /*
    * HOW TO EXECUTE Real CANDLES
    strategy.Start();
    //Candle Simulation Still Fake
    for (int i = 0; i < realCandles.Count; i++)
    {
        strategy.ProcessCandle(realCandles[i]);
    }
    strategy.Stop();
     */


    public class TraderLib
    {
        public static IConnection emulationConnection = new EmulationConnection(1000000, OrderLimitType.Value, 5000, 5, 100);
        public static Dictionary<string, StrategyGeneric> Strategies = new Dictionary<string, StrategyGeneric>();
        public static Dictionary<string, StrategyGeneric> ExpiringStrategies = new Dictionary<string, StrategyGeneric>();
        public static bool RaceCondition = false;

        private static StreamWriter _TradingLogWriter = null;
        private static StreamWriter TradingLogWriter
        {
            get
            {
                if (!(_TradingLogWriter != null))
                {
                    try
                    {
                        string partialPath = @"C:\StockHistory\Real";
                        Directory.CreateDirectory(partialPath);
                        _TradingLogWriter = new StreamWriter($"{partialPath}\\TradingLog", true);
                    }
                    catch { }
                }

                return _TradingLogWriter;
            }
        }

        private static void SortTradingLog()
        {
            try
            {
                string partialPath = @"C:\StockHistory\Real";
                Directory.CreateDirectory(partialPath);

                string[] lines = File.ReadAllLines($"{partialPath}\\TradingLog");
                string[] sortedLines = lines.OrderByDescending(line => DateTime.ParseExact(line.Substring(0, 16), "yyyy-MM-dd hh-mm", CultureInfo.InvariantCulture)).ToArray();
                List<string> noDuplicates = new List<string>();

                foreach(string line in sortedLines)
                {
                    int lastIndex = noDuplicates.Count - 1;

                    if(lastIndex != -1)
                    {
                        noDuplicates.Add(line);
                        string lastLine = noDuplicates[lastIndex];
                        int lastLength = lastLine.Length;

                        if(lastLength >= line.Length)
                        {
                            lastLine = lastLine.Substring(0, line.Length);
                            if (lastLine.Equals(line))
                            {
                                noDuplicates[lastIndex] = $"{lastLine} - Duplicate";
                            }
                            else
                            {
                                noDuplicates.Add(line);
                            }
                        }
                        else
                        {
                            noDuplicates.Add(line);
                        }
                    }
                }

                File.WriteAllLines($"{partialPath}\\TradingLog", noDuplicates);
            }
            catch { }
        }

        private static StreamWriter _ErrorWriter = null;
        private static StreamWriter ErrorWriter
        {
            get
            {
                if (!(_ErrorWriter != null))
                {
                    bool completed = false;
                    int count = 1;

                    while (!completed)
                    {
                        try
                        {
                            string partialPath = @"C:\StockHistory\Real";
                            Directory.CreateDirectory(partialPath);
                            _ErrorWriter = new StreamWriter($"{partialPath}\\ErrorLog_Trader{count}.txt");
                            completed = true;
                        }
                        catch
                        {
                            count += 1;
                        }
                    }
                }

                return _ErrorWriter;
            }
        }

        public static void RunTradingProgram(TickPeriod tickPeriod, TradingEnum tradingEnum)
        {
            OnStart(@"C:\StockHistory\Real", tickPeriod, tradingEnum);

            switch (tradingEnum)
            {
                case TradingEnum.NewStrategies:
                    NewStrategies();
                    break;
                case TradingEnum.ContinueTrading:
                    ContinueTrading();
                    break;
            }

            OnExit();
        }

        private static void NewStrategies()
        {
            Dictionary<string, StrategyGeneric> newStrategies = TraderLib.FindNewStrategies(300);
            MoveStrategiesToExpiring(ref newStrategies, "CurrentStrategies.csv", "ExpiringStrategies.csv");
        }

        private static void ContinueTrading()
        {
            InvokeTrading(ref Strategies);
            InvokeTrading(ref ExpiringStrategies);
        }

        private static void OnStart(string dataLocation, TickPeriod tickPeriod, TradingEnum tradingEnumn)
        {
            CollectorLib.DataLocation = dataLocation;

            if (TradingEnum.NewStrategies == tradingEnumn)
            {
                //ImportAndExport.CollectData(tickPeriod, ImportAndExport.GetAllSymbols(), false, true);
            }

            if (TradingEnum.ContinueTrading == tradingEnumn)
            {
                List<string> symbols = ImportAndExport.LoadStrategiesSymbols(CollectorLib.DataLocation, "CurrentStrategies.csv");

                symbols.AddRange(ImportAndExport.LoadStrategiesSymbols(CollectorLib.DataLocation, "ExpiringStrategies.csv"));
                ImportAndExport.CollectData(TickPeriod.Daily, symbols, true, true);
            }

            Strategies = CollectorLib.LoadStrategies(ref emulationConnection, tickPeriod, "CurrentStrategies.csv", false);
            ExpiringStrategies = CollectorLib.LoadStrategies(ref emulationConnection, tickPeriod, "ExpiringStrategies.csv", true);
        }

        private static void OnExit()
        {
            //Doesn't Save Disabled Values 
            CollectorLib.SaveStrategies("CurrentStrategies.csv", Strategies);
            CollectorLib.SaveStrategies("ExpiringStrategies.csv", ExpiringStrategies);

            TradingLogWriter.Close();
            _TradingLogWriter = null;
            //Sort Log
            SortTradingLog();
        }

        //Unused For Future Implementations (needs when automated)
        private static void CancelNonActiveOrders(IConnection connection, List<string> activeSymbols, List<string> ignoreSymbols_Csv)
        {
            Dictionary<SecurityInfo, Order> orders = connection.LoadOrders();
            //List without IgnoreSymbols
            List<SecurityInfo> orderSecurityInfos = orders.Keys.Where(securityInfo => !ignoreSymbols_Csv.Any(ignoreSymbol => securityInfo.SecurityID.Equals(ignoreSymbol))).ToList();
            
            //Order SecurityInfos
            foreach (SecurityInfo securityInfo in orderSecurityInfos)
            {
                bool isActive = false;
                
                //Active
                foreach (string symbol in activeSymbols)
                {
                    if (securityInfo.SecurityID.ToLower().Equals(symbol.ToLower()))
                    {
                        isActive = true;
                    }
                }

                if(isActive == false)
                {
                    connection.CancelOrder(securityInfo);
                }
            }
        }

        //Unused For Future Implementations (needs when automated)
        private static void CleanupDisplacedOrders(IConnection connection, string symbol)
        {

        }

        private static void InvokeTrading(ref Dictionary<string, StrategyGeneric> strategies)
        {
            List<string> symbols = strategies.Keys.ToList<string>();

            foreach (string symbol in symbols)
            {
                List<Candle> candles = CollectorLib.GetSecurityInfo(TickPeriod.Daily, symbol).Candles;

                if (candles != null)
                {
                    //Could OtherWise Cause Troubles With Disabling Strategies, should add another date to strategies instead of this
                    DateTime executionStart = strategies[symbol].LastExecution;

                    foreach (Candle candle in CollectorLib.GetSecurityInfo(TickPeriod.Daily, symbol).Candles)
                    {
                        bool isNewerThanLastExecution = strategies[symbol].LastExecution.CompareTo(candle.CloseTime) < 0;
                        if (isNewerThanLastExecution)
                        {
                            //Simulate Real Values
                            strategies[symbol] = AddCandleToStrategy(strategies[symbol], candle);
                        }
                    }

                    strategies[symbol].LastExecution = executionStart;
                }
                else
                {
                    strategies[symbol] = AddCandleToStrategy(strategies[symbol], null);
                }

            }
        }

        private static void MoveStrategiesToExpiring(ref Dictionary<string, StrategyGeneric> newStrategies, string strategiesFilename, string expiredStrategiesFilename)
        {
            string partialPath = CollectorLib.DataLocation;
            Dictionary<string, StrategyGeneric> strategies1 = CollectorLib.LoadStrategies(ref emulationConnection, TickPeriod.Daily, strategiesFilename, true);
            Dictionary<string, StrategyGeneric> expiringStrategies = CollectorLib.LoadStrategies(ref emulationConnection, TickPeriod.Daily, expiredStrategiesFilename, true);

            foreach (string symbol in strategies1.Keys)
            {
                expiringStrategies[symbol] = strategies1[symbol];
            }

            //If Same Symbol Exist Remove And Maybe Cancel Current Orders
            List<string> symbols = newStrategies.Keys.ToList();
            foreach (string symbol in newStrategies.Keys)
            {
                if (expiringStrategies.ContainsKey(symbol))
                {
                    OrderKeptOrCancled(symbol, newStrategies[symbol], expiringStrategies[symbol]);
                    expiringStrategies.Remove(symbol);
                }
            }

            CollectorLib.SaveStrategies(expiredStrategiesFilename, Strategies);
            Strategies = new Dictionary<string, StrategyGeneric>();
            ExpiringStrategies = expiringStrategies;

            if (File.Exists($"{CollectorLib.DataLocation}\\{strategiesFilename}"))
            {
                File.Delete($"{CollectorLib.DataLocation}\\{strategiesFilename}");
            }

            //Add New Strategies
            Strategies = newStrategies;
        }

        /**
         * This is for strategies where new and expired contains same symbol
         * */ 
        private static void OrderKeptOrCancled(string symbol, StrategyGeneric newStrategy, StrategyGeneric expiredStrategy)
        {
            Sides newDirection = newStrategy.GetDirection();
            Sides expiredDirection = expiredStrategy.GetDirection();

            SecurityInfo securityInfo = CollectorLib.GetSecurityInfo(TickPeriod.Daily, symbol);

            Candle lastCandle = null;
            foreach (Candle candle in securityInfo.Candles)
            {
                bool isNewerThanLastExecution = newStrategy.LastExecution.CompareTo(candle.CloseTime) < 0;

                if (isNewerThanLastExecution)
                {
                    break;
                }

                newStrategy.LongIndicator.Process(candle.ClosePrice, true);
                newStrategy.ShortIndicator.Process(candle.ClosePrice, true);
                expiredStrategy.LongIndicator.Process(candle.ClosePrice, true);
                expiredStrategy.ShortIndicator.Process(candle.ClosePrice, true);
                lastCandle = candle;
            }

            if (newDirection != expiredDirection)
            {
                if (expiredDirection == Sides.Buy)
                {
                    TradingLogWriter.WriteLine($"{lastCandle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {symbol} - Cancel Orders (Order Type: Buy)");
                }

                if (expiredDirection == Sides.Sell)
                {
                    TradingLogWriter.WriteLine($"{lastCandle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {symbol} - Cancel Orders (Order Type: Sell)");
                }
            }
        }

        private static StrategyGeneric AddCandleToStrategy(StrategyGeneric strategy, Candle candle)
        {
            if (candle != null)
            {
                //Old Values
                bool previousDisabled = strategy.IsDisabled;
                Sides previousDirection = strategy.GetDirection();

                //Process Candle
                strategy.Start();
                strategy.ProcessCandle(candle);
                strategy.Stop();

                //New Values
                bool currentDisabled = strategy.IsDisabled;
                Sides currentDirection = strategy.GetDirection();

                //Create New Order And Cancel Old
                if (!previousDisabled && !currentDisabled && previousDirection != currentDirection && strategy.IsStrategyExpiring == false)
                {
                    if (currentDirection == Sides.Buy)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {strategy.SecurityID} - Order Type: Buy - Current Price: {candle.ClosePrice}");
                    }

                    if (currentDirection == Sides.Sell)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {strategy.SecurityID} - Order Type: Sell - Current Price: {candle.ClosePrice}");
                    }

                    TradingLogWriter.Flush();
                }

                //Cancel Current Orders
                if (!previousDisabled && currentDisabled)
                {
                    if (previousDirection == Sides.Buy)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {strategy.SecurityID} - Cancel Orders (Order Type: Buy)");
                    }

                    if (previousDirection == Sides.Sell)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - ID: {strategy.SecurityID} - Cancel Orders (Order Type: Sell)");
                    }

                    //Discontinue state
                    if (strategy.IsStrategyExpiring == true)
                    {
                        strategy.IsActive = false;
                    }

                    TradingLogWriter.Flush();
                }
            }
            else
            {
                strategy.IsActive = false;
                TradingLogWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh-mm")} - ID: {strategy.SecurityID} - Candles Invalied - Cancel Orders If Connection Valied");
                TradingLogWriter.Flush();
            }

            return strategy;
        }

        private static Dictionary<string, StrategyGeneric> FindNewStrategies(int minNrOfTestValues)
        {
            Dictionary<string, StrategyGeneric> newStrategies = new Dictionary<string, StrategyGeneric>();
            Optimizer optimizer = new Optimizer();
            OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
            int nrOfTestValues = 90;
            int testMoney = 100000;
            int orderLimit = testMoney / 10;
            DateTime currentDateTime = DateTime.Now;
            DateTimeOffset dateMayNotBeOlderThan = DateTimeOffset.UtcNow.AddDays(-(minNrOfTestValues * 1.7));

            try
            {
                // #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
                //var startTime = DateTime.Now;
                System.Console.WriteLine(currentDateTime);


                #region LOAD INDEVIDUAL CANDLES - Distibuted Load - Memory Efficient
                string fullPath = ImportAndExport.GetFullPath(TickPeriod.Daily);

                string storagePath = ImportAndExport.GetFullPath(TickPeriod.Daily);
                List<string> failedSecurities = new List<string>();
                IList<string> securityIDs = LoaderService.GetSecurityIDs(storagePath);

                Parallel.ForEach(securityIDs, new ParallelOptions { MaxDegreeOfParallelism = 8 }, securityID =>
                //foreach (string securityID in securityIDs)
                {
                    try
                    {
                        //Load Candles for ID
                        SecurityInfo securityInfo = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), storagePath, securityID, dateMayNotBeOlderThan.DateTime, currentDateTime);

                        if (securityInfo.Candles.Count >= minNrOfTestValues)
                        {
                            int firstTestIndex = securityInfo.Candles.Count - minNrOfTestValues;
                            DateTimeOffset startDateTest = securityInfo.Candles[firstTestIndex].CloseTime;
                            if (startDateTest.CompareTo(dateMayNotBeOlderThan) >= 0)
                            {
                                //Restrict Candle Size
                                List<Candle> candles = new List<Candle>();
                                for (int j = securityInfo.Candles.Count - minNrOfTestValues; j < securityInfo.Candles.Count; j++)
                                {
                                    candles.Add(securityInfo.Candles[j]);
                                }
                                //BEGIN
                                securityInfo.Candles = candles;
                                StrategyGeneric strategy = FindStrategy(securityInfo, optimizer, optimizerOptions, nrOfTestValues);


                                //Try To Add Strategy
                                if (strategy != null)
                                {
                                    for (int trie = 0; trie < 5; trie++)
                                    {
                                        try
                                        {
                                            newStrategies[securityInfo.SecurityID] = strategy;
                                        }
                                        catch
                                        {
                                            Thread.Sleep(50);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        failedSecurities.Add(securityID);
                        System.Console.WriteLine(e.ToString());
                        System.Console.WriteLine(new StackTrace(e, true).GetFrame(0).GetFileLineNumber());
                    }
                }
                );
                failedSecurities.ForEach(failed => System.Console.WriteLine("Failed Security: " + failed));

                #endregion
            }
            catch (Exception e)
            {
                while (RaceCondition) { Thread.Sleep(5); }
                RaceCondition = true;
                try
                {
                    ErrorWriter.WriteLineAsync(e.ToString()).Wait();
                    ErrorWriter.WriteLineAsync(e.Message).Wait();
                    ErrorWriter.WriteLineAsync(e.Data.ToString()).Wait();
                    ErrorWriter.FlushAsync().Wait();
                }
                catch { }
                finally { RaceCondition = false; }
            }


            return newStrategies;
        }

        private static StrategyGeneric FindStrategy(SecurityInfo securityInfo, Optimizer optimizer, OptimizerOptions optimizerOptions, int nrOfTestValues)
        {
            try
            {
                #region Set Candles
                //Get Test Candles
                List<Candle> candles = securityInfo.Candles.ToList();
                int keyCount = securityInfo.Candles.Count;
                int beginIndexTest = keyCount - (optimizer.RecursiveTests * nrOfTestValues + optimizerOptions.IndicatorLength.Max);
                int testCount = optimizerOptions.IndicatorLength.Max + optimizer.RecursiveTests * nrOfTestValues;


                List<Candle> testCandles = candles.GetRange(beginIndexTest, testCount);
                #endregion


                try
                {
                    #region Find Strategy / Best Indicator Pairs + Its options

                    optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, nrOfTestValues, 1);
                    #endregion

                    #region Create The Strategy
                    //EmulationConnection emulationConnection = new EmulationConnection(testMoney, OrderLimitType.Value, orderLimit, 1, 80);
                    if (optimizerOptions.BestIndicatorPair.ShortIndicator != null && optimizerOptions.BestIndicatorPair.LongIndicator != null)
                    {
                        decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                        StrategyGeneric strategy = new StrategyGeneric(TraderLib.emulationConnection, securityInfo, optimizerOptions);

                        return strategy;
                    }
                    #endregion
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("NullReferenceException - No Strategy Found - " + securityInfo);
                }

                RaceCondition = false;
            }
            catch (Exception e)
            {
                while (RaceCondition) { Thread.Sleep(5); }
                RaceCondition = true;
                try
                {
                    ErrorWriter.WriteLineAsync(e.ToString()).Wait();
                    ErrorWriter.WriteLineAsync(e.Message).Wait();
                    ErrorWriter.WriteLineAsync(e.Data.ToString()).Wait();
                    ErrorWriter.WriteLineAsync(new StackTrace(e, true).GetFrame(0).GetFileLineNumber().ToString());
                    ErrorWriter.FlushAsync().Wait();
                }
                catch { }
                finally { RaceCondition = false; }
            }

            //If not suatable
            return null;
        }
    }
}