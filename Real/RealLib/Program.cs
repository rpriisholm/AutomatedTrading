using StockSharp.Algo.Indicators;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace RealLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);

            string dataLocation = @"C:\StockHistory\TEST";
            CollectorLib.DataLocation = dataLocation;
            Dictionary<string, StrategyGeneric> strategies = new Dictionary<string, StrategyGeneric>();
            LengthIndicator shortIndicator = new LengthIndicator(new SimpleMovingAverage() { Length = 5 });
            LengthIndicator longIndicator = new LengthIndicator(new SimpleMovingAverage() { Length = 10 });

            strategies["AABA"] = new StrategyGeneric(TraderLib.emulationConnection, new SecurityInfo() { SecurityID = "AABA" }, longIndicator, shortIndicator, true, true, -0.12m);
            strategies["ABMD"] = new StrategyGeneric(TraderLib.emulationConnection, new SecurityInfo() { SecurityID = "ABMD" }, longIndicator, shortIndicator, true, true, -0.12m);

            CollectorLib.SaveStrategies(strategies);

            TraderLib.OnStart(dataLocation);

            TraderLib.OnExit(TraderLib.Strategies);
        }
    }
}
