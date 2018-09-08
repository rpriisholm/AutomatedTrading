using RealLib;
using Stocks.Service;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Empty Console  
            TextWriter outTextWriter = Console.Out;
            TextWriter errTextWriter = Console.Error;
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
            #endregion


            DateTime startTime = DateTime.Now;
            //Should Amount To A Little More Than Two Years (Only Weekend Removed)
            int minNrOfTestValues = 766;

            OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
            bool isSellEnabled = false;
            //Legacy Test Should Be Moved And Needs Disable/Enable Sell
            StockSolution.Program.TestSelectedValuesAllData(startTime, minNrOfTestValues, optimizerOptions.MinOrders/2, optimizerOptions.PositiveOrderPct, optimizerOptions.MinProfitPct/2, optimizerOptions.LoseLimitConstant, isSellEnabled);
            StockSolution.Program.TestSelectedValuesAllData(startTime, minNrOfTestValues, optimizerOptions.MinOrders / 2, optimizerOptions.PositiveOrderPct, (int)(optimizerOptions.MinProfitPct * 1.5), optimizerOptions.LoseLimitConstant, isSellEnabled);
            StockSolution.Program.TestSelectedValuesAllData(startTime, minNrOfTestValues, optimizerOptions.MinOrders / 2, optimizerOptions.PositiveOrderPct, (int) (optimizerOptions.MinProfitPct * 0.66m) , optimizerOptions.LoseLimitConstant, isSellEnabled);
            StockSolution.Program.TestSelectedValuesAllData(startTime, minNrOfTestValues, optimizerOptions.MinOrders, optimizerOptions.PositiveOrderPct, optimizerOptions.MinProfitPct, optimizerOptions.LoseLimitConstant, isSellEnabled);

            /*
            DateTime now = DateTime.Now;
            List<DateTime> endDates = new List<DateTime>();
            for (int i = 7; i >= 0; i--)
            {
                //About 90 Trading Days // Need Better Calc?
                endDates.Add(now.AddDays(-i*(18*5+18*2))); 
            }

            foreach (DateTime endDate in endDates)
            {
                Dictionary<string, StrategyGeneric> newStrategies = TraderLib.FindNewStrategies(300, 6.25m, endDate);

                foreach (string symbol in newStrategies.Keys)
                {
                    StrategyGeneric strategyGeneric = newStrategies[symbol];
                    
                    //Run Simulation And Collect values
                }
            }
            

            Console.ReadLine();
            */
        }

        /*
        private static IList<SecurityInfo> LoadCandles(int minNrOfTestValues, DateTimeOffset dateMayNotBeOlderThan, )
        {
            #region Load Speed - 5 Minutes (18:43:25-18:48:36) - 7619 Files - 1,28 GB - Rows: 19230860 - (nu en enkelt security ekstra)
            //var startTime = DateTime.Now;
            string fullPath = ImportAndExport.GetFullPath(TickPeriod.Daily);
            IList<SecurityInfo> securityInfos = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, dateMayNotBeOlderThan.DateTime, startTime);

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

            return securityInfos;
        }
        */
    }
}
