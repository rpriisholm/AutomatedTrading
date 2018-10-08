using CsvHelper;
using LumenWorks.Framework.IO.Csv;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Stocks.Export;
using Stocks.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TickEnum;
using static Stocks.Import.Other;

namespace Stocks.Service
{
    public static class ImportAndExport
    {
        public static string PartialPath = @"C:\StockHistory\Active\";
        public static decimal MinStockPrice = -1;

        public static void CSVToMySQL_Excel(string url, string path)
        {
            DB_Connector_MySQL connector = DB_Connector_MySQL.GetStocksConnector();
            MySqlConnection conn = connector.Connection;

            try
            {
                conn.Open();

                string commaSepperated = "sep =," + Environment.NewLine;
                Other.DownloadFile(url, path, commaSepperated);
                InteropExcel.ExportToMySql(path);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ClearDataFolder(TickPeriod tickPeriod, List<string> excludeList)
        {
            //string size = "full";
            try
            {
                DirectoryInfo directory = new DirectoryInfo(GetFullPath(tickPeriod));

                foreach (FileInfo file in directory.GetFiles())
                {
                    try
                    {
                        string name = file.Name.Split('.')[0];
                        bool isNotExcluded = excludeList.Contains(name);
                        if (!excludeList.Contains(name))
                        {
                            file.Delete();
                        }
                    }
                    catch
                    {
                        file.Delete();
                    }
                }
            }
            catch (IOException e) { }
        }

        public static void CollectData(TickPeriod tickPeriod, List<string> symbols, bool appendSymbols, bool deleteSymbols)
        {
            if (deleteSymbols)
            {
                if (!appendSymbols)
                {
                    ClearDataFolder(tickPeriod, new List<string>());
                }
                else
                {
                    ClearDataFolder(tickPeriod, symbols);
                }
            }

            Parallel.ForEach(symbols, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, symbol =>
            //foreach (var symbol in symbols)
            {
                CollectChoosenData(symbol, tickPeriod, appendSymbols);
            }
            );
            //Cleanup - Delete Files With Less Than 400 Rows
            Console.WriteLine("Done");
        }

        public static void CollectChoosenData(string symbol, TickPeriod tickPeriod, bool append)
        {
            string url = null;
            string path = $"{GetFullPath(TickPeriod.Daily)}\\{symbol}.csv";

            // Get Information About Stock
            // https://api.iextrading.com/1.0/stock/aapl/quote
            //-----/ /-----/ /-----/ /-----/  
            try
            {
                switch (tickPeriod)
                {
                    case TickPeriod.Daily:
                        url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/5y?format=csv";
                        break;

                    case TickPeriod.SixMin:
                        url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/6m?format=csv";
                        break;

                    case TickPeriod.ThreeMin:
                        url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/3m?format=csv";
                        break;

                    case TickPeriod.OneMin:
                        url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/1m?format=csv";
                        break;
                }

                //Start Collecting
                SingleDataCollector(path, url, symbol, append);

                // https://api.iextrading.com/1.0/stock/market/batch?symbols=aapl,fb&types=quote,news,chart&range=1m&last=5

                // Daily 5 Years
                // url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/5y?format=csv";
                // SingleDataCollector(path, url);
            }
            finally { } /*
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }*/
        }

        /* *
         * Special Signs Not Allowed
         * These: \ / : * " < > |
         * */
        public static void SingleDataCollector(string path, string url, string symbol, bool append)
        {
            bool isPathValied = true;

            try
            {
                Path.GetFullPath(path);
            }
            catch (System.ArgumentException e)
            {
                isPathValied = false;
            }

            if (isPathValied)
            {

                string csvContent = null;

                if (!append || !File.Exists(path))
                {
                    //CSVToMySQL_Excel(url, path);
                    bool hasCompleted = false;
                    int count = 0;
                    int tries = 20;
                    while (!hasCompleted && count < tries)
                    {
                        try
                        {
                            csvContent = Other.Download(url);

                            if (!string.IsNullOrWhiteSpace(csvContent))
                            {
                                hasCompleted = true;
                            }
                        }
                        catch { }
                        count++;
                    }
                }

                StreamReader stream_Append = null;
                StringReader stream_nonAppend = null;
                CachedCsvReader csvReader = null;

                try
                {
                    if (append && File.Exists(path))
                    {
                        stream_Append = new StreamReader(path);
                        csvReader = new CachedCsvReader(stream_Append);
                    }
                    else
                    {
                        stream_nonAppend = new StringReader(csvContent);
                        csvReader = new CachedCsvReader(stream_nonAppend);
                    }
                }
                catch { }

                if (stream_Append != null || stream_nonAppend != null)
                {
                    int rows = 0;
                    while (csvReader.ReadNextRecord())
                    {
                        rows++;

                        if (rows >= 400)
                        {
                            break;
                        }
                    }

                    if (stream_Append != null)
                    {
                        stream_Append.Close();
                    }
                    else
                    {
                        stream_nonAppend.Close();
                    }


                    if (rows < 400)
                    {
                        File.Delete(path);
                    }
                    else
                    {
                        StringReader stringReader = null;
                        LumenWorks.Framework.IO.Csv.CsvReader csv = null;
                        bool hasArgumentException = false;

                        try
                        {
                            if (csvContent != null)
                            {
                                File.WriteAllLines(path, csvContent.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None));
                                stringReader = new StringReader(csvContent);
                                csv = new LumenWorks.Framework.IO.Csv.CsvReader(stringReader, true);
                            }
                            else
                            {
                                stringReader = new StringReader(File.ReadAllText(path));
                                csv = new LumenWorks.Framework.IO.Csv.CsvReader(stringReader, true);
                            }
                        }
                        catch (System.ArgumentException e)
                        {
                            hasArgumentException = true;
                        }

                        if (!hasArgumentException)
                        {
                            string[] headerRow = csv.GetFieldHeaders();

                            /*Currently Used
                            string lastPriceUrl = @"https://api.iextrading.com/1.0/stock/" + symbol + @"/chart/1d?chartReset=true&changeFromClose=true&chartSimplify=true&chartLast=1&format=csv";
                            string lastPriceCsv = Other.Download(lastPriceUrl);
                            */
                            StreamWriter streamWriter = new StreamWriter(path, true);
                            CsvWriter csvWriter = new CsvWriter(streamWriter);


                            //JSON TEST
                            JObject jsonObj = JObject.Parse(Other.Download(@"https://api.iextrading.com/1.0/stock/" + symbol + "/quote"));
                            Dictionary<string, object> dictObj = jsonObj.ToObject<Dictionary<string, object>>();
                            decimal closePrice = -1;

                            foreach (string header in headerRow)
                            {
                                bool isMatch = false;
                                foreach (string header2 in dictObj.Keys)
                                {
                                    if (((header.Equals(header2) && !header2.Equals("date") && !header2.Equals("close")) || (header.Equals("date") && header2.Equals("extendedPriceTime")) || (header.Equals("close") && header2.Equals("latestPrice"))) && dictObj[header2] != null)
                                    {
                                        string field = dictObj[header2].ToString();

                                        try
                                        {
                                            string[] array = field.Split(',');


                                            if (array.Length == 2)
                                            {
                                                field = field.Replace(',', '.');
                                            }
                                        }
                                        catch { }

                                        /*
                                        if (header2.Equals("closeTime"))
                                        {
                                            field = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(long.Parse(field)).ToString(@"yyyy-MM-dd");
                                        }
                                        */

                                        if (header2.Equals("extendedPriceTime"))
                                        {
                                            field = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(long.Parse(field)).ToString(@"yyyy-MM-dd");
                                        }

                                        isMatch = true;
                                        csvWriter.WriteField(field);
                                        /*
                                        if (header2.Equals("close"))
                                        {
                                            closePrice = decimal.Parse(field);
                                        }
                                        */

                                        if (header2.Equals("latestPrice"))
                                        {
                                            closePrice = decimal.Parse(field);
                                        }
                                    }
                                }

                                if (!isMatch)
                                {
                                    csvWriter.WriteField("");
                                }
                            }
                            csvWriter.NextRecord();
                            csvWriter.Flush();
                            streamWriter.Flush();
                            stringReader.Close();
                            streamWriter.Close();

                            if (closePrice < MinStockPrice && append == false)
                            {
                                File.Delete(path);
                            }
                        }

                        if (File.Exists(path))
                        {
                            RemoveDuplicates(path);
                        }
                    }
                }
            }
        }

        private static void RemoveDuplicates(string path)
        {
            List<string> output = new List<string>();
            HashSet<string> dates = new HashSet<string>();
            StringReader stringReader = new StringReader(File.ReadAllText(path));
            LumenWorks.Framework.IO.Csv.CsvReader csv = new LumenWorks.Framework.IO.Csv.CsvReader(stringReader, true);
            string[] headers = csv.GetFieldHeaders();
            string newCsvContent = "";


            foreach (string header in headers)
            {
                newCsvContent += header + ',';
            }
            newCsvContent = newCsvContent.TrimEnd(',');
            output.Add(newCsvContent);

            while (csv.ReadNextRecord())
            {
                newCsvContent = "";
                if (!dates.Contains(csv["date"]))
                {
                    dates.Add(csv["date"]);
                    foreach (string header in headers)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(csv[header]))
                            {
                                newCsvContent += csv[header] + ',';
                            }
                            else { newCsvContent += "-1" + ','; }
                        }
                        catch { newCsvContent += "-1" + ','; }
                    }
                    newCsvContent = newCsvContent.TrimEnd(',');
                    output.Add(newCsvContent);
                }
            }
            stringReader.Close();
            output.Add("");
            File.WriteAllLines(path, output.ToArray());
        }

        public static List<string> LoadStrategiesSymbols(string dataLocation, string fileName)
        {
            List<string> symbols = new List<string>();

            if (File.Exists($"{dataLocation}\\{fileName}"))
            {
                StreamReader streamReader = new StreamReader($"{dataLocation}\\{fileName}");
                CachedCsvReader csvReader = new CachedCsvReader(streamReader);
                while (csvReader.ReadNextRecord())
                {
                    string symbol = csvReader["Symbol"];
                    symbols.Add(symbol);
                }

                streamReader.Close();
            }

            return symbols;
        }

        public static List<string> GetAllSymbols()
        {
            List<string> symbols = new List<string>();

            CsvContainer CSV = Other.DownloadCSV("https://api.iextrading.com/1.0/ref-data/symbols?format=csv");

            /* Legacy (very simpel)
            StreamReader streamReader = new StreamReader(@"C:\StockHistory\StockList");
            while (streamReader.Peek() >= 0)
            {
                symbols.Add(streamReader.ReadLine().Trim());
            }
            */

            List<string> filteredSymbols = new List<string>();

            //Filter Symbols
            for (int i = 0; i < CSV["symbol"].Count; i++)
            {
                //Crypto is Disabled
                if (!CSV["isEnabled"][i].Equals("false") && !CSV["type"][i].Equals("crypto"))
                {
                    filteredSymbols.Add(CSV["symbol"][i]);
                }
            }

            return filteredSymbols;
        }

        public static string GetFullPath(TickPeriod tickPeriod)
        {
            string path = PartialPath;

            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    path += @"Daily";
                    break;
                case TickPeriod.DailySimulation:
                    path += @"DailySimulation";
                    break;
                case TickPeriod.SixMin:
                    path += @"6Min";
                    break;
                case TickPeriod.ThreeMin:
                    path += @"3Min";
                    break;
                case TickPeriod.OneMin:
                    path += @"1Min";
                    break;
            }

            return path;
        }
    }
}
