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
            int minNrOfTestValues = 290;

            OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(TickPeriod.Daily);
            bool isSellEnabled = false;
            //Legacy Test Should Be Moved And Needs Disable/Enable Sell



            for (int minOrders = 8; minOrders <= 14; minOrders += 2)
            {
                for (int minPositiveOrderPct = 60; minPositiveOrderPct <= 75; minPositiveOrderPct += 5)
                {
                    for (int minProfitPct = 15; minProfitPct <= 35; minProfitPct += 5)
                    {
                        for (decimal loseLimitConstant = -0.08m; loseLimitConstant <= -0.04m; loseLimitConstant += 0.01m)
                        {
                            StockSolution.Program.TestSelectedValuesAllData(startTime, minNrOfTestValues, minOrders, minPositiveOrderPct, minProfitPct, loseLimitConstant, isSellEnabled);
                        }
                    }
                }
            }


            // SKIP SYMBOL
            /*
            if (!IsSkipped(skipSecurityIDs, securityID, loseLimitConstant, minOrders, positiveOrderPct, minProfitPct))
            {

            } */
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

    }
}
