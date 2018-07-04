using MySql.Data.MySqlClient;
using Stocks.Export;
using Stocks.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using LumenWorks.Framework.IO.Csv;
using TickEnum;

namespace Stocks.Service
{
    public static class ImportAndExport
    {
        public static string PartialPath = @"C:\StockHistory\Active\";

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

        public static void CollectData(TickPeriod tickPeriod)
        {
            //string size = "full";
            Parallel.ForEach(GetSymbols(), symbol =>
            {
                CollectChoosenData(symbol, tickPeriod);
            });

            //Cleanup - Delete Files With Less Than 400 Rows
            Console.WriteLine("Done");
        }

        public static void CollectChoosenData(string symbol, TickPeriod tickPeriod)
        {
            string url = null;
            string path = $"{GetFullPath(TickPeriod.Daily)}{symbol}.csv";

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
                SingleDataCollector(path, url);

                // https://api.iextrading.com/1.0/stock/market/batch?symbols=aapl,fb&types=quote,news,chart&range=1m&last=5

                // Daily 5 Years
                // url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/5y?format=csv";
                // SingleDataCollector(path, url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SingleDataCollector(string path, string url)
        {
            //CSVToMySQL_Excel(url, path);
            bool hasCompleted = false;
            int count = 0;
            int tries = 4;
            while (!hasCompleted && count < tries)
            {
                try
                {
                    Other.DownloadFile(url, path);
                    hasCompleted = true;
                }
                catch
                {
                    count++;
                }
            }

            StreamReader streamReader = new StreamReader(path);
            CachedCsvReader csvReader = new CachedCsvReader(streamReader);

            int rows = 0;
            while (csvReader.ReadNextRecord())
            {
                rows++;

                if (rows >= 400)
                {
                    break;
                }
            }
            streamReader.Close();

            if (rows < 400)
            {
                File.Delete(path);
            }
        }

        public static List<string> GetSymbols()
        {
            List<string> symbols = new List<string>();

            StreamReader streamReader = new StreamReader(@"C:\StockHistory\StockList");
            while (streamReader.Peek() >= 0)
            {
                symbols.Add(streamReader.ReadLine().Trim());
            }

            return symbols;
        }

        public static string GetFullPath(TickPeriod tickPeriod)
        {
            string path = PartialPath;

            switch (tickPeriod)
            {
                case TickPeriod.Daily:
                    path += @"Daily\";
                    break;
                case TickPeriod.SixMin:
                    path += @"6Min\";
                    break;
                case TickPeriod.ThreeMin:
                    path += @"3Min\";
                    break;
                case TickPeriod.OneMin:
                    path += @"1Min\";
                    break;
            }

            return path;
        }
    }
}
