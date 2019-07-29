using LumenWorks.Framework.IO.Csv;
using SandS.Algorithm.Library.SortNamespace;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public static int CountCandleLines(string storagePath, string securityID)
        {
            FileInfo file = new FileInfo($"{storagePath}\\{securityID}.csv");
            return File.ReadLines(file.FullName).Count() - 5;
        }

        public static SecurityInfo ConvertCsvToCandles(TimeSpan timeFrame, string filePath, string securityID)
        {
            //Create SecurityInfo
            SecurityInfo securityInfo = new SecurityInfo() { SecurityID = securityID };

            //Candles
            StreamReader streamReader = new StreamReader($"{filePath}\\{securityID}.csv");
            try
            {
                CachedCsvReader csvReader = new CachedCsvReader(streamReader, true);

                //Language Standard
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;

                /*try
                { */
                // Generate Candles
                while (csvReader.ReadNextRecord())
                {
                    DateTime openTime = new DateTime();
                    try
                    {
                        if (csvReader.HasHeader("Date"))
                        {
                            if (csvReader.HasHeader("Date"))
                            {
                                //openTime = DateTime.Parse(csvReader["timestamp"]);
                                string ds = csvReader["Date"];
                                openTime = DateTime.ParseExact(ds, "yyyy-MM-dd", cultureInfo);
                            }
                            else
                            {
                                if (csvReader.HasHeader("date"))
                                {
                                    //openTime = DateTime.Parse(csvReader["date"]);
                                    string ds = csvReader["date"];
                                    openTime = DateTime.Parse(ds, cultureInfo);
                                }
                            }
                        }
                    }
                    catch (System.FormatException e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("At Candles To CSV");

                        Debug.WriteLine(e.ToString());
                        Debug.WriteLine(e.Data.ToString());
                    }

                    Candle candle = new Candle();
                    candle.TimeFrame = timeFrame;
                    candle.OpenTime = openTime;
                    candle.CloseTime = openTime.Add(timeFrame);


                    if (csvReader.HasHeader("Close"))
                    {
                        try
                        {
                            cultureInfo = new CultureInfo("en-US", false);
                            string value = csvReader["Close"];
                            candle.ClosePrice = decimal.Parse(value, cultureInfo);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            throw e;
                        }
                    }
                    else
                    {
                        candle.ClosePrice = decimal.Parse(csvReader["close"], cultureInfo);
                    }

                    try
                    {
                        if (csvReader.HasHeader("Open"))
                        {
                            candle.OpenPrice = decimal.Parse(csvReader["Open"], cultureInfo);
                        }
                        else
                        {
                            candle.OpenPrice = decimal.Parse(csvReader["open"], cultureInfo);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw e;
                    }

                    try
                    {
                        if (csvReader.HasHeader("High"))
                        {
                            candle.HighPrice = decimal.Parse(csvReader["High"], cultureInfo);
                        }
                        else
                        {
                            candle.HighPrice = decimal.Parse(csvReader["high"], cultureInfo);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw e;
                    }

                    try
                    {
                        if (csvReader.HasHeader("High"))
                        {
                            candle.LowPrice = decimal.Parse(csvReader["low"], cultureInfo);
                        }
                        else
                        {
                            candle.LowPrice = decimal.Parse(csvReader["Low"], cultureInfo);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        throw e;
                    }
                    //TotalVolume = decimal.Parse(csvReader["volume"], cultureInfo)


                    if (candle.ClosePrice_ > 0m)
                    {
                        securityInfo.Candles.Add(candle);
                    }
                }

                securityInfo.Candles = (List<Candle>)SortingAlgorithm.MergeSort(securityInfo.Candles);

                /* 
             }

             catch(System.FormatException e)
             {
                 securityInfo.Candles = null;
             }
             */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                var st = new StackTrace(e, true);
                Console.WriteLine(st);
                var frame = st.GetFrame(0);
                Console.WriteLine(frame);
                var line = frame.GetFileLineNumber();
                Console.WriteLine(line);
                throw e;
            }
            finally
            {
                streamReader.Close();
            }
            return securityInfo;
        }
    }
}
