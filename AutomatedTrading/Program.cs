using CsvHelper;
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
using static Stocks.Services;

namespace StockSolution
{
    class Program
    {
        private static StreamWriter ErrorWriter = null;
        private static bool RaceCondition = false;
        private readonly static bool TestCandleDates = false;

        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(254,254);
            ThreadPool.SetMinThreads(64,64);
            
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
            

            //NEEDS TO BE ABLE TO CREATE PATH OR PUT IN DATABASE (HIDDEN ERRROR)
            //Should Be In A Sepperate Program
            //ImportAndExport.CollectData(TickPeriod.Daily);

            bool completed = false;
            int count = 1;

            while (!completed)
            {
                try
                {
                    ErrorWriter = new StreamWriter(@"C:\StockHistory\Optimizer\ErrorLog" + count + ".txt");
                    completed = true;
                }
                catch
                {
                    count += 1;
                }
            }

            //Parallel.For(0, 15, prlNumber =>
            {
                //Should Amount To A Little More Than Two Years (Only Weekend Removed)
                int minNrOfTestValues = 766;

                //?;
                //int minNrOfTestValues = 514;

                //Once;
                //int minNrOfTestValues = 274;

                //Test Values - Min Orders: 15 - Positive Order Pct: 75 - Min Profit Pct: 55 - LLC: -0.12
                int minOrders = 15;
                int positiveOrderPct = 75;
                int minProfitPct = 55;
                decimal loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 70 - Min Profit Pct: 55 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 70;
                minProfitPct = 55;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 70 - Min Profit Pct: 60 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 70;
                minProfitPct = 60;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 65 - Min Profit Pct: 60 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 65;
                minProfitPct = 60;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 75 - Min Profit Pct: 40 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 75;
                minProfitPct = 40;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 75 - Min Profit Pct: 45 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 75;
                minProfitPct = 45;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 75 - Min Profit Pct: 50 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 75;
                minProfitPct = 50;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 70 - Min Profit Pct: 50 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 70;
                minProfitPct = 50;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 65 - Min Profit Pct: 60 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 65;
                minProfitPct = 60;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 65 - Min Profit Pct: 55 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 65;
                minProfitPct = 55;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 70 - Min Profit Pct: 50 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 70;
                minProfitPct = 50;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 20 - Positive Order Pct: 75 - Min Profit Pct: 35 - LLC: -0.12
                minOrders = 20;
                positiveOrderPct = 75;
                minProfitPct = 35;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 75 - Min Profit Pct: 45 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 75;
                minProfitPct = 45;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //Test Values - Min Orders: 15 - Positive Order Pct: 65 - Min Profit Pct: 55 - LLC: -0.12
                minOrders = 15;
                positiveOrderPct = 65;
                minProfitPct = 55;
                loseLimitConstant = -0.12m;
                TestSelectedValuesAllData(minNrOfTestValues, minOrders, positiveOrderPct, minProfitPct, loseLimitConstant);

                //SingleTest();
                //BIGTest();
            }
            //);


            ErrorWriter.FlushAsync().Wait();
            ErrorWriter.Close();
        }

        private static bool IsSkipped(Dictionary<SecurityInfo, decimal[]> skipSecurityIDs, SecurityInfo securityId, decimal loseLimitConstant, int minOrders, int positiveOrderPct, int minProfitPct)
        {
            bool isSkipped = false;

            if (skipSecurityIDs.ContainsKey(securityId) && loseLimitConstant >= skipSecurityIDs[securityId][0] && minOrders >= skipSecurityIDs[securityId][1] && positiveOrderPct >= skipSecurityIDs[securityId][2] && minProfitPct >= skipSecurityIDs[securityId][3])
            {
                isSkipped = true;
            }

            return isSkipped;
        }

        private static T GetDefault<T>()
        {
            return default(T);
        }

        //Simulate Real Trades Using Specificed Strategy
        public static void TestSelectedValuesAllData(int minNrOfTestValues, int minOrders, int positiveOrderPct, int minProfitPct, decimal loseLimitConstant)
        {
            int nrOfTestValues = 90;
            int realValues = 30;
            int initialMoney = 100000;
            int orderLimit = initialMoney / 10;
            DateTimeOffset dateMayNotBeOlderThan = DateTimeOffset.UtcNow.AddDays(-(minNrOfTestValues * 1.7));

            try
            {
                StreamWriter streamWriter = null;
                CsvWriter csvWriter = null;
                string path = @"C:\StockHistory\Optimizer\Result_" + $"Min Orders {minOrders}_Positive Order Pct {positiveOrderPct}_Min Profit Pct {minProfitPct}_Lose Limit Constant {loseLimitConstant}" + ".csv";
                if (File.Exists(path))
                {
                    throw new IOException("File Allready Exist");
                }
                streamWriter = new StreamWriter(path);
                csvWriter = new CsvWriter(streamWriter);
                RaceCondition = false;

                //Init candlesDictionary, minRows And maxRows
                IList<SecurityInfo> securityInfos = null;

                #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
                var startTime = DateTime.Now;
                System.Console.WriteLine(startTime);
                string fullPath = ImportAndExport.GetFullPath(TickPeriod.Daily);
                securityInfos = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, new DateTime(2000, 1, 1), new DateTime(2019, 1, 1));

                //Remove Candle Values If To Few
                for (int i = 0; i < securityInfos.Count; i++)
                {
                    if (securityInfos[i].Candles.Count < minNrOfTestValues)
                    {
                        securityInfos.RemoveAt(i);
                        i -= 1;
                    }
                    else
                    {
                        int firstTestIndex = securityInfos[i].Candles.Count - minNrOfTestValues;
                        DateTimeOffset startDateTest = securityInfos[i].Candles[firstTestIndex].CloseTime;
                        if (startDateTest.CompareTo(dateMayNotBeOlderThan) < 0)
                        {
                            securityInfos.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }
                #endregion

                if (securityInfos.Count != 0)
                {
                    #region Normalize Candle Size

                    for (int i = 0; i < securityInfos.Count; i++)
                    {
                        List<Candle> candles = new List<Candle>();

                        for (int j = securityInfos[i].Candles.Count - minNrOfTestValues; j < securityInfos[i].Candles.Count; j++)
                        {
                            candles.Add(securityInfos[i].Candles[j]);
                        }

                        securityInfos[i].Candles = candles;
                    }
                    #endregion


                    Optimizer optimizer = new Optimizer();
                    List<decimal> emulationConnections = new List<decimal>();

                    #region CSV Headers
                    //StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\Optimizer\Test Result MP_PCT " + minProfitPct + " - M_Order " + minOrders + " - P_OrderPct " + positiveOrderPct + ".csv");

                    //CsvWriter csvWriter = new CsvWriter(streamWriter);
                    csvWriter.WriteField("Avg Increase Pr. Security ID");
                    csvWriter.WriteField("Total");
                    csvWriter.WriteField("Securities");
                    csvWriter.WriteField("Start DateTime");
                    csvWriter.WriteField("End DateTime");
                    csvWriter.NextRecord();
                    #endregion

                    #region Optimizer Options - Temp Calcs
                    int indicatorLength = OptimizerOptions.GetIndicatorLength(TickPeriod.Daily).Max;
                    int nrOfTests = (minNrOfTestValues - (indicatorLength + (optimizer.RecursiveTests * nrOfTestValues))) / realValues;
                    #endregion

                    for (int currentTestNr = 0; currentTestNr < nrOfTests; currentTestNr++)
                    {
                        TestSelectedValuesOnce(ref streamWriter, ref csvWriter,
                            securityInfos, optimizer, minOrders,
                            positiveOrderPct, minProfitPct, loseLimitConstant,
                            indicatorLength, realValues, nrOfTestValues, nrOfTests,
                            currentTestNr, orderLimit, initialMoney, TestCandleDates);
                    }
                }
                else
                {
                    csvWriter.WriteField("No Enough Candles");
                    if (TestCandleDates)
                    {
                        csvWriter.WriteField("THIS IS A CANDLE TEST - DISABLE TestCandlesDates");
                    }
                    csvWriter.Flush();
                    streamWriter.Flush();
                }

                streamWriter.Close();
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
        }

        // TODO TEST - private static void TestSelectedValuesOnce(ref StreamWriter streamWriter, ref CsvWriter csvWriter, ref IDictionary<SecurityInfo, IList<Candle>> candlesDictionary, ref Optimizer optimizer, int minOrders, int positiveOrderPct, int minProfitPct, decimal loseLimitConstant, int indicatorLength, int realValues, int nrOfTestValues, int nrOfTests, int currentTestNr, int orderLimit, int initialMoney, bool testCandleDates)
        private static void TestSelectedValuesOnce(ref StreamWriter streamWriter, ref CsvWriter csvWriter, IList<SecurityInfo> securityInfoes, Optimizer optimizer, int minOrders, int positiveOrderPct, int minProfitPct, decimal loseLimitConstant, int indicatorLength, int realValues, int nrOfTestValues, int nrOfTests, int currentTestNr, int orderLimit, int initialMoney, bool testCandleDates)
        {
            int successfulSecurityIDs = 0;
            decimal profitPctTotal = 0m;
            DateTimeOffset? start = DateTimeOffset.MaxValue;
            DateTimeOffset? end = DateTimeOffset.MinValue;

            //Calc Average
            //Parallel.ForEach(candlesDictionary.Keys, new ParallelOptions { MaxDegreeOfParallelism = 4 }, securityID =>
            //Parallel.ForEach(securityInfoes, new ParallelOptions { MaxDegreeOfParallelism = 32 }, securityID =>
            //foreach (SecurityInfo securityID in candlesDictionary.Keys)
            
            //HIGH RAM USAGE !!!
            //Parallel.For(0, securityInfoes.Count, new ParallelOptions { MaxDegreeOfParallelism = 3 }, index =>
            for(int index = 0; index < securityInfoes.Count; index++)
            {
                try
                {
                    #region Optimizer Options
                    OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
                    optimizerOptions.MinProfitPct = minProfitPct;
                    optimizerOptions.MinOrders = minOrders;
                    optimizerOptions.PositiveOrderPct = positiveOrderPct;
                    optimizerOptions.LoseLimitConstant = loseLimitConstant;
                    #endregion


                    #region Set Candles
                    //Get Test Candles
                    List<Candle> candles = securityInfoes[index].Candles.ToList();
                    int keyCount = securityInfoes[index].Candles.Count;
                    int beginIndexTest = keyCount - (optimizer.RecursiveTests * nrOfTestValues + realValues * (nrOfTests - currentTestNr) + indicatorLength);
                    int testCount = indicatorLength + optimizer.RecursiveTests * nrOfTestValues;
                    int beginIndexReal = beginIndexTest + testCount;

                    List<Candle> testCandles = candles.GetRange(beginIndexTest, testCount);
                    List<Candle> realCandles = candles.GetRange(beginIndexReal, realValues);

                    //if (start != null && end != null) { }
                    if ((start < realCandles[0].CloseTime && end > realCandles[realCandles.Count - 1].CloseTime)) { }
                    else
                    {
                        start = realCandles[0].CloseTime;
                        end = realCandles[realCandles.Count - 1].CloseTime;
                    }
                    #endregion

                    try
                    {
                        if (!testCandleDates)
                        {
                            #region Find Strategy

                            optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, nrOfTestValues, 1);
                            #endregion

                            #region Simulate Real Values
                            EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, 80);
                            decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                            StrategyBasic strategy = new StrategyGeneric(emulationConnection, securityInfoes[index], optimizerOptions);
                            strategy.Start();
                            //Candle Simulation Still Fake
                            for (int i = 0; i < realCandles.Count; i++)
                            {
                                strategy.ProcessCandle(realCandles[i]);
                            }
                            strategy.Stop();
                            #endregion

                            while (RaceCondition) { Thread.Sleep(5); }
                            RaceCondition = true;

                            //Calc Result
                            decimal profit = emulationConnection.GetTotalValue() - initialMoney;
                            profitPctTotal += (profit / orderLimit * 100);
                            successfulSecurityIDs += 1;
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine("NullReferenceException - No Strategy Found - " + securityInfoes[index]);
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
            }
            //);


            if (successfulSecurityIDs == 0)
            {
                csvWriter.WriteField("No Security IDs");
            }
            else
            {
                csvWriter.WriteField(profitPctTotal / successfulSecurityIDs);
            }


            csvWriter.WriteField(profitPctTotal);
            csvWriter.WriteField(successfulSecurityIDs);
            csvWriter.WriteField(((DateTimeOffset)start).ToString("yyyyy-MM-dd hh:mm", new CultureInfo("da-DK")));
            csvWriter.WriteField(((DateTimeOffset)end).ToString("yyyyy-MM-dd hh:mm", new CultureInfo("da-DK")));
            csvWriter.NextRecord();
            csvWriter.Flush();
            streamWriter.Flush();
        }

        private static void SingleTest()
        {
            //Min positive pct of strategies
            //Positive Values - use min value
            //Balance between positive and negative orders (at least 40% positive orders)
            //Makes sure strategy still is good
            //FLYT loseLimitConstant Indtil Min Orders er Nået og 40% Værdier, med mere er nået samt min positivt resultat, evt fortsæt for at finde bedste match
            //

            //Øvre grænse på ordre ligesom minordre
            //
            //Find bedste værdier til at vælge strategier - Ligesom test blot for hver enkelt aktie
            //Lav en findeling af strategier, optimer på den enkelte kurve frem for en mængde ad gangen og test candles efterfølgende og mængden efterfølgende
            //Optimer til bedste værdier og udvælg strategi efterfølgende

            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);

            #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
            var startTime = DateTime.Now;
            System.Console.WriteLine(startTime);

            IList<SecurityInfo> SecurityInfoes = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), @"C:\StockHistory\TEST", new DateTime(2000, 1, 1), new DateTime(2019, 1, 1));
            var endTime = DateTime.Now;
            int rows = 0;
            foreach (SecurityInfo securityInfo in SecurityInfoes)
            {
                rows += securityInfo.Candles.Count;
            }

            System.Console.WriteLine("Start: " + startTime + " - End: " + endTime);
            System.Console.WriteLine("Securities: " + SecurityInfoes.Count + " - Rows: " + rows);
            #endregion
            /*
                        StockSharpSolution.Console.ConsoleApp consoleApp = new StockSharpSolution.Console.ConsoleApp();
                        //consoleApp.Start(new DateTime(2000,1,1), DateTime.Now);

                        var values = consoleApp.Test();

                        foreach (dynamic strategy in values)
                        {
                            Console.WriteLine(strategy.PnL_Total);
                        }
            */

            //List<EmulationConnection> emulationConnections = new List<EmulationConnection>();


            StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\Optimizer\Test Result MP_PCT.csv");
            CsvWriter csvWriter = new CsvWriter(streamWriter);

            int realValues = 90;
            int initialMoney = 100000;
            int orderLimit = initialMoney / 10;

            Dictionary<SecurityInfo, decimal[]> skipSecurityIDs = new Dictionary<SecurityInfo, decimal[]>();

            decimal loseLimitConstant = -0.12m;
            int minOrders = 20;
            int positiveOrderPct = 75;
            int minProfitPct = 40;


            Optimizer optimizer = new Optimizer();
            List<decimal> emulationConnections = new List<decimal>();

            #region CSV Headers
            //StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\Optimizer\Test Result MP_PCT " + minProfitPct + " - M_Order " + minOrders + " - P_OrderPct " + positiveOrderPct + ".csv");

            //CsvWriter csvWriter = new CsvWriter(streamWriter);
            csvWriter.WriteField("Security ID");
            csvWriter.WriteField("Short Indicator");
            csvWriter.WriteField("Long Indicator");
            csvWriter.WriteField("Recursive Tests");
            csvWriter.WriteField("Is Sell Enabled");
            csvWriter.WriteField("Is Buy Enabled");
            csvWriter.WriteField("Min Orders");
            csvWriter.WriteField("Positive Order Pct");
            csvWriter.WriteField("Test Positive Order Pct");
            csvWriter.WriteField("Min Profit Pct");
            csvWriter.WriteField("Lose Limit Constant");
            csvWriter.WriteField("Test Result PCT");
            csvWriter.WriteField("Profit PCT");
            csvWriter.NextRecord();
            #endregion

            bool raceCondition = false;
            Parallel.ForEach(SecurityInfoes, securityID =>
            {
                // foreach (SecurityInfo securityID in candlesDictionary.Keys) {
                /*
                decimal loseLimitConstant = -0.105m;
                int minOrders = 10;
                int positiveOrderPct = 84;
                int minProfitPct = 20;
                */

                if (!IsSkipped(skipSecurityIDs, securityID, loseLimitConstant, minOrders, positiveOrderPct, minProfitPct))
                {
                    //Alternativ til FindBestOptimizerOptions brug overordnet sortering mere end en gang (benyt best)
                    //Lav vægtning så slut værdier betyder mere end startværdier - Brug test result som indicator på godt og skidt
                    startTime = DateTime.Now;

                    System.Console.WriteLine();

                    //int sellPositiveOrdersPct;
                    //Find Best securityIDs
                    //Split indicators One of buy one for sell
                    //Stop Ordering if lose to high (find another strategy)

                    List<Candle> candles = securityID.Candles.ToList();
                    List<Candle> testCandles = candles.GetRange(0, candles.Count - realValues);
                    List<Candle> realCandles = candles.GetRange(candles.Count - realValues - 1, realValues);

                    try
                    {
                        OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
                        optimizerOptions.MinProfitPct = minProfitPct;
                        optimizerOptions.MinOrders = minOrders;
                        optimizerOptions.PositiveOrderPct = positiveOrderPct;
                        optimizerOptions.LoseLimitConstant = loseLimitConstant;
                        optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, 90, 1);
                        EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, 80);
                        decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                        StrategyBasic strategy = new StrategyGeneric(emulationConnection, securityID, optimizerOptions);
                        strategy.Start();
                        //Candle Simulation Still Fake
                        for (int i = 0; i < realCandles.Count; i++)
                        {
                            strategy.ProcessCandle(realCandles[i]);
                        }
                        strategy.Stop();

                        /*   
                        StrategyBasic strategySell = new StrategyGeneric(emulationConnection, securityID, indicatorPairSell.LongIndicator, indicatorPairSell.ShortIndicator, marginSellPct, marginBuyPct, true, false);
                        StrategyBasic strategyBuy = new StrategyGeneric(emulationConnection, securityID, indicatorPairBuy.LongIndicator, indicatorPairBuy.ShortIndicator, marginSellPct, marginBuyPct, false, true);
                        strategySell.Start();
                        strategyBuy.Start();
                        //Candle Simulation Still Fake
                        foreach (var candle in realCandles)
                        {
                            strategySell.ProcessCandle(candle);
                            strategyBuy.ProcessCandle(candle);
                        }
                        strategySell.Stop();
                        strategyBuy.Stop();
                        */

                        decimal profit = emulationConnection.GetTotalValue() - initialMoney;

                        decimal profitPct = (profit / orderLimit * 100);
                        System.Console.WriteLine("Portfolie End Value PCT: " + profitPct.ToString() + " - " + securityID + " - " + strategy.Connection.GetPortfolio().TotalValue);
                        //System.Console.WriteLine(strategy.Connection.GetPortfolio().MaxLoseValue);

                        /*
                        var sorted = SortingAlgorithm.MergeSort(emulationConnections);


                        Console.WriteLine("Portfolie End Value: " + (sorted[0] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[1] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[2] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[3] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[4] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[5] / initialMoney * 100).ToString());

                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 5] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 4] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 3] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 2] / initialMoney * 100).ToString());
                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 1] / initialMoney * 100).ToString());

                        Console.WriteLine();
                         */

                        while (raceCondition)
                        {
                            Thread.Sleep(5);
                        }

                        raceCondition = true;
                        endTime = DateTime.Now;
                        System.Console.WriteLine("Start: " + startTime + " - End: " + endTime);
                        csvWriter.WriteField(securityID);
                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.ShortIndicator.ToString());
                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.LongIndicator.ToString());
                        csvWriter.WriteField((optimizerOptions.RecursiveTests));
                        csvWriter.WriteField(optimizerOptions.IsSellEnabled);
                        csvWriter.WriteField(optimizerOptions.IsBuyEnabled);
                        csvWriter.WriteField(optimizerOptions.MinOrders);
                        csvWriter.WriteField(optimizerOptions.PositiveOrderPct);
                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.PositiveOrderPct);
                        csvWriter.WriteField(optimizerOptions.MinProfitPct);
                        csvWriter.WriteField(optimizerOptions.LoseLimitConstant);
                        csvWriter.WriteField(lastResultPct);
                        csvWriter.WriteField(profitPct);
                        csvWriter.NextRecord();
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine("NullReferenceException - No Strategy Found - " + securityID);

                        skipSecurityIDs[securityID] = new decimal[]
                        {
                                            loseLimitConstant,
                                            minOrders,
                                            positiveOrderPct,
                                            minProfitPct
                        };
                    }

                    raceCondition = false;
                    csvWriter.Flush();
                    streamWriter.Flush();
                }
            });
            streamWriter.Close();
        }


        public static void BIGTest()
        {
            //Min positive pct of strategies
            //Positive Values - use min value
            //Balance between positive and negative orders (at least 40% positive orders)
            //Makes sure strategy still is good
            //FLYT loseLimitConstant Indtil Min Orders er Nået og 40% Værdier, med mere er nået samt min positivt resultat, evt fortsæt for at finde bedste match
            //

            //Øvre grænse på ordre ligesom minordre
            //
            //Find bedste værdier til at vælge strategier - Ligesom test blot for hver enkelt aktie
            //Lav en findeling af strategier, optimer på den enkelte kurve frem for en mængde ad gangen og test candles efterfølgende og mængden efterfølgende
            //Optimer til bedste værdier og udvælg strategi efterfølgende

            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);

            #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
            var startTime = DateTime.Now;
            System.Console.WriteLine(startTime);

            IList<SecurityInfo> securityInfoes = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), @"C:\StockHistory\TEST", new DateTime(2000, 1, 1), new DateTime(2019, 1, 1));
            var endTime = DateTime.Now;
            int rows = 0;
            foreach (SecurityInfo securityInfo in securityInfoes)
            {
                rows += securityInfo.Candles.Count;
            }

            System.Console.WriteLine("Start: " + startTime + " - End: " + endTime);
            System.Console.WriteLine("Securities: " + securityInfoes.Count + " - Rows: " + rows);
            #endregion
            /*
                        StockSharpSolution.Console.ConsoleApp consoleApp = new StockSharpSolution.Console.ConsoleApp();
                        //consoleApp.Start(new DateTime(2000,1,1), DateTime.Now);

                        var values = consoleApp.Test();

                        foreach (dynamic strategy in values)
                        {
                            Console.WriteLine(strategy.PnL_Total);
                        }
            */

            //List<EmulationConnection> emulationConnections = new List<EmulationConnection>();


            StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\Optimizer\Test Result MP_PCT.csv");
            CsvWriter csvWriter = new CsvWriter(streamWriter);

            int realValues = 90;
            int initialMoney = 100000;
            int orderLimit = initialMoney / 10;

            Dictionary<SecurityInfo, decimal[]> skipSecurityIDs = new Dictionary<SecurityInfo, decimal[]>();

            for (decimal loseLimitConstant = -0.12m; loseLimitConstant <= -0.12m; loseLimitConstant += 0.01m)
            {
                int minOrdersMax = 40;
                for (int minOrders = 10; minOrders <= minOrdersMax; minOrders += 5)
                {
                    int positiveOrderPctMax = 80;
                    for (int positiveOrderPct = 60; positiveOrderPct <= positiveOrderPctMax; positiveOrderPct += 5)
                    {
                        int minProfitPctMax = 60;
                        for (int minProfitPct = 20; minProfitPct <= minProfitPctMax; minProfitPct += 5)
                        {

                            Optimizer optimizer = new Optimizer();
                            List<decimal> emulationConnections = new List<decimal>();

                            #region CSV Headers
                            //StreamWriter streamWriter = new StreamWriter(@"C:\StockHistory\Optimizer\Test Result MP_PCT " + minProfitPct + " - M_Order " + minOrders + " - P_OrderPct " + positiveOrderPct + ".csv");

                            //CsvWriter csvWriter = new CsvWriter(streamWriter);
                            csvWriter.WriteField("Security ID");
                            csvWriter.WriteField("Short Indicator");
                            csvWriter.WriteField("Long Indicator");
                            csvWriter.WriteField("Recursive Tests");
                            csvWriter.WriteField("Is Sell Enabled");
                            csvWriter.WriteField("Is Buy Enabled");
                            csvWriter.WriteField("Min Orders");
                            csvWriter.WriteField("Positive Order Pct");
                            csvWriter.WriteField("Test Positive Order Pct");
                            csvWriter.WriteField("Min Profit Pct");
                            csvWriter.WriteField("Lose Limit Constant");
                            csvWriter.WriteField("Test Result PCT");
                            csvWriter.WriteField("Profit PCT");
                            csvWriter.NextRecord();
                            #endregion

                            bool raceCondition = false;
                            foreach (SecurityInfo securityInfo in securityInfoes)
                            {
                                /*
                                decimal loseLimitConstant = -0.105m;
                                int minOrders = 10;
                                int positiveOrderPct = 84;
                                int minProfitPct = 20;
                                */


                                if (!IsSkipped(skipSecurityIDs, securityInfo, loseLimitConstant, minOrders, positiveOrderPct, minProfitPct))
                                {

                                    //Parallel.ForEach(candlesDictionary.Keys, new ParallelOptions { MaxDegreeOfParallelism = 8 }, securityID =>{
                                    //Alternativ til FindBestOptimizerOptions brug overordnet sortering mere end en gang (benyt best)
                                    //Lav vægtning så slut værdier betyder mere end startværdier - Brug test result som indicator på godt og skidt
                                    startTime = DateTime.Now;

                                    System.Console.WriteLine();

                                    //int sellPositiveOrdersPct;
                                    //Find Best securityIDs
                                    //Split indicators One of buy one for sell
                                    //Stop Ordering if lose to high (find another strategy)

                                    List<Candle> candles = securityInfo.Candles.ToList();
                                    List<Candle> testCandles = candles.GetRange(0, candles.Count - realValues);
                                    List<Candle> realCandles = candles.GetRange(candles.Count - realValues - 1, realValues);

                                    try
                                    {
                                        OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
                                        optimizerOptions.MinProfitPct = minProfitPct;
                                        optimizerOptions.MinOrders = minOrders;
                                        optimizerOptions.PositiveOrderPct = positiveOrderPct;
                                        optimizerOptions.LoseLimitConstant = loseLimitConstant;
                                        optimizerOptions = optimizer.FindBestOptions(optimizerOptions, testCandles, 90, 1);
                                        EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, orderLimit, 1, 80);
                                        decimal lastResultPct = optimizerOptions.BestIndicatorPair.LastResult;
                                        StrategyBasic strategy = new StrategyGeneric(emulationConnection, securityInfo, optimizerOptions);
                                        strategy.Start();
                                        //Candle Simulation Still Fake
                                        for (int i = 0; i < realCandles.Count; i++)
                                        {
                                            strategy.ProcessCandle(realCandles[i]);
                                        }
                                        strategy.Stop();

                                        /*   
                                        StrategyBasic strategySell = new StrategyGeneric(emulationConnection, securityID, indicatorPairSell.LongIndicator, indicatorPairSell.ShortIndicator, marginSellPct, marginBuyPct, true, false);
                                        StrategyBasic strategyBuy = new StrategyGeneric(emulationConnection, securityID, indicatorPairBuy.LongIndicator, indicatorPairBuy.ShortIndicator, marginSellPct, marginBuyPct, false, true);
                                        strategySell.Start();
                                        strategyBuy.Start();
                                        //Candle Simulation Still Fake
                                        foreach (var candle in realCandles)
                                        {
                                            strategySell.ProcessCandle(candle);
                                            strategyBuy.ProcessCandle(candle);
                                        }
                                        strategySell.Stop();
                                        strategyBuy.Stop();
                                        */

                                        decimal profit = emulationConnection.GetTotalValue() - initialMoney;

                                        decimal profitPct = (profit / orderLimit * 100);
                                        System.Console.WriteLine("Portfolie End Value PCT: " + profitPct.ToString() + " - " + securityInfo + " - " + strategy.Connection.GetPortfolio().TotalValue);
                                        //System.Console.WriteLine(strategy.Connection.GetPortfolio().MaxLoseValue);

                                        /*
                                        var sorted = SortingAlgorithm.MergeSort(emulationConnections);


                                        Console.WriteLine("Portfolie End Value: " + (sorted[0] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[1] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[2] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[3] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[4] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[5] / initialMoney * 100).ToString());

                                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 5] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 4] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 3] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 2] / initialMoney * 100).ToString());
                                        Console.WriteLine("Portfolie End Value: " + (sorted[sorted.Count - 1] / initialMoney * 100).ToString());

                                        Console.WriteLine();
                                         */

                                        while (raceCondition)
                                        {
                                            Thread.Sleep(5);
                                        }

                                        raceCondition = true;
                                        endTime = DateTime.Now;
                                        System.Console.WriteLine("Start: " + startTime + " - End: " + endTime);
                                        csvWriter.WriteField(securityInfo);
                                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.ShortIndicator.ToString());
                                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.LongIndicator.ToString());
                                        csvWriter.WriteField((optimizerOptions.RecursiveTests));
                                        csvWriter.WriteField(optimizerOptions.IsSellEnabled);
                                        csvWriter.WriteField(optimizerOptions.IsBuyEnabled);
                                        csvWriter.WriteField(optimizerOptions.MinOrders);
                                        csvWriter.WriteField(optimizerOptions.PositiveOrderPct);
                                        csvWriter.WriteField(optimizerOptions.BestIndicatorPair.PositiveOrderPct);
                                        csvWriter.WriteField(optimizerOptions.MinProfitPct);
                                        csvWriter.WriteField(optimizerOptions.LoseLimitConstant);
                                        csvWriter.WriteField(lastResultPct);
                                        csvWriter.WriteField(profitPct);
                                        csvWriter.NextRecord();
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                        Console.WriteLine("NullReferenceException - No Strategy Found - " + securityInfo);

                                        skipSecurityIDs[securityInfo] = new decimal[]
                                        {
                                            loseLimitConstant,
                                            minOrders,
                                            positiveOrderPct,
                                            minProfitPct
                                        };
                                    }

                                    raceCondition = false;
                                    csvWriter.Flush();
                                    streamWriter.Flush();
                                }
                            }

                            //});
                        }
                    }
                }
                //}
            }

            streamWriter.Close();
        }
    }
}
