using System;
using System.Collections.Generic;
using System.IO;
using TickEnum;

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
                        TraderLib.SimulateStrategies("ALL", args[1].ToString());
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
            catch (Exception e)
            {
                List<string> list = new List<string>();
                list.Add(e.ToString());
                list.Add(e.Data.ToString());
                list.Add("Inner Execption: " + e.InnerException.ToString());
                list.Add("");
                File.AppendAllLines(@"c:\StockHistory\", list);

                Console.WriteLine(e.ToString());
                Console.WriteLine("Press Enter To Continue...");
                Console.ReadLine();
            }
        }


        public static void FindAndSaveStratigies()
        {
            CollectorLib.DataLocation = @"c:\StockHistory\Real";
            TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
        }
    }
}
