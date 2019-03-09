using SidesEnum;
using Stocks.Service;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
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
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                        Debug.WriteLine(e.Message);
                        Debug.WriteLine(e.Data.ToString());
                    }
                }

                return _TradingLogWriter;
            }
        }

        private static void SortTradingLog()
        {
            string partialPath = @"C:\StockHistory\Real";
            Directory.CreateDirectory(partialPath);

            string[] lines = File.ReadAllLines($"{partialPath}\\TradingLog");
            //string[] sortedLines = lines.OrderByDescending(line => DateTime.ParseExact(line.Substring(0, 16), "yyyy-MM-dd hh-mm", CultureInfo.InvariantCulture)).ToArray();
            List<string> noDuplicates = new List<string>();

            Dictionary<DateTime, List<string>> sortedDictonary = new Dictionary<DateTime, List<string>>();

            //string[] sortedLines = lines.OrderByDescending(line => DateTime.ParseExact(line.Substring(0, 16), "yyyy-MM-dd hh-mm", CultureInfo.InvariantCulture)).ToArray();
            foreach (string line in lines)
            {
                DateTime date = DateTime.ParseExact(line.Substring(0, 16), "yyyy-MM-dd hh-mm", CultureInfo.InvariantCulture);
                if (!sortedDictonary.ContainsKey(date))
                {
                    sortedDictonary[date] = new List<string>();
                }

                sortedDictonary[date].Add(line);
            }

            //SortLines
            List<DateTime> keys = sortedDictonary.Keys.ToList();
            foreach (DateTime key in keys)
            {
                sortedDictonary[key] = sortedDictonary[key].OrderByDescending(line => line).ToList();
            }

            //Begin Writing
            foreach (DateTime key in sortedDictonary.Keys.OrderByDescending(key => key))
            {
                foreach (string line in sortedDictonary[key])
                {
                    int lastIndex = noDuplicates.Count - 1;

                    if (lastIndex != -1)
                    {
                        string lastLine = noDuplicates[lastIndex];
                        int lastLength = lastLine.Length;

                        if (lastLength >= line.Length)
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
                    else
                    {
                        noDuplicates.Add(line);
                    }
                }
            }

            File.WriteAllLines($"{partialPath}\\TradingLog", noDuplicates);
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
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
                            Debug.WriteLine(e.Message);
                            Debug.WriteLine(e.Data.ToString());
                            count += 1;
                        }
                    }
                }

                return _ErrorWriter;
            }
        }

        public static void RunTradingProgram(TickPeriod tickPeriod, TradingEnum tradingEnum)
        {
            ImportAndExport.MinStockPrice = 2m;
            bool isDownloadEnabled = true;
            //bool isDownloadEnabled = true;
            OnStart(@"C:\StockHistory\Real", tickPeriod, tradingEnum, isDownloadEnabled);

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
            Dictionary<string, StrategyGeneric> newStrategies = TraderLib.FindNewStrategies(300, ImportAndExport.MinStockPrice);
            MoveStrategiesToExpiring(ref newStrategies, "CurrentStrategies.csv", "ExpiringStrategies.csv");
        }

        private static void ContinueTrading()
        {
            InvokeTrading(ref Strategies);
            InvokeTrading(ref ExpiringStrategies);
        }

        private static void OnStart(string dataLocation, TickPeriod tickPeriod, TradingEnum tradingEnum, bool isDownloadEnabled)
        {
            CollectorLib.DataLocation = dataLocation;

            if (isDownloadEnabled)
            {
                if (TradingEnum.NewStrategies == tradingEnum)
                {
                    ImportAndExport.CollectData(tickPeriod, ImportAndExport.GetAllSymbols(), false, true);
                }

                if (TradingEnum.ContinueTrading == tradingEnum)
                {
                    List<string> symbols = ImportAndExport.LoadStrategiesSymbols(CollectorLib.DataLocation, "CurrentStrategies.csv");
                    symbols.AddRange(ImportAndExport.LoadStrategiesSymbols(CollectorLib.DataLocation, "ExpiringStrategies.csv"));

                    ImportAndExport.CollectData(TickPeriod.Daily, symbols, true, true);
                }
            }

            Strategies = CollectorLib.LoadStrategies(ref emulationConnection, tickPeriod, "CurrentStrategies.csv", false);
            ExpiringStrategies = CollectorLib.LoadStrategies(ref emulationConnection, tickPeriod, "ExpiringStrategies.csv", true);
        }

        private static void OnExit()
        {
            //Doesn't Save Disabled Values 
            CollectorLib.SaveStrategies("CurrentStrategies.csv", Strategies);
            CollectorLib.SaveStrategies("ExpiringStrategies.csv", ExpiringStrategies);

            TradingLogWriter.Flush();
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

                if (isActive == false)
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
                //USED FOR WRITING
                DateTime executionStart = strategies[symbol].LastExecution;

                List<Candle> candles = CollectorLib.GetSecurityInfo(TickPeriod.Daily, symbol).Candles;

                if (candles != null)
                {
                    //Could OtherWise Cause Troubles With Disabling Strategies, should add another date to strategies instead of this
                    foreach (Candle candle in candles)
                    {
                        bool isNewerThanLastExecution = strategies[symbol].LastExecution.CompareTo(candle.CloseTime) < 0;
                        if (isNewerThanLastExecution)
                        {
                            //Simulate Real Values
                            strategies[symbol] = AddCandleToStrategy(strategies[symbol], candle);
                        }
                    }

                    // strategies[symbol].LastExecution = executionStart;
                }
                else
                {
                    strategies[symbol] = AddCandleToStrategy(strategies[symbol], null);
                }

                //USED FOR WRITING
                strategies[symbol].LastExecution = executionStart;
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
                bool isNewerThanLastExecution = newStrategy.LastExecution.CompareTo(candle.CloseTime) <= 0;

                if (isNewerThanLastExecution)
                {
                    break;
                }

                newStrategy.IndicatorPair.LongIndicator.Process(candle.ClosePrice, true);
                newStrategy.IndicatorPair.ShortIndicator.Process(candle.ClosePrice, true);
                expiredStrategy.IndicatorPair.LongIndicator.Process(candle.ClosePrice, true);
                expiredStrategy.IndicatorPair.ShortIndicator.Process(candle.ClosePrice, true);
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

                //No Change
                if (!previousDisabled && !currentDisabled && previousDirection == currentDirection && strategy.IsStrategyExpiring == false)
                {
                    TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh-mm")} - (No Change) ID: {strategy.SecurityID}");
                }

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
                if ((!previousDisabled && currentDisabled) || (strategy.IsStrategyExpiring && previousDirection != currentDirection))
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

            string s = candle.CloseTime.ToLongDateString();
            return strategy;
        }

        private static Dictionary<string, StrategyGeneric> FindNewStrategies(int minNrOfTestValues, decimal minStockValue)
        {
            DateTime currentDateTime = DateTime.Now;
            return FindNewStrategies(minNrOfTestValues, minStockValue, currentDateTime);
        }

        public static Dictionary<string, StrategyGeneric> FindNewStrategies(int minNrOfTestValues, decimal minStockValue, DateTime endDate)
        {
            Dictionary<string, StrategyGeneric> newStrategies = new Dictionary<string, StrategyGeneric>();
            Optimizer optimizer = new Optimizer();
            OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
            //int nrOfTestValues = 90;
            int testMoney = 100000;
            int orderLimit = testMoney / 10;
            DateTime currentDateTime = endDate;
            DateTimeOffset dateMayNotBeOlderThan = DateTime.SpecifyKind(currentDateTime, DateTimeKind.Utc).AddDays(-(minNrOfTestValues * 1.7));

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

                //Parallel.ForEach(securityIDs, new ParallelOptions { MaxDegreeOfParallelism = 8 }, securityID =>
                foreach (string securityID in securityIDs)
                {
                    try
                    {
                        //Load Candles for ID
                        SecurityInfo securityInfo = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), storagePath, securityID, dateMayNotBeOlderThan.DateTime, currentDateTime);

                        if (securityInfo.Candles.Count >= minNrOfTestValues)
                        {
                            if (securityInfo.Candles[securityInfo.Candles.Count - 1].ClosePrice > minStockValue)
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
                                    StrategyGeneric strategy = FindStrategy(securityInfo, optimizer, optimizerOptions);


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
                    }
                    catch (Exception e)
                    {
                        failedSecurities.Add(securityID);
                        System.Console.WriteLine(e.ToString());
                        System.Console.WriteLine(new StackTrace(e, true).GetFrame(0).GetFileLineNumber());

                        ErrorWriter.WriteLineAsync(e.ToString()).Wait();
                        ErrorWriter.WriteLineAsync(e.Message).Wait();
                        ErrorWriter.WriteLineAsync(e.Data.ToString()).Wait();
                        ErrorWriter.FlushAsync().Wait();
                        Debug.WriteLine(e.ToString());
                        Debug.WriteLine(e.Message);
                        Debug.WriteLine(e.Data.ToString());
                    }

                    finally { }

                }
                //);
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
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.Data.ToString()); ;
                }
                catch (Exception ei)
                {
                    Debug.WriteLine(ei.ToString());
                    Debug.WriteLine(ei.Message);
                    Debug.WriteLine(ei.Data.ToString());
                }
                finally { RaceCondition = false; }
            }

            finally { }

            return newStrategies;
        }

        /* TODO 06/12/2018 */
        private static StrategyGeneric FindStrategy(SecurityInfo securityInfo, Optimizer optimizer, OptimizerOptions optimizerOptions)
        {
            try
            {
                #region Set Candles
                //Get Test Candles
                List<Candle> candles = securityInfo.Candles.ToList();
                int keyCount = securityInfo.Candles.Count;
                int beginIndexTest = keyCount - (optimizer.RecursiveTests * optimizerOptions.NrOfTestValues + optimizer.GetMaxIndicatorLength());
                int testCount = optimizer.GetMaxIndicatorLength() + optimizer.RecursiveTests * optimizerOptions.NrOfTestValues;


                List<Candle> testCandles = candles.GetRange(beginIndexTest, testCount);
                #endregion


                try
                {
                    #region Find Strategy / Best Indicator Pairs + Its options

                    optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, 1);
                    #endregion

                    #region Create The Strategy
                    //EmulationConnection emulationConnection = new EmulationConnection(testMoney, OrderLimitType.Value, orderLimit, 1, 80);
                    if (optimizerOptions.BestIndicatorPair.ShortIndicator != null && optimizerOptions.BestIndicatorPair.LongIndicator != null)
                    {
                        decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                        StrategyGeneric strategy = new StrategyGeneric(TraderLib.emulationConnection, securityInfo, optimizerOptions, optimizerOptions.BestIndicatorPair.LoseLimit);

                        return strategy;
                    }
                    #endregion
                }
                catch (System.NullReferenceException e)
                {
                    Console.WriteLine("NullReferenceException - No Strategy Found - " + securityInfo);
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.Data.ToString());
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
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.Data.ToString());
                }
                catch (Exception ei)
                {
                    Debug.WriteLine(ei.ToString());
                    Debug.WriteLine(ei.Message);
                    Debug.WriteLine(ei.Data.ToString());
                }
                finally { RaceCondition = false; }
            }

            //If not suatable
            return null;
        }

        public static void SimulateStrategies()
        {
            ImportAndExport.PartialPath = @"C:\StockHistory\Testing\";
            string storagePath = ImportAndExport.GetFullPath(TickPeriod.Daily);

            Dictionary<string, StrategyGeneric> newStrategies = new Dictionary<string, StrategyGeneric>();
            ImportAndExport.CollectData(TickPeriod.Daily, ImportAndExport.GetAllSymbols(), false, true);
            int nrOfTestValues = 90;
            int testMoney = 100000;
            int orderLimit = testMoney / 10;

            try
            {
                // #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
                //var startTime = DateTime.Now;

                #region LOAD INDEVIDUAL CANDLES - Distibuted Load - Memory Efficient
                List<string> failedSecurities = new List<string>();
                IList<string> securityIDs = LoaderService.GetSecurityIDs(storagePath);

                List<int> securitiesLength = new List<int>();
                int nrOfSecurities = 0;
                int maxCandles = 0;

                foreach (string securityID in securityIDs)
                {
                    try
                    {
                        ImportAndExport.CollectChoosenData(securityID, TickPeriod.Daily, false);
                        int count = LoaderService.CountCandleLines(storagePath, securityID);

                        if (maxCandles < count)
                        {
                            maxCandles = count;
                        }

                        nrOfSecurities += 1;
                    }
                    catch { }
                }

                int minNumberOfSecurities = securityIDs.Count * 100 / 70 / 100;
                
                //Round Down
                int maxNrIterations = maxCandles / nrOfTestValues;
                int lessThanMaxIterations = -1;
                int iterations = maxNrIterations;

                //Min Nr Of Iterations
                while (true && iterations >= 0)
                {
                    int count = 0;
                    lessThanMaxIterations += 1;
                    iterations = maxNrIterations - lessThanMaxIterations;

                    foreach (int length in securitiesLength)
                    {
                        if ((length / nrOfTestValues) >= iterations)
                        {
                            count += 1;
                        }
                    }

                    if (count >= minNumberOfSecurities)
                    {
                        break;
                    }
                }

                //First Round
                bool isFirst = true;

                //Run Simulation
                foreach (string securityID in securityIDs)
                {
                    SecurityInfo securityInfo = null;
                    bool isCandlesValied = false;
                    try
                    {
                        securityInfo = LoaderService.ConvertCsvToCandles(TimeSpan.FromDays(1), storagePath, securityID);
                        isCandlesValied = true;
                    }
                    catch { }

                    if (isCandlesValied && securityInfo != null && securityInfo.Candles.Count > (64 + nrOfTestValues * iterations))
                    {
                        List<Candle> initialCandles = new List<Candle>();
                        int initialStart = (64 + nrOfTestValues * iterations) - 1;

                        for (int i = 0; i < 64; i++)
                        {
                            initialCandles.Add(securityInfo.Candles[initialStart - i]);
                        }

                        List<IndicatorPair> indicatorPairs = Optimizer.CreateIndicatorPairs(initialCandles, Optimizer._TestPairs);

                        int iterNr = 1;
                        for (int i = iterations; i > 0; i--)
                        {
                            List<Candle> candles = new List<Candle>();
                            int startIndex = (nrOfTestValues * iterations) - 1;

                            string connectionString = @"Data Source=localhost;Initial Catalog=StockHistDB;Integrated Security=True;";
                            SqlConnection connection = new SqlConnection(connectionString);

                            if (isFirst)
                            {
                                string insert = "INSERT INTO[dbo].[InOrder]([Nr],[CloseDate],[NrOfTests]) VALUES " +
                                    $"({iterNr}" +
                                    $",'{securityInfo.Candles[startIndex].CloseTime.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                                    $",{nrOfTestValues})";

                                connection.Open();
                                SqlCommand command = new SqlCommand(insert, connection);
                                command.ExecuteNonQuery();
                                connection.Close();
                            }

                            for (int j = 0; j < nrOfTestValues; j++)
                            {
                                initialCandles.Add(securityInfo.Candles[startIndex - j]);
                            }

                            bool isSellEnabled = false;
                            bool isBuyEnabled = true;
                            decimal loseLimit = -1m;

                            Optimizer.SimulateIndicatorPairs(ref indicatorPairs, initialCandles, isSellEnabled, isBuyEnabled, loseLimit);

                            foreach (IndicatorPair indicatorPair in indicatorPairs)
                            {
                                string insert = "INSERT INTO [dbo].[CombinationResult] ([SecurityId],[Nr],[ShortIndicator],[LongIndicator],[LoseLimitMin],[Orders],[PositiveOrderPct],[LastResult],[ClosePrice]) VALUES " +
                                    $"('{securityID}'" +
                                    $",{iterNr}" +
                                    $",{indicatorPair.ShortIndicator.ToString()}" +
                                    $",{indicatorPair.LongIndicator.ToString()}" +
                                    $",{indicatorPair.LoseLimitMin}" +
                                    $",{indicatorPair.OrdersCount}" +
                                    $",{indicatorPair.StrategyBasic.AllPositiveOrdersPct()}" +
                                    $",{indicatorPair.LastResult}" +
                                    $",{securityInfo.Candles[startIndex].ClosePrice})";

                                connection.Open();
                                SqlCommand command = new SqlCommand(insert, connection);
                                command.ExecuteNonQuery();
                                connection.Close();

                                indicatorPair.Reset();
                                iterNr += 1;
                            }
                        }
                    }

                    isFirst = false;
                }
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
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.Data.ToString()); ;
                }
                catch (Exception ei)
                {
                    Debug.WriteLine(ei.ToString());
                    Debug.WriteLine(ei.Message);
                    Debug.WriteLine(ei.Data.ToString());
                }
                finally { RaceCondition = false; }
            }

            finally { }
        }
    }
}