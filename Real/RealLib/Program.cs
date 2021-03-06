﻿using Stocks.Service;
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
                        if (args.Length > 1 )
                        {
                            if (!(args[1].ToLower().Equals("NoDownload".ToLower())))
                            {
                                ImportAndExport.CollectData(TickPeriod.Daily, ImportAndExport.GetAllSymbols(), false, true);
                            }
                        }
                        else
                        {
                            ImportAndExport.CollectData(TickPeriod.Daily, ImportAndExport.GetAllSymbols(), false, true);
                        }

                        TraderLib.SimulateStrategies(TickPeriod.Daily);
                        TraderLib.SqlRunInserts(TickPeriod.Daily);
                    }

                    if(args[0].ToLower().Equals("InsertSQL".ToLower()))
                    {
                        TraderLib.SqlRunInserts(TickPeriod.Daily);
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
            
            /*catch (Exception e)
            {
                List<string> list = new List<string>();
                list.Add(e.ToString());
                list.Add(e.Data.ToString());
                list.Add("");
                File.AppendAllLines(@"c:\StockHistory\errors.txt", list);

                Console.WriteLine(e.ToString());
                Console.WriteLine("Press Enter To Continue...");
                Console.ReadLine();
            }*/
            
        }


        public static void FindAndSaveStratigies()
        {
            CollectorLib.DataLocation = @"c:\StockHistory\Real";
            TraderLib.RunTradingProgram(TickPeriod.Daily, TradingEnum.NewStrategies);
        }
    }
}
