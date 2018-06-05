using LumenWorks.Framework.IO.Csv;
using SandS.Algorithm.Library.SortNamespace;
using StockSharp.BusinessEntities;
using StockSharp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StockSharp.Services
{
    public class LoaderService
    {
        public static IList<Candle> LoadLocalCandles(TimeSpan timeFrame, string storagePath, string securityID, DateTime startTime, DateTime stopTime)
        {
            return ConvertCsvToCandles(timeFrame, storagePath, securityID);
        }

        public static IDictionary<string, IList<Candle>> LoadLocalCandles(TimeSpan timeFrame, string storagePath, DateTime startTime, DateTime stopTime)
        {
            Dictionary<string, IList<Candle>> candles = new Dictionary<string, IList<Candle>>();
            List<string> failedSecurities = new List<string>();
            
            foreach (string securityID in GetSecurityIDs(storagePath))
            {
                try
                {
                    candles[securityID] = LoadLocalCandles(timeFrame, storagePath, securityID, startTime, stopTime);
                }
                catch(Exception e)
                {
                    failedSecurities.Add(securityID);
                    System.Console.WriteLine(e.ToString());
                }
            }
            failedSecurities.ForEach(failed => System.Console.WriteLine("Failed Security: " + failed));
            return candles;
        }

        private static IList<string> GetSecurityIDs(string storagePath)
        {
            return new List<string>(Directory.GetFiles(storagePath, "*.csv").Select(Path.GetFileNameWithoutExtension));
        }

        public static IList<Candle> ConvertCsvToCandles(TimeSpan timeFrame, string filePath, string securityID)
        {
            //Candles
            StreamReader streamReader = new StreamReader($"{filePath}\\{securityID}.csv");
            CachedCsvReader csvReader = new CachedCsvReader(streamReader);
            //CsvReader csvReader = new CsvReader(streamReader);

            //Language Standard
            CultureInfo cultureInfo = new CultureInfo("en-US");

            //Create TimeFrameCandle List
            IList<Candle> candles = new List<Candle>();

            // Generate Candles
            while (csvReader.ReadNextRecord())
            {
                DateTime openTime = DateTime.Parse(csvReader["timestamp"]);

                Candle candle = new Candle
                {
                    SecurityID = securityID,
                    TimeFrame = timeFrame,
                    OpenTime = openTime,
                    CloseTime = openTime.Add(timeFrame),
                    OpenPrice = decimal.Parse(csvReader["open"], cultureInfo),
                    ClosePrice = decimal.Parse(csvReader["close"], cultureInfo),
                    HighPrice = decimal.Parse(csvReader["high"], cultureInfo),
                    LowPrice = decimal.Parse(csvReader["low"], cultureInfo),
                    TotalVolume = decimal.Parse(csvReader["volume"], cultureInfo)
                };

                candles.Add(candle);
            }
            candles = SortingAlgorithm.MergeSort(candles);
            return candles;
        }
    }
}
