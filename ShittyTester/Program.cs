using Stocks.Service;
using System;
using static Stocks.Import.Other;

namespace ShittyTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CsvContainer csv = ImportAndExport.GetUnitPrice();
            Console.WriteLine(csv.ToString());

            string[] headers = new string[2];
            int count = 0;
            for (int i = 0; i < csv.Headers.Count; i++)
            {
                headers[i] = csv.Headers[i];
                count = csv[headers[i]].Count;

                if(i != 0)
                {
                    Console.Write(" - ");
                }
                Console.Write(headers[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < csv[csv.Headers[0]].Count; i++)
            {
                string s = "";
                foreach(string header in headers)
                {
                    s += csv[header][i] + " - ";
                }

                Console.WriteLine(s.Trim().TrimEnd('-'));
            }

            Console.ReadLine();
        }
    }
}
