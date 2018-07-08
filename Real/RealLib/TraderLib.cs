using Stocks.Service;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace RealLib
{
    public class TraderLib
    {
        public static void StartTrading()
        {
            //LOAD Current Strategies
            //Locate New Strategies
            //SET LAST STRATEGY Or OVERRIDE / Change Existing Seurities To New (Might have to change security profit to 0m) 
            //SET All or rest of Profits to 0m
            //ADD Candles Until Now - Console Buy And Sell
        }

        public static void ContinueTrading()
        {
            //LOAD Current Strategies
            //LOAD USED CANDLES OR THE LAST STATE
            //ADD CANDLES UNTIL NOW - Console Buy And Sell
            
        }

        public static void OnStart(string dataLocation)
        {
            CollectorLib.DataLocation = dataLocation;
            CollectorLib.LoadStrategies();
            // Load Others - Connection Is Important
        }

        public static void OnExit(List<StrategyGeneric> strategies)
        {
            CollectorLib.SaveStrategies(strategies);
            // Save Others - Connection Is Important
        }

        public static void AddCandleToStrategy(StrategyGeneric strategy, Candle candle)
        {

        }
        /*
        public static void FindStrategies()
        {
            DateTime startDate = DateTime.Now.AddYears(-5);
            DateTime endDate = DateTime.Now;
            TimeSpan timeSpan = TimeSpan.FromDays(1);
            string path = ImportAndExport.GetFullPath(TickPeriod.Daily);
            
            IDictionary<SecurityInfo, IList<Candle>> symbolsAndCandles = LoaderService.LoadLocalCandles(timeSpan, path, startDate, endDate);

        }
        */
    }
}
