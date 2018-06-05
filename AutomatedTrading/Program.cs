using CsvHelper;
using StockSolution.Model;
using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockSolution
{
    class Program
    {
        static void Main(string[] args)
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

            IDictionary<string, IList<Candle>> candlesDictionary = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), @"C:\StockHistory\TEST", new DateTime(2000, 1, 1), new DateTime(2019, 1, 1));
            var endTime = DateTime.Now;
            int rows = 0;
            foreach (string securityID in candlesDictionary.Keys)
            {
                rows += candlesDictionary[securityID].Count;
            }

            System.Console.WriteLine("Start: " + startTime + " - End: " + endTime);
            System.Console.WriteLine("Securities: " + candlesDictionary.Keys.Count + " - Rows: " + rows);
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

            Dictionary<string, decimal[]> skipSecurityIDs = new Dictionary<string, decimal[]>();

            for (decimal loseLimitConstant = -0.12m; loseLimitConstant <= -0.045m; loseLimitConstant += 0.015m)
            {
                int minOrdersMax = 25;
                for (int minOrders = 10; minOrders <= minOrdersMax; minOrders += 5)
                {
                    int positiveOrderPctMax = 84;
                    for (int positiveOrderPct = 60; positiveOrderPct <= positiveOrderPctMax; positiveOrderPct += 12)
                    {
                        int minProfitPctMax = 40;
                        for (int minProfitPct = 10; minProfitPct <= minProfitPctMax; minProfitPct += 5)
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
                            foreach (string securityID in candlesDictionary.Keys)
                            {
                                /*
                                decimal loseLimitConstant = -0.105m;
                                int minOrders = 10;
                                int positiveOrderPct = 84;
                                int minProfitPct = 20;
                                */


                                if (!IsSkipped(skipSecurityIDs, securityID, loseLimitConstant, minOrders, positiveOrderPct, minProfitPct))
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

                                    List<Candle> candles = candlesDictionary[securityID].ToList();
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
                            }

                            //});
                        }
                    }
                }
                //}
            }

            streamWriter.Close();
        }

        private static bool IsSkipped(Dictionary<string, decimal[]> skipSecurityIDs, string securityId, decimal loseLimitConstant, int minOrders, int positiveOrderPct, int minProfitPct)
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

        public static List<List<T>> BreakIntoChunks<T>(List<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize must be greater than 0.");
            }

            List<List<T>> retVal = new List<List<T>>();

            while (list.Count > 0)
            {
                int count = list.Count > chunkSize ? chunkSize : list.Count;
                retVal.Add(list.GetRange(0, count));
                list.RemoveRange(0, count);
            }

            return retVal;
        }
    }
}
