using RealLib;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using TickEnum;

namespace SaveAndLoadStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataLocation = @"C:\StockHistory\TEST";
            CollectorLib.DataLocation = dataLocation;
            Dictionary<string, StrategyGeneric> strategies = new Dictionary<string, StrategyGeneric>();
            strategies["ALBO"] = new StrategyGeneric(TraderLib.emulationConnection, new SecurityInfo() { SecurityID = "ALBO" }, OptimizerOptions.GetInstance(TickPeriod.Daily));
            strategies["AGMH"] = new StrategyGeneric(TraderLib.emulationConnection, new SecurityInfo() { SecurityID = "AGMH" }, OptimizerOptions.GetInstance(TickPeriod.Daily));

            CollectorLib.SaveStrategies(strategies);

            TraderLib.OnStart(dataLocation);

            TraderLib.OnExit(TraderLib.Strategies);
        }
    }
}
