using Ecng.Common;
using LumenWorks.Framework.IO.Csv;
using SandS.Algorithm.Library.SortNamespace;
using StockSharp.BusinessEntities;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StockSolution.Services
{
    public class LoaderService
    {
        public static IList<Candle> LoadLocalCandles(TimeSpan timeFrame, string storagePath, SecurityInfo securityID, DateTime startTime, DateTime stopTime)
        {
            return ConvertCsvToCandles(timeFrame, storagePath, securityID);
        }

        public static IDictionary<SecurityInfo, IList<Candle>> LoadLocalCandles(TimeSpan timeFrame, string storagePath, DateTime startTime, DateTime stopTime)
        {
            Dictionary<SecurityInfo, IList<Candle>> candles = new Dictionary<SecurityInfo, IList<Candle>>();
            List<SecurityInfo> failedSecurities = new List<SecurityInfo>();

            foreach (SecurityInfo securityID in GetSecurityIDs(storagePath))
            {
                try
                {
                    candles[securityID] = LoadLocalCandles(timeFrame, storagePath, securityID, startTime, stopTime);
                }
                catch (Exception e)
                {
                    failedSecurities.Add(securityID);
                    System.Console.WriteLine(e.ToString());
                }
            }
            failedSecurities.ForEach(failed => System.Console.WriteLine("Failed Security: " + failed));
            return candles;
        }

        private static IList<SecurityInfo> GetSecurityIDs(string storagePath)
        {
            string[] filePaths = Directory.GetFiles(storagePath, "*.csv");
            IEnumerable<string> IDs = filePaths.Select(Path.GetFileNameWithoutExtension);
            List<string> IdStrings = new List<string>(IDs);
            List<SecurityInfo> securityInfos = new List<SecurityInfo>();

            foreach (string id in IdStrings)
            {
                securityInfos.Add(new SecurityInfo() { SecurityID = id });
            }

            return securityInfos;
        }

        public static IList<Candle> ConvertCsvToCandles(TimeSpan timeFrame, string filePath, SecurityInfo securityID)
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
                DateTime openTime = new DateTime();

                if (csvReader.HasHeader("timestamp"))
                {
                    openTime = DateTime.Parse(csvReader["timestamp"]);
                }
                else
                {
                    if (csvReader.HasHeader("date"))
                    {
                        openTime = DateTime.Parse(csvReader["date"]);
                    }
                }

                Candle candle = new Candle
                {
                    SecurityID = securityID.SecurityID,
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
