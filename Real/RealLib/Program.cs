using Stocks.Service;
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
            //try
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
                        
                        //TickPeriod.Daily seems inderpendent when using continue
                        TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.ContinueTrading);
                    }

                    if (args[0].ToLower().Equals("AddSimulationToDatabase".ToLower()))
                    {
                        //COLLECT ALL .csv files before start
                    //Se Strings
                        ImportAndExport.CollectData(TickPeriod.Daily, ImportAndExport.GetAllSymbols(), false, true);
                        //TraderLib.SimulateStrategies("ALL", "SQL_Inserts");
                        TraderLib.SimulateStrategies("DAILY", "SQL_Inserts");
                    }
                }
                else
                {
                    //TraderLib.SimulateStrategies("ALL", "3");
                    TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.ContinueTrading);
                    //TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
                    //TraderLib.SimulateStrategies();
                }
                //SimulateSaveOnStartAndOnExit();
            }
            //catch (ExecutionEngineException e)
            /*
            catch (Exception e)
            {
                List<string> list = new List<string>();
                list.Add(e.ToString());
                list.Add(e.Data.ToString());
                list.Add("");
                File.AppendAllLines(@"c:\StockHistory\errors.txt", list);

                Console.WriteLine(e.ToString());
                Console.WriteLine("Press Enter To Continue...");
                Console.ReadLine();
            }
            */
        }


        public static void FindAndSaveStratigies()
        {
            CollectorLib.DataLocation = @"c:\StockHistory\Real";
            TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
        }
    }
}
