﻿using Stocks.Service;
using StockSolution.Model;
using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealLib
{
    public class CollectorLib
    {
        public static string DataLocation { get; set; }

        public static void DownloadHistoricalData()
        {
            ImportAndExport.CollectData(CollectorType.Daily);
        }

        public static void DownloadCurrentData()
        {
            ImportAndExport.CollectData(CollectorType.Daily);
            ImportAndExport.CollectData(CollectorType.SixMin);
            ImportAndExport.CollectData(CollectorType.ThreeMin);
            ImportAndExport.CollectData(CollectorType.OneMin);
        }

        public static void LoadData()
        {

        }

        public static void LoadStrategiesAndOrders()
        {
            
        }

        public static void SaveStrategies(List<StrategyGeneric> strategyGenerics)
        {
            WriteToBinaryFile<List<StrategyGeneric>>(DataLocation, strategyGenerics);
        }

        public static List<StrategyGeneric> LoadStrategies()
        {
            List<StrategyGeneric> strategies = null;

            try
            {
                strategies = ReadFromBinaryFile<List<StrategyGeneric>>(DataLocation);
            }
            catch { /* Do Nothing */ }

            return strategies;
        }

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
    }
}
