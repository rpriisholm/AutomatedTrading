using StockSharp.Algo.Indicators;
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

namespace Services
{
    class Program
    {
        static void Main(string[] args)
        {
            Optimizer optimizer = new Optimizer();
            List<Candle> candles = new List<Candle>();
            List<IndicatorPair> indicatorPairs = Optimizer.CreateIndicatorPairs(candles, Permanent.PermanentValues.TestPairs);
            int res1 = optimizer.GetMaxIndicatorLength(Permanent.PermanentValues.TestPairs);

            Console.WriteLine();
        }
        
    }
}
