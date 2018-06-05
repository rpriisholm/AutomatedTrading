using System;
using System.Collections.Generic;
using System.Linq;
using StockSolution.Model;
using StockSolution.Services;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
            var startTime = DateTime.Now;
            Console.WriteLine(startTime);

            IDictionary<string, IList<Candle>> candlesDictionary = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), @"C:\StockHistory\TEST", new DateTime(2000, 1, 1), new DateTime(2019, 1, 1));
            var endTime = DateTime.Now;
            int rows = 0;
            foreach (string securityID in candlesDictionary.Keys)
            {
                rows += candlesDictionary[securityID].Count;
            }

            Console.WriteLine("Start: " + startTime + " - End: " + endTime);
            Console.WriteLine("Securities: " + candlesDictionary.Keys.Count + " - Rows: " + rows);
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
            List<decimal> emulationConnections = new List<decimal>();

            startTime = DateTime.Now;

            Console.WriteLine();
            foreach (string securityID in candlesDictionary.Keys)
            {
                int initialMoney = 100000;
                decimal marginSellPct = 2.0m;
                decimal marginBuyPct = 2.0m;

                StrategyBasic strategy;

                Optimizer optimizer = new Optimizer();
                List<IndicatorPair> indicatorPairs = optimizer.GetIndicatorPairs();
                List<Candle> candles = candlesDictionary[securityID].ToList();

                int realValues = 364;
                List<Candle> realCandles = candles.GetRange(candles.Count - (realValues + 1), realValues);
                candles.RemoveRange(candles.Count - (realValues + 1), realValues);

                IndicatorPair indicatorPair = optimizer.CurrentBestStrategy(securityID, candles, 64, realValues);
                EmulationConnection emulationConnection = new EmulationConnection(initialMoney, OrderLimitType.Value, initialMoney, 1, 80);
                strategy = new StrategyGeneric(emulationConnection, securityID, indicatorPair.LongIndicator, indicatorPair.ShortIndicator, marginSellPct, marginBuyPct);
                strategy.Start();
                //Candle Simulation Still Fake
                foreach (var candle in realCandles)
                {
                    strategy.ProcessCandle(candle);
                }
                strategy.Stop();
                Console.WriteLine("Portfolie End Value: " + (emulationConnection.GetTotalValue() / initialMoney * 100).ToString());


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


                Console.WriteLine("Start: " + startTime + " - End: " + endTime);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
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
