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

namespace Stocks.Service
{
    public static class ImportAndExport
    {
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

        public static void CollectDailyData()
        {           
            //string size = "full";
            string partialPath = @"C:\StockHistory\Active\";
           
            #region ForEach normal
            /*
            foreach (string symbol in GetSymbols())
            {
                //string url = "https://www.alphavantage.co/query?" + $"function=TIME_SERIES_DAILY&symbol={symbol}&outputsize={size}&apikey=11EJMYL88Q4DW5BT&datatype=csv";
                string url = "https://api.iextrading.com/1.0/stock/"+ $"{symbol}/chart/5y?format=csv";
                string path = $"{partialPath}{symbol}.csv";

                //CSVToMySQL_Excel(url, path);
                Other.DownloadFile(url, path);

                //StreamReader streamReader = Other.GetStreamReader(url);
                //CSVToMySQL.ExportToMySQL(streamReader, 86400, symbol);

                Thread.Sleep(25);
            }
            */
            #endregion

            Parallel.ForEach(GetSymbols(), new ParallelOptions { MaxDegreeOfParallelism = 16 }, symbol => 
            {
                try
                {
                    //string url = "https://www.alphavantage.co/query?" + $"function=TIME_SERIES_DAILY&symbol={symbol}&outputsize={size}&apikey=11EJMYL88Q4DW5BT&datatype=csv";
                    string url = "https://api.iextrading.com/1.0/stock/" + $"{symbol}/chart/5y?format=csv";
                    string path = $"{partialPath}{symbol}.csv";


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

                    //StreamReader streamReader = Other.GetStreamReader(url);
                    //CSVToMySQL.ExportToMySQL(streamReader, 86400, symbol);

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
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            });

            //Cleanup - Delete Files With Less Than 400 Rows
           

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static List<string> GetSymbols()
        {
            List<string> symbols = new List<string>();

            StreamReader streamReader = new StreamReader(@"C:\StockHistory\StockList");
            while(streamReader.Peek() >= 0)
            {
                symbols.Add(streamReader.ReadLine().Trim());
            }

            return symbols;
        }
    }
}
