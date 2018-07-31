﻿using CsvHelper;
using LumenWorks.Framework.IO.Csv;
using Stocks.Service;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using StockSolution.Services;
using StockSolution.Services.Optimizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickEnum;

namespace RealLib
{
    public class  CollectorLib
    {
        private static Dictionary<TickPeriod, IList<SecurityInfo>> SecurityInfos = new Dictionary<TickPeriod, IList<SecurityInfo>>();

        public static string DataLocation { get; set; }

        public static void DownloadHistoricalData()
        {
            ImportAndExport.CollectData(TickPeriod.Daily);
            /*
            ImportAndExport.CollectData(TickPeriod.SixMin);
            ImportAndExport.CollectData(TickPeriod.ThreeMin);
            ImportAndExport.CollectData(TickPeriod.OneMin);
            */
        }

        public static void DownloadCurrentData()
        {
            ImportAndExport.CollectData(TickPeriod.Daily);
            /*
            ImportAndExport.CollectData(TickPeriod.SixMin);
            ImportAndExport.CollectData(TickPeriod.ThreeMin);
            ImportAndExport.CollectData(TickPeriod.OneMin);
            */
        }

        public static void LoadStrategiesAndOrders()
        {

        }

        private static void LoadDailyData()
        {
            int minNrOfTestValues = 766;
            DateTimeOffset dateMayNotBeOlderThan = DateTimeOffset.UtcNow.AddDays(-(minNrOfTestValues * 1.7));
            DateTime lastDateTime = DateTime.Now;
            string fullPath = ImportAndExport.GetFullPath(TickPeriod.Daily);

            IList<SecurityInfo> securityInfos = LoaderService.LoadLocalCandles(TimeSpan.FromDays(1), fullPath, dateMayNotBeOlderThan.DateTime, lastDateTime);
            Dictionary<SecurityInfo, List<Candle>> dailySecurityInfos = new Dictionary<SecurityInfo, List<Candle>>();

            SecurityInfos[TickPeriod.Daily] = securityInfos;
        }



        public static void SaveStrategies(Dictionary<string, StrategyGeneric> strategyGenerics)
        {
            // Relocate old strategies csv using relocation date
            // Need One For Strategies which haven't expired yeat (Will expire on next change)

            StreamWriter streamWriter = new StreamWriter($"{DataLocation}\\CurrentStrategies.csv");
            CsvWriter csvWriter = new CsvWriter(streamWriter);

            csvWriter.WriteField("Symbol");
            csvWriter.WriteField("Last Execution");
            csvWriter.WriteField("Short Indicator");
            csvWriter.WriteField("Long Indicator");
            csvWriter.WriteField("Lose Limit Constant");
            //MAYBE PROFIT?
            csvWriter.NextRecord();

            foreach (string symbol in strategyGenerics.Keys)
            {
                csvWriter.WriteField(symbol);
                csvWriter.WriteField(strategyGenerics[symbol].LastExecution.ToString("yyyy-MM-dd hh-mm"));
                csvWriter.WriteField(strategyGenerics[symbol].ShortIndicator.ToString());
                csvWriter.WriteField(strategyGenerics[symbol].LongIndicator.ToString());
                csvWriter.WriteField(strategyGenerics[symbol].LoseLimitConstant.ToString());
                csvWriter.NextRecord();
            }

            csvWriter.Flush();
            streamWriter.Close();
            //WriteToBinaryFile<List<StrategyGeneric>>(DataLocation, strategyGenerics);
        }

        public static Dictionary<string, StrategyGeneric> LoadStrategies(ref IConnection connection, TickPeriod tickPeriod)
        {
            LoadDailyData();
            Dictionary<string, StrategyGeneric> strategies = new Dictionary<string, StrategyGeneric>();

            StreamReader streamReader = new StreamReader($"{DataLocation}\\CurrentStrategies.csv");
            CachedCsvReader csvReader = new CachedCsvReader(streamReader);
            Optimizer optimizer = new Optimizer();
            OptimizerOptions optimizerOptions = OptimizerOptions.GetInstance(tickPeriod);

            while (csvReader.ReadNextRecord())
            {
                // Prepare Strategy For Symbol
                // Need to load current candles up till last execution date


                string symbol = csvReader["Symbol"];
                SecurityInfo securityInfo = null;

                foreach(var security in SecurityInfos[TickPeriod.Daily])
                {
                    if(security.SecurityID.Equals(symbol))
                    {
                        securityInfo = security;
                        break;
                    }
                }

                LengthIndicator longIndicator = optimizer.FindIndicator(csvReader["Long Indicator"], optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);
                LengthIndicator shortIndicator = optimizer.FindIndicator(csvReader["Short Indicator"], optimizerOptions.IndicatorLength.Min, optimizerOptions.IndicatorLength.Max, optimizerOptions.IndicatorLength.IncrementIncrease);
                bool isBuyEnabled = true;
                bool isSellEnabled = true;
                DateTime lastExecution = DateTime.ParseExact(csvReader["Last Execution"], "yyyy-MM-dd hh-mm", CultureInfo.CurrentCulture);
                decimal loseLimitConstant = decimal.Parse(csvReader["Lose Limit Constant"]);

                foreach (Candle candle in securityInfo.Candles)
                {
                    if (lastExecution.CompareTo(candle.CloseTime) <= 0)
                    {
                        break;
                    }

                    longIndicator.Process(candle.ClosePrice, true);
                    shortIndicator.Process(candle.ClosePrice, true);
                }

                strategies[symbol] = new StrategyGeneric(connection, securityInfo, longIndicator, shortIndicator, isSellEnabled, isBuyEnabled, loseLimitConstant);
            }

            streamReader.Close();

            return strategies;
        }

        /*
            private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
            {
                using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, objectToWrite);
                }
            }

            //Muligt at et id kan blive nødvendigt til referencer (sættes i public set metoder) 
            private static T ReadFromBinaryFile<T>(string filePath)
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            */
    }
}
