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
            string size = "full";
            string partialPath = @"C:\StockHistory\Active\";
            foreach (string symbol in GetSymbols())
            {
                string url = "https://www.alphavantage.co/query?" + $"function=TIME_SERIES_DAILY&symbol={symbol}&outputsize={size}&apikey=11EJMYL88Q4DW5BT&datatype=csv";
                string path = $"{partialPath}{symbol}.csv";

                //CSVToMySQL_Excel(url, path);
                //Other.DownloadFile(url, path);

                //StreamReader streamReader = Other.GetStreamReader(url);
                //CSVToMySQL.ExportToMySQL(streamReader, 86400, symbol);
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static List<string> GetSymbols()
        {
            List<string> symbols = new List<string>();

            StreamReader streamReader = new StreamReader(@"C:\StockHistory\StockList");
            while(streamReader.Peek() >= 0)
            {
                symbols.Add(streamReader.ReadLine());
            }

            return symbols;
        }
    }
}
