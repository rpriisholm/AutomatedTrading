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
            #region Empty Console
            TextWriter outTextWriter = Console.Out;
            TextWriter errTextWriter = Console.Error;
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
            #endregion

            TraderLib.RunTradingProgram(TradingEnum.ContinueTrading);

            //SimulateSaveOnStartAndOnExit();
        }


        public static void FindAndSaveStratigies()
        {
            CollectorLib.DataLocation = @"c:\StockHistory\Real";
            TraderLib.RunTradingProgram(TradingEnum.NewStrategies);
        }
    }
}
