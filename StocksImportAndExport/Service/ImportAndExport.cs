using CsvHelper;
using LumenWorks.Framework.IO.Csv;
//using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
//using Stocks.Export;
using Stocks.Import;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using TickEnum;
using static Stocks.Import.Other;

namespace Stocks.Service
{
    public static class ImportAndExport
    {
        public static string SecretToken = Environment.GetEnvironmentVariable("SecretToken");
        public static string PartialPath = @"C:\StockHistory\Active\";
        public static string UsdPath = @"C:\StockHistory\Real\USD.csv";
        public static decimal MinStockPrice = -1;
        public static Dictionary<string, decimal> UnitPrices = new Dictionary<string, decimal>();

        /*
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
        
        */





        //Day-Month-Year - dd-MM-yyyy  
        public static decimal GetUsdUnitPrice(string date)
        {

            decimal result = 0.0m;
            string symbol = "USD";

            if (UnitPrices.Count == 0)
            {
                StreamReader stream_Append = new StreamReader(UsdPath);
                CachedCsvReader csvReader = new CachedCsvReader(stream_Append);

                while (csvReader.ReadNextRecord())
                {
                    UnitPrices[csvReader[0]] = decimal.Parse(csvReader[1], new System.Globalization.CultureInfo("en-US"));
                }
                stream_Append.Close();


                CsvContainer csv = DownloadCSV("http://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRateCSV?lang=en&iso=" + $"{symbol}", ';', '"');
                for (int i = 0; i < csv["Date"].Count; i++)
                {
                    if (!UnitPrices.ContainsKey(csv["Date"][i]))
                    {
                        UnitPrices[csv["Date"][i]] = decimal.Parse(csv["US dollars"][i], new System.Globalization.CultureInfo("en-US"));
                    }
                }


                StreamWriter streamWriter = new StreamWriter(UsdPath);
                char cellSeperator = ',';
                streamWriter.WriteLine("Date" + cellSeperator + "US dollars");
                for (int i = 0; i < csv["Date"].Count; i++)
                {
                    streamWriter.WriteLine(csvReader[0] + cellSeperator + csvReader[1]);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }


            if (!UnitPrices.ContainsKey(date))
            {
                DateTime nearThis = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string lastKey = null;
                foreach (string key in UnitPrices.Keys)
                {
                    DateTime currentDate = DateTime.ParseExact(key, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    var t = UnitPrices.Keys;

                    if (currentDate > nearThis)
                    {
                        result = UnitPrices[lastKey];
                        break;
                    }

                    lastKey = key;
                }
            }
            else
            {
                result = UnitPrices[date];
            }


            if (result <= 0.0m)
            {
                throw new Exception("No Unit Price Found");
            }

            //return UnitPrices[date];
            return result;
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
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(e.Data.ToString());
            }
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

            List<string> failedDownloads = new List<string>();


            // TODO //
            foreach (var symbol in symbols)
            //Parallel.ForEach(symbols, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, symbol =>
            {
                //try
                {
                    CollectChoosenData(symbol, tickPeriod, appendSymbols);
                }
                //catch
                {
                    //failedDownloads.Add(symbol);
                }
            }
            //);
            Thread.Sleep(15000);
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                foreach (string symbol in failedDownloads)
                {
                    try
                    {
                        CollectChoosenData(symbol, tickPeriod, appendSymbols);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                        Debug.WriteLine(e.Data.ToString());
                    }
                }
            }

            //Cleanup - Delete Files With Less Than 400 Rows
            Console.WriteLine("Done");
        }

        public static void CollectChoosenData(string symbol, TickPeriod tickPeriod, bool append)
        {
            CollectChoosenData(symbol, $"{GetFullPath(TickPeriod.Daily)}", tickPeriod, append);
        }

        public static void CollectChoosenData(string symbol, string folderPath, TickPeriod tickPeriod, bool append)
        {
            string url = null;
            string path = $"{folderPath}\\{symbol}.csv";

            // Get Information About Stock
            // https://api.iextrading.com/1.0/stock/aapl/quote
            //-----/ /-----/ /-----/ /-----/ SecretToken  

            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/5y?token={SecretToken}&format=csv";
                    break;

                case TickPeriod.SixMin:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/6m?token={SecretToken}&format=csv";
                    break;

                case TickPeriod.ThreeMin:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/3m?token={SecretToken}&format=csv";
                    break;

                case TickPeriod.OneMin:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/1m?token={SecretToken}&format=csv";
                    break;
                case TickPeriod.OneDay:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/1d?token={SecretToken}&format=csv";
                    break;
                case TickPeriod.Dynamic:
                    url = "https://cloud.iexapis.com/stable/stock/" + $"{symbol}/chart/dynamic?token={SecretToken}&format=csv";
                    break;
            }

            //Start Collecting
            ImportAndExport.SingleDataCollector(path, url, symbol, append);

            // https://api.iextrading.com/1.0/stock/market/batch?symbols=aapl,fb&types=quote,news,chart&range=1m&last=5

            // Daily 5 Years
            // url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/5y?format=csv";
            // SingleDataCollector(path, url);

            /*
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
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            

            try
            {
                Path.GetFullPath(path);
            }
            catch (System.ArgumentException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine(e.Data.ToString());
                Debug.WriteLine("Failed Path:" + path);
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
                        csvContent = Other.Download(url);

                        if (!string.IsNullOrWhiteSpace(csvContent))
                        {
                            hasCompleted = true;
                        }
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
                            Debug.WriteLine(e.ToString());
                            Debug.WriteLine(e.Data.ToString());
                            hasArgumentException = true;
                            Console.WriteLine(e.ToString());
                            Console.WriteLine(e.Data.ToString());

                            Console.WriteLine("Argument Invalied !!! - Press Enter To Continue");
                            Console.ReadLine();
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
                            string output = Other.Download(@"https://api.iextrading.com/1.0/stock/" + symbol + "/quote");
                            JObject jsonObj = JObject.Parse(output);

                            //                            string download = Other.Download(url);
                            //                           JObject jsonObj = JObject.Parse(download);
                            Dictionary<string, object> dictObj = jsonObj.ToObject<Dictionary<string, object>>();
                            decimal closePrice = -1;

                            /*
                            if(symbol.Equals("DXR"))
                            {
                                string symbol2 = "DXR";
                            }
                            */

                            Dictionary<string, string> testValues = new Dictionary<string, string>();

                            foreach (string header in headerRow)
                            {
                                bool isMatch = false;
                                foreach (string header2 in dictObj.Keys)
                                {
                                    if (((header.Equals(header2) && !header2.Equals("date") && !header2.Equals("close")) || (header.Equals("date") && header2.Equals("latestUpdate")) || (header.Equals("close") && header2.Equals("latestPrice"))) && dictObj[header2] != null)
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

                                        if (header2.Equals("latestUpdate"))
                                        {
                                            field = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(long.Parse(field)).ToString(@"yyyy-MM-dd HH:MM");
                                        }

                                        isMatch = true;
                                        csvWriter.WriteField(field);
                                        /*
                                        if (header2.Equals("close"))
                                        {
                                            closePrice = decimal.Parse(field);
                                        }
                                        

                                        if (header2.Equals("latestPrice"))
                                        {
                                            closePrice = decimal.Parse(field);
                                        }*/

                                        testValues[header] = field.ToString();
                                    }
                                }

                                if (!isMatch)
                                {
                                    csvWriter.WriteField("");
                                    testValues[header] = "";
                                }
                            }

                            //testValues = testValues;

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
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
                            Debug.WriteLine(e.Data.ToString());
                            newCsvContent += "-1" + ',';
                        }
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

            /* Legacy Download 
            CsvContainer CSV = Other.DownloadCSV("https://api.iextrading.com/1.0/ref-data/symbols?format=csv");

            //Filter Symbols
            List<string> filteredSymbols = new List<string>();
            
            for (int i = 0; i < CSV["symbol"].Count; i++)
            {
                //Crypto is Disabled
                if (SymbolFilter(CSV, i))
                {
                    filteredSymbols.Add(CSV["symbol"][i]);
                }
            }

            return filteredSymbols;
            */

            /* Legacy (simpel) - From https://www.home.saxo/da-dk/rates-and-conditions/equities-and-etfs/stocks-available */
            StreamReader streamReader = new StreamReader(@"C:\OneDrive\ALL STOCKS SAXO.txt");
            while (streamReader.Peek() >= 0)
            {
                symbols.Add(streamReader.ReadLine().Trim());
            }

            List<string> filteredSymbols = new List<string>();
            CsvContainer CSV = Other.DownloadCSV("https://api.iextrading.com/1.0/ref-data/symbols?format=csv");
            for (int i = 0; i < CSV["symbol"].Count; i++)
            {
                //Crypto is Disabled
                if (SymbolFilter(CSV, i))
                {
                    foreach (string symbol in symbols)
                    {
                        if (CSV["symbol"][i].CompareTo(symbol) == 0)
                        {
                            filteredSymbols.Add(symbol);
                            break;
                        }
                    }
                }
            }


            return filteredSymbols;
        }

        public static bool SymbolFilter(CsvContainer csvContainer, int index)
        {
            // CSV["symbol"][i]
            // Filter with saxo stocks/symbols compare name and price
            return !csvContainer["isEnabled"][index].Equals("false") && !csvContainer["type"][index].Equals("crypto");
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
