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
using Xceed.Wpf.Toolkit;

namespace RealLib
{
    class Program
    {
        static void Main(string[] args)
        {
            //CollectorLib.DataLocation = @"c:\StockHistory\Real";
            try
            {
                #region Empty Console  
                TextWriter outTextWriter = Console.Out;
                TextWriter errTextWriter = Console.Error;
                //Console.SetOut(TextWriter.Null);
                //Console.SetError(TextWriter.Null);
                #endregion

                if (args.Length != 0)
                {
                    if (args[0].ToLower().Equals("NewStrategies".ToLower()))
                    {
                        TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
                    }

                    if (args[0].ToLower().Equals("ContinueTrading".ToLower()))
                    {
                        TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.ContinueTrading);
                    }

                    if (args[0].ToLower().Equals("AddSimulationToDatabase".ToLower()))
                    {
                        TraderLib.SimulateStrategies();
                    }
                }
                else
                {
                    //TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.ContinueTrading);
                    //TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
                    //TraderLib.SimulateStrategies();
                }
                //SimulateSaveOnStartAndOnExit();
            }
            catch ( Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public static void FindAndSaveStratigies()
        {
            CollectorLib.DataLocation = @"c:\StockHistory\Real";
            TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
        }
    }
}
