using Ecng.Common;
using LumenWorks.Framework.IO.Csv;
using SandS.Algorithm.Library.SortNamespace;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Services
{
    public class LoaderService
    {
        public static List<string> FailedSecurities = new List<string>();
        public static SecurityInfo LoadLocalCandles(TimeSpan timeFrame, string storagePath, string securityID, DateTime startTime, DateTime stopTime)
        {
            return ConvertCsvToCandles(timeFrame, storagePath, securityID);
        }

        public static IList<SecurityInfo> LoadLocalCandles(TimeSpan timeFrame, string storagePath, DateTime startTime, DateTime stopTime)
        {
            IList<SecurityInfo> securityInfoes = new List<SecurityInfo>();
            List<string> failedSecurities = new List<string>();

            //foreach (string securityID in GetSecurityIDs(storagePath))
            Parallel.ForEach(GetSecurityIDs(storagePath), securityID =>
            {
                //try
                {
                    SecurityInfo securityInfo = LoadLocalCandles(timeFrame, storagePath, securityID, startTime, stopTime);

                    if (securityInfo != null)
                    {
                        securityInfoes.Add(securityInfo);
                    }
                    else
                    {
                        throw new InvalidDataException("Security Info Is Null");
                    }

                }
                
                /*
                catch (Exception e)
                {
                    failedSecurities.Add(securityID);
                    System.Console.WriteLine(e.ToString());
                    System.Console.WriteLine(new StackTrace(e, true).GetFrame(0).GetFileLineNumber());
                }
                */
                
            }
            );
            failedSecurities.ForEach(failed => System.Console.WriteLine("Failed Security: " + failed));
            FailedSecurities = failedSecurities;
            return securityInfoes;
        }

        public static IList<string> GetSecurityIDs(string storagePath)
        {
            string[] filePaths = Directory.GetFiles(storagePath, "*.csv");
            IEnumerable<string> IDs = filePaths.Select(Path.GetFileNameWithoutExtension);
            List<string> IdStrings = new List<string>(IDs);

            return IdStrings;
        }

        public static SecurityInfo ConvertCsvToCandles(TimeSpan timeFrame, string filePath, string securityID)
        {
            //Create SecurityInfo
            SecurityInfo securityInfo = new SecurityInfo() { SecurityID = securityID };

            //Candles
            StreamReader streamReader = new StreamReader($"{filePath}\\{securityID}.csv");
            CachedCsvReader csvReader = new CachedCsvReader(streamReader, true);

            //Language Standard
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            try
            {
                // Generate Candles
                while (csvReader.ReadNextRecord())
                {
                    csvReader.ReadNextRecord()

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

                    Candle candle = new Candle();
                    candle.TimeFrame = timeFrame;
                    candle.OpenTime = openTime;
                    candle.CloseTime = openTime.Add(timeFrame);

                    candle.ClosePrice = decimal.Parse(csvReader["close"], cultureInfo);

                    try
                    {
                        candle.OpenPrice = decimal.Parse(csvReader["open"], cultureInfo);
                    }
                    catch { }

                    try
                    {
                        candle.HighPrice = decimal.Parse(csvReader["high"], cultureInfo);
                    }
                    catch { }
                    
                    try
                    {
                        candle.LowPrice = decimal.Parse(csvReader["low"], cultureInfo);
                    }
                    catch { }
                    //TotalVolume = decimal.Parse(csvReader["volume"], cultureInfo)


                    securityInfo.Candles.Add(candle);
                    securityInfo.Candles = (List<Candle>)SortingAlgorithm.MergeSort(securityInfo.Candles);
                }
            }
            catch(System.FormatException e)
            {
                securityInfo.Candles = null;
            }

            return securityInfo;
        }
    }
}
