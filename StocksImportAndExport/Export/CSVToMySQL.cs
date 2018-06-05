using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Stocks.Export
{
    public static class CSVToMySQL
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
        private static Dictionary<string, int> Indices = null;

        /*OLD*
        public static async void ExportToMySQL(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions);
            StreamReader reader = new StreamReader(fileStream);
            char csvSeperator = ',';

            string line = reader.ReadLine();
            string[] columns = line.Split(csvSeperator);
            Dictionary<string, int> columnIndices = GetColumnIndices(columns);

            List<string> lines = new List<string>();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                lines.Add(line); //Or send to MySQL / Create a insert statment
            }
        }
        */

        public static void ExportToMySQL(StreamReader streamReader, int nrOfLines, int intervalSec, string symbol)
        {
            char csvSeperator = ',';

            string line = streamReader.ReadLine();
            string[] columns = line.Split(csvSeperator);
            Dictionary<string, int> columnIndices = GetColumnIndices(columns);
            string insertMySQL = @"INSERT INTO stocksdb.stockdb(FKInterval,FKStockInfo,StartTime,Start,End,High,Low) VALUES";
            string dateTimeFormat = null;

            if(intervalSec >= 86400)
            {
                dateTimeFormat = "'%Y-%m-%d'";
            } else
            {
                dateTimeFormat = "'%Y-%m-%d %H %i %S'";
            }

            int counter = 0;
            while ((line = streamReader.ReadLine()) != null && counter < nrOfLines)
            {
               
                columns = line.Split(csvSeperator);
                insertMySQL += "("+ intervalSec+ ',' + "'" + symbol + "'" + ',' + "STR_TO_DATE('" + columns[columnIndices["StartTime"]] + "', " + dateTimeFormat + ")" + ',' + columns[columnIndices["Start"]] + ',' + columns[columnIndices["End"]] + ',' + columns[columnIndices["High"]] + ',' + columns[columnIndices["Low"]] + "),";
                counter++;
            }

            insertMySQL = insertMySQL.TrimEnd(',') + ";";


            DB_Connector_MySQL mySQL = DB_Connector_MySQL.GetStocksConnector();
            MySqlConnection conn = mySQL.Connection;

            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(insertMySQL, conn);
                command.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            
        }

        public static void ExportToMySQL(StreamReader streamReader, int intervalSec, string symbol)
        {
            ExportToMySQL(streamReader, int.MaxValue, intervalSec, symbol);
        }

        public static EventWaitHandle ExportLineToMySQL(Task<string> getLine, Dictionary<string, int> columnIndices)
        {
            EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);

            Thread exportThread = new Thread(() =>
            {
                try
                {
                    getLine.Start();
                    string line = getLine.Result;
                    string[] values = line.Split(',');
                }
                finally
                {
                    handle.Set();
                }
            });
            exportThread.Start();

            return handle;
        }

        private static Dictionary<string, int> GetColumnIndices(string[] columns)
        {
            if (Indices != null)
            {
                //Do Nothing
            }
            else
            {
                Indices = new Dictionary<string, int>();

                for (int i = 0; i < columns.Length; i++)
                {
                    string value = columns[i];

                    switch (value)
                    {
                        case "timestamp":
                            Indices["StartTime"] = i;
                            break;
                        case "open":
                            Indices["Start"] = i;
                            break;
                        case "high":
                            Indices["High"] = i;
                            break;
                        case "low":
                            Indices["Low"] = i;
                            break;
                        case "close":
                            Indices["End"] = i;
                            break;
                    }
                }
            }

            return Indices;
        }
    }
}
