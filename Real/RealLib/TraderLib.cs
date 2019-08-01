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
        //Fx. public static string connectionString = @"Data Source=192.168.x.xxx;Initial Catalog=StockHistDB; User ID=myUsername;Password=myPassword;";
        //Fx. public static string connectionString = @"Data Source=localhost;Initial Catalog=StockHistDB;Integrated Security=True;";
        public static string connectionString = Environment.GetEnvironmentVariable("MSSQL_Connection_String", EnvironmentVariableTarget.Machine);
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
                    //try 
                    {
                        string partialPath = @"C:\StockHistory\Real";
                        Directory.CreateDirectory(partialPath);
                        _TradingLogWriter = new StreamWriter($"{partialPath}\\TradingLog", true);
                    }
                    /* catch (Exception e)
                     {
                         Debug.WriteLine(e.ToString());
                         Debug.WriteLine(e.Message);
                         Debug.WriteLine(e.Data.ToString());
                     }
                     */
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
                DateTime date = DateTime.ParseExact(line.Substring(0, 16), "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);
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

            List<string> dirtyResultList = new List<string>();
            List<string> temp = new List<string>();

            foreach (string s in noDuplicates)
            {
                if (s.Contains(" - Duplicate"))
                {
                    temp.Add(s);
                }
                else
                {
                    dirtyResultList.Add(s);
                }
            }

            foreach (string s in temp)
            {
                dirtyResultList.Add(s);
            }

            File.WriteAllLines($"{partialPath}\\TradingLog", dirtyResultList);
            //File.WriteAllLines($"{partialPath}\\TradingLog", noDuplicates);
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
            //TODO
            //bool isDownloadEnabled = false;
            bool isDownloadEnabled = true;

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

                    ImportAndExport.CollectData(tickPeriod, symbols, true, true);
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

                SecurityInfo securityInfo = CollectorLib.GetSecurityInfo(TickPeriod.Daily, symbol);

                if (securityInfo.Candles != null)
                {
                    List<Candle> candles = securityInfo.Candles;


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
                else
                {
                    Console.WriteLine("Unable To load Candles See InvokeTrading In TraderLib.");
                    Console.WriteLine("Problematic Symbol: " + symbol);
                    Console.WriteLine("Press Enter To Continue");
                    Console.ReadLine();
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
                    TradingLogWriter.WriteLine($"{lastCandle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {symbol} - Cancel Orders (Order Type: Buy)");
                }

                if (expiredDirection == Sides.Sell)
                {
                    TradingLogWriter.WriteLine($"{lastCandle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {symbol} - Cancel Orders (Order Type: Sell)");
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

                decimal pctProfit = strategy.ConnectionSecurityIDProfit() / strategy.Connection.CalcPayment();
                string pctProfitString = pctProfit.ToString().Length >= 6 ? pctProfit.ToString().Substring(0, 6) : pctProfit.ToString();

                //No Change
                if (!previousDisabled && !currentDisabled && previousDirection == currentDirection && strategy.IsStrategyExpiring == false)
                {
                    TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - (No Change) ID: {strategy.SecurityID}  - LoseLimit:  {strategy.LoseLimitConstant} - Profit: {pctProfitString}");
                }

                //Create New Order And Cancel Old
                if (!previousDisabled && !currentDisabled && previousDirection != currentDirection && strategy.IsStrategyExpiring == false)
                {
                    if (currentDirection == Sides.Buy)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {strategy.SecurityID} - Order Type: Buy - Current Price: {candle.ClosePrice}  - LoseLimit:  {strategy.LoseLimitConstant} - Profit: {pctProfitString}");
                    }

                    if (currentDirection == Sides.Sell)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {strategy.SecurityID} - Order Type: Sell - Current Price: {candle.ClosePrice}  - LoseLimit:  {strategy.LoseLimitConstant} - Profit: {pctProfitString}");
                    }

                    TradingLogWriter.Flush();
                }

                //Cancel Current Orders
                if ((!previousDisabled && currentDisabled) || (strategy.IsStrategyExpiring && previousDirection != currentDirection))
                {
                    if (previousDirection == Sides.Buy)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {strategy.SecurityID} - Cancel Orders (Order Type: Buy) - LoseLimit:  {strategy.LoseLimitConstant} - Profit: {pctProfitString}");
                    }

                    if (previousDirection == Sides.Sell)
                    {
                        TradingLogWriter.WriteLine($"{candle.CloseTime.ToString("yyyy-MM-dd hh:mm")} - ID: {strategy.SecurityID} - Cancel Orders (Order Type: Sell)  - LoseLimit:  {strategy.LoseLimitConstant} - Profit: {pctProfitString}");
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
                TradingLogWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm")} - ID: {strategy.SecurityID} - Candles Invalied - Cancel Orders If Connection Valied");
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
            //OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
            List<OptimizerOptions> optimizerOptionsList = OptimizerOptions.GetInstances(TickPeriod.Daily);
            //int nrOfTestValues = 90;
            int testMoney = 10000;
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
                        Console.WriteLine(securityID);
                    }
                    catch { }

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
                                    StrategyGeneric strategy = FindStrategy(securityInfo, optimizer, optimizerOptionsList);


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
        private static StrategyGeneric FindStrategy(SecurityInfo securityInfo, Optimizer optimizer, List<OptimizerOptions> optimizerOptionsList)
        {
            foreach (OptimizerOptions optimizerOptionsInList in optimizerOptionsList)
            {
                OptimizerOptions optimizerOptions = optimizerOptionsInList;

                try
                {
                    #region Set Candles
                    //Get Test Candles
                    List<Candle> candles = securityInfo.Candles.ToList();
                    int keyCount = securityInfo.Candles.Count;
                    int beginIndexTest = keyCount - (optimizer.RecursiveTests * optimizerOptions.NrOfTestValues + optimizer.GetMaxIndicatorLength(optimizerOptions.EnabledPairs));
                    int testCount = optimizer.GetMaxIndicatorLength(optimizerOptions.EnabledPairs) + optimizer.RecursiveTests * optimizerOptions.NrOfTestValues;


                    List<Candle> testCandles = candles.GetRange(beginIndexTest, testCount);
                    #endregion


                    try
                    {
                        #region Find Strategy / Best Indicator Pairs + Its options

                        optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, 1);
                        #endregion

                        #region Create The Strategy
                        //EmulationConnection emulationConnection = new EmulationConnection(testMoney, OrderLimitType.Value, orderLimit, 1, 80);
                        if (optimizerOptions.BestIndicatorPair != null)
                        {
                            if (optimizerOptions.BestIndicatorPair.ShortIndicator != null && optimizerOptions.BestIndicatorPair.LongIndicator != null)
                            {
                                decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                                StrategyGeneric strategy = new StrategyGeneric(TraderLib.emulationConnection, securityInfo, optimizerOptions, optimizerOptions.BestIndicatorPair.LoseLimit);

                                return strategy;
                            }
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
            }

            //If not suatable
            return null;
        }

        public static void SimulateStrategies(TickPeriod tickPeriod, string subPath)
        {
            string storagePath = ImportAndExport.GetFullPath(tickPeriod);
            Dictionary<string, StrategyGeneric> newStrategies = new Dictionary<string, StrategyGeneric>();
            //importAndExport.CollectData(TickPeriod.Daily, ImportAndExport.GetAllSymbols(), false, true);
            int nrOfTestValues = 90;
            int testMoney = 100000;
            int orderLimit = testMoney / 10;


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
                    //ImportAndExport.CollectChoosenData(securityID, TickPeriod.Daily, false);
                    int count = LoaderService.CountCandleLines(storagePath, securityID);

                    if (maxCandles < count)
                    {
                        maxCandles = count;
                    }
                    securitiesLength.Add(count);
                    nrOfSecurities += 1;
                }
                catch { }

                #region Try Inserting SecurityId           
                SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    string insertSecurity = "INSERT INTO [dbo].[Security] ([SecurityId]) VALUES " +
                        $"('{securityID}')";

                    connection.Open();
                    SqlCommand commandInOrder = new SqlCommand(insertSecurity, connection);
                    commandInOrder.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Tried adding symbol: " + securityID + " to DB");
                }
                #endregion
            }

            int minNumberOfSecurities = securityIDs.Count * 70 / 100;

            //Round Down
            int maxNrIterations = maxCandles / nrOfTestValues;

            int iterations = maxNrIterations;
            int lessThanMaxIterations = -1;

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

            securityIDs.Clear();

            SimulateStrategies(tickPeriod, nrOfTestValues, maxNrIterations);
        }

        public static void SimulateStrategies(TickPeriod tickPeriod, int nrOfTestValues, int iterations)
        {
            string storagePath = ImportAndExport.GetFullPath(tickPeriod);
            IList<string> securityIDs = LoaderService.GetSecurityIDs(storagePath);

            // Create or Append File
            Dictionary<string, List<string>> dictSecurityIDs = new Dictionary<string, List<string>>();

            int maxJobs = 16;
            for(int i = 0; i < maxJobs; i++)
            {
                string insertPath = System.IO.Directory.GetParent(storagePath) + @"\Inserts" + i + ".sql";
                dictSecurityIDs[insertPath] = new List<string>();

                try
                {
                    System.IO.File.Delete(insertPath);
                }
                catch { }
            }

            foreach (string securityID in securityIDs)
            {
                foreach (string key in dictSecurityIDs.Keys)
                {
                    dictSecurityIDs[key].Add(securityID);
                }
            }

            //Run Simulation
            for(int index = 0; index < dictSecurityIDs[dictSecurityIDs.Keys.First()].Count; index++)
            {
                Task[] tasks = new Task[maxJobs];
                int i = 0;
                foreach (string path in dictSecurityIDs.Keys)
                {
                    if (index < dictSecurityIDs[path].Count)
                    {
                        Task t = Task.Run(() => SimulateStrategy(dictSecurityIDs[path][index], path, tickPeriod, nrOfTestValues, iterations));
                        tasks[i] = t;
                        i += 1;
                    }
                }

                Task.WaitAll(tasks);
            }
            #endregion

            /*
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
            */

            Console.WriteLine("Completed. Press Enter...");
            Console.ReadLine();
        }

        public static void SimulateStrategy(string securityID, string streamPath, TickPeriod tickPeriod, int nrOfTestValues, int iterations)
        {

            string storagePath = ImportAndExport.GetFullPath(tickPeriod);
            StreamWriter stream = new StreamWriter(streamPath, true);

            try
            {
                

                //string insert = "";
                SecurityInfo securityInfo = null;
                bool isCandlesValied = false;
                try
                {
                    switch (tickPeriod)
                    {
                        case TickPeriod.Daily:
                            if (tickPeriod == TickPeriod.Daily)
                            {
                                securityInfo = LoaderService.ConvertCsvToCandles(TimeSpan.FromDays(1), storagePath, securityID);
                                isCandlesValied = true;
                            }
                            break;
                        default:
                            throw new InvalidDataException();
                    }
                }
                catch { }

                if (isCandlesValied && securityInfo != null && securityInfo.Candles.Count > (64 + nrOfTestValues * iterations))
                {
                    int iterNr = 1;
                    for (int i = iterations; i > 0; i--)
                    {
                        List<Candle> initialCandles = new List<Candle>();
                        int initialStart = securityInfo.Candles.Count - (64 + nrOfTestValues * i);

                        for (int j = 0; j < 64; j++)
                        {
                            initialCandles.Add(securityInfo.Candles[initialStart + j]);
                        }

                        List<IndicatorPair> indicatorPairs = Optimizer.CreateIndicatorPairs(initialCandles, Optimizer._TestPairs);

                        List<Candle> candles = new List<Candle>();
                        int startIndex = securityInfo.Candles.Count - (nrOfTestValues * i);

                        //SqlConnection connection = new SqlConnection(connectionString);

                        /* Doesn't Work 2. Time
                        if (isFirst && false)
                        {
                            try
                            {
                                string insertInOrder = "INSERT INTO[dbo].[InOrder]([Nr],[CloseDate],[NrOfTests]) VALUES " +
                                    $"({iterNr}" +
                                    $",'{securityInfo.Candles[startIndex].CloseTime.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                                    $",{nrOfTestValues})";

                                connection.Open();
                                SqlCommand commandInOrder = new SqlCommand(insertInOrder, connection);
                                commandInOrder.ExecuteNonQuery();
                                connection.Close();
                            }
                            catch (SqlException e)
                            {

                            }
                        }
                        */

                        List<Candle> testCandles = new List<Candle>();
                        for (int j = 0; j < nrOfTestValues; j++)
                        {
                            testCandles.Add(securityInfo.Candles[startIndex + j]);
                        }

                        bool isSellEnabled = false;
                        bool isBuyEnabled = true;
                        decimal loseLimit = -1m;

                        stream.WriteLine("INSERT INTO [dbo].[CombinationResult] ([SecurityId],[Nr],[ShortIndicator],[LongIndicator],[LoseLimitMin],[Orders],[PositiveOrderPct],[LastResult],[ClosePrice]) VALUES ");
                        //insert += "INSERT INTO [dbo].[CombinationResult] ([SecurityId],[Nr],[ShortIndicator],[LongIndicator],[LoseLimitMin],[Orders],[PositiveOrderPct],[LastResult],[ClosePrice]) VALUES ";
                        int count = 0;
                        for (int j = 0; j < indicatorPairs.Count; j++)
                        {
                            indicatorPairs[j] = Optimizer.SimulateIndicatorPair(indicatorPairs[j], testCandles, isSellEnabled, isBuyEnabled, loseLimit);

                            stream.WriteLine($"('{securityID}'" +
                                $",{iterNr}" +
                                $",'{indicatorPairs[j].ShortIndicator.ToString()}'" +
                                $",'{indicatorPairs[j].LongIndicator.ToString()}'" +
                                $",{indicatorPairs[j].LoseLimitMin}" +
                                $",{indicatorPairs[j].OrdersCount}" +
                                $",{indicatorPairs[j].StrategyBasic.AllPositiveOrdersPct()}" +
                                $",{indicatorPairs[j].LastResult}" +
                                $",{securityInfo.Candles[startIndex].ClosePrice})");
                            /*
                            insert += $"('{securityID}'" +
                                $",{iterNr}" +
                                $",'{indicatorPairs[j].ShortIndicator.ToString()}'" +
                                $",'{indicatorPairs[j].LongIndicator.ToString()}'" +
                                $",{indicatorPairs[j].LoseLimitMin}" +
                                $",{indicatorPairs[j].OrdersCount}" +
                                $",{indicatorPairs[j].StrategyBasic.AllPositiveOrdersPct()}" +
                                $",{indicatorPairs[j].LastResult}" +
                                $",{securityInfo.Candles[startIndex].ClosePrice})";
                                */
                            indicatorPairs[j].Reset();
                            indicatorPairs[j].ShortIndicator.Indicator.Container.ClearValues();
                            indicatorPairs[j].LongIndicator.Indicator.Container.ClearValues();
                            stream.Flush();

                            count += 1;
                            if (count >= 1000)
                            {
                                stream.Write(";");
                                /*
                                try
                                {
                                */
                                //insert += ";" + Environment.NewLine + Environment.NewLine;
                                /*
                                connection.Open();
                                SqlCommand command = new SqlCommand(insert, connection);
                                command.ExecuteNonQuery();
                                connection.Close();
                                */
                                count = 0;
                                /*
                                stream.WriteLine(insert);
                                stream.WriteLine();
                                stream.Flush();
                                insert = "";
                                */
                                if (!((j + 1) >= indicatorPairs.Count))
                                {
                                    stream.WriteLine("INSERT INTO [dbo].[CombinationResult] ([SecurityId],[Nr],[ShortIndicator],[LongIndicator],[LoseLimitMin],[Orders],[PositiveOrderPct],[LastResult],[ClosePrice]) VALUES ");
                                    stream.Flush();
                                    //insert += "INSERT INTO [dbo].[CombinationResult] ([SecurityId],[Nr],[ShortIndicator],[LongIndicator],[LoseLimitMin],[Orders],[PositiveOrderPct],[LastResult],[ClosePrice]) VALUES ";
                                }
                                /*
                            }
                            catch (Exception e)
                            {
                                //File.WriteAllText(@"C:\StockHistory\insert.txt", insert);
                                throw e;
                            }
                            */
                            }
                            else
                            {
                                if (!((j + 1) >= indicatorPairs.Count))
                                {
                                    stream.Write(",");
                                    //insert += $",";
                                }
                            }
                        }
                        if (count > 0)
                        {
                            /*
                            try
                            {
                            */
                            //insert = insert.TrimEnd(',');
                            stream.Write(";");
                            stream.WriteLine("");
                            //insert += ";" + Environment.NewLine + Environment.NewLine;
                            /*
                            connection.Open();
                            SqlCommand command = new SqlCommand(insert, connection);
                            command.ExecuteNonQuery();
                            connection.Close();


                            stream.WriteLine(insert);
                            stream.WriteLine();
                            stream.Flush();
                            insert = "";
                            */
                            stream.Flush();
                            iterNr += 1;
                            /*
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        */
                        }

                        //stream.WriteLine(insert);
                        //stream.WriteLine();
                        stream.Flush();
                    }
                }
            }
            finally
            {
                stream.Close();
            }
            /*
             stream.WriteLine(insert);
             stream.WriteLine();
             stream.Flush();
             * */

            Console.WriteLine(securityID + " - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            FileInfo fi1 = new FileInfo(storagePath + '\\' + securityID + ".csv");
            fi1.Delete();
        }
    }
}