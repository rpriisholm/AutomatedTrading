using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace TradingNotifierApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RealLib.CollectorLib.DataLocation = @"C:\StockHistory\RealTimeNotifierTest\Data.bin";


            decimal initialValue = 100000m;
            OrderLimitType orderLimitType = OrderLimitType.Value;
            decimal orderLimit = initialValue / 10;
            int leverage = 1;
            int maxInvestedPct = 80;
            IConnection connection = new EmulationConnection(initialValue, orderLimitType, orderLimit, leverage, maxInvestedPct);

            SecurityInfo securityId = new SecurityInfo() { SecurityID = "TEST_ID" };
            StrategyGeneric strategy = new StrategyGeneric(connection, securityId, OptimizerOptions.GetInstance(TickPeriod.Daily));
            List<StrategyGeneric> strategies = new List<StrategyGeneric>();
            strategies.Add(strategy);

            /*
            RealLib.CollectorLib.SaveStrategies(strategies);

            strategies = RealLib.CollectorLib.LoadStrategies();
            */
        }
    }
}
