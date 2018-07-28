using CsvHelper;
using LumenWorks.Framework.IO.Csv;
using Stocks.Service;
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

namespace RealLib
{
    public class CollectorLib
    {
        public static string DataLocation { get; set; }

        public static void DownloadHistoricalData()
        {
            ImportAndExport.CollectData(TickPeriod.Daily);
        }

        public static void DownloadCurrentData()
        {
            ImportAndExport.CollectData(TickPeriod.Daily);
            ImportAndExport.CollectData(TickPeriod.SixMin);
            ImportAndExport.CollectData(TickPeriod.ThreeMin);
            ImportAndExport.CollectData(TickPeriod.OneMin);
        }

        public static void LoadData()
        {

        }

        public static void LoadStrategiesAndOrders()
        {

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
            //MAYBE PROFIT?
            csvWriter.NextRecord();

            foreach (string symbol in strategyGenerics.Keys)
            {
                csvWriter.WriteField(symbol);
                csvWriter.WriteField(strategyGenerics[symbol].LastExecution.ToString("yyyy-MM-dd hh-mm"));
                csvWriter.WriteField(strategyGenerics[symbol].ShortIndicator.ToString());
                csvWriter.WriteField(strategyGenerics[symbol].LongIndicator.ToString());
                csvWriter.NextRecord();
            }

            csvWriter.Flush();
            streamWriter.Close();
            //WriteToBinaryFile<List<StrategyGeneric>>(DataLocation, strategyGenerics);
        }

        public static Dictionary<string,StrategyGeneric> LoadStrategies()
        {

            Dictionary<string, StrategyGeneric> strategies = new Dictionary<string, StrategyGeneric>();

            StreamReader streamReader = new StreamReader($"{DataLocation}\\CurrentStrategies.csv");
            CachedCsvReader csvReader = new CachedCsvReader(streamReader);

            while (csvReader.ReadNextRecord())
            {
                // Prepare Strategy For Symbol
                // Need to load current candles up till last execution date
                strategies[csvReader["Symbol"]] = new StrategyGeneric(csvReader["Short Indicator"], csvReader["Long Indicator"], csvReader["Last Execution"], candles);
            }

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
