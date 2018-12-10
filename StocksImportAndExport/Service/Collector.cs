using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql;
//using MySql.Data.MySqlClient;
using System.Globalization;
using Stocks.Import;
using System.Threading;

namespace Stocks
{
    public static partial class Services
    {
        public enum ColloctorType
        {
            Full, Compact
        }
        /*
        public static void Collector_DailyCompact()
        {
            Collector_Daily(ColloctorType.Compact);
        }

        public static void Collector_DailyFull()
        {
            Collector_Daily(ColloctorType.Full);
        }
        */

        //GENERATE LIST TO COLLECT
        //FIND LAST DAILY DATE In Database
        //EXTRACT NEWER DATES
        /*
        public static void Collector_Daily(ColloctorType type)
        {
            Dictionary<string, List<string>> symbols = GetSymbols();
            int interval = 60 * 60 * 24;

            foreach (string symbol in symbols["Symbol"])
            {
                string typeString = "";
                if (ColloctorType.Compact == type)
                {
                    typeString = "compact";
                }

                if (ColloctorType.Full == type)
                {
                    typeString = "full";
                }




                int numOfThreads = 8;
                int currentNumOfThreads = 0;
                WaitHandle[] waitHandles = new WaitHandle[numOfThreads];

                string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=" + symbol + "&outputsize=" + typeString + "&apikey=11EJMYL88Q4DW5BT&datatype=csv";
                if (url != null)
                {
                    EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                    Thread collector = new Thread(
                        () => {
                            try
                            {
                                Collector(symbol, interval, url);
                            }
                            finally
                            {
                                handle.Set();
                            }
                        }
                    );
                    collector.Start();

                    if(numOfThreads <= currentNumOfThreads)
                    {
                        currentNumOfThreads = 0;
                    }
                    else
                    {
                        waitHandles[currentNumOfThreads] = handle;
                        waitHandles = new WaitHandle[numOfThreads];
                        currentNumOfThreads = 0;
                    }
                }
            }

        }
        
        public static void Collector(string symbol, int interval, string url)
        {
            DB_Connector_MySQL connector = DB_Connector_MySQL.GetStocksConnector();
            MySqlConnection conn = connector.Connection;

            try
            {
                Dictionary<string, List<string>> stocks = CSV.SplitCSV(url);

                if (stocks.Keys.Count >= 5)
                {
                    try
                    {
                        string query = "SELECT COUNT(*) FROM stockinfodb WHERE Symbol like '" + symbol + "'";
                        conn.Open();
                        MySqlCommand command = new MySqlCommand(query, conn);
                        var row = command.ExecuteScalar();

                        if (int.Parse(row.ToString()) == 0)
                        {
                            //Insert row in table stockinfodb
                            query = "INSERT INTO stockinfodb(Symbol,Name) VALUES('" + symbol + "','" + "Name Place Holder" + "');";

                            command = new MySqlCommand(query, conn);
                            command.ExecuteNonQuery();
                        }

                        query = "SELECT COUNT(*) FROM intervaldb WHERE IntervalSec=" + interval;

                        command = new MySqlCommand(query, conn);
                        row = command.ExecuteScalar();

                        if (int.Parse(row.ToString()) == 0)
                        {
                            //Insert row in table intervaldb
                            query = "INSERT INTO intervaldb(IntervalSec) VALUES(" + interval + ");";

                            command = new MySqlCommand(query, conn);
                            command.ExecuteNonQuery();
                        }

                        //Insert rows in table stockdb
                        query = "INSERT INTO stockdb(FKInterval,FKStockInfo,StartTime,EndTime,Start,End,Low,High) VALUES";
                        for (int i = 0; i < stocks["open"].Count; i++)
                        {
                            DateTime startTimeD = DateTime.Parse(stocks["timestamp"][i]);
                            string startTime = "'" + startTimeD.ToString("yyyy-MM-dd H:mm:ss") + "'";
                            string endTime = "'" + startTimeD.AddSeconds(interval).ToString("yyyy-MM-dd H:mm:ss") + "'";
                            query += "(" + interval + ",'" + symbol + "'," + startTime + "," + endTime + "," + stocks["open"][i] + "," + stocks["close"][i] + "," + stocks["low"][i] + "," + stocks["high"][i] + ")";

                            if (i < stocks["open"].Count - 1)
                            {
                                query += ",";
                            }
                            else
                            {
                                query += ";";
                            }

                        }
                        command = new MySqlCommand(query, conn);
                        command.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            catch { }
        }
        */

        private static string MySQL_DateTimeString(DateTime dateTime)
        {
            return "'" + dateTime.ToString("yyyy-MM-dd H:mm:ss") + "'";
        }

        private static string MySQL_DoubleString(double value)
        {
            return value.ToString(new CultureInfo("en-US"));
        }

        private static Dictionary<string, List<string>> GetSymbols()
        {
            return CSV.SplitCSV("http://www.nasdaq.com/screening/companies-by-name.aspx?exchange=NASDAQ&render=download");
        }
    }
}
