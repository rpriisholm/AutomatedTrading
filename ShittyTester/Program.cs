using Stocks.Service;
using System;
using static Stocks.Import.Other;

namespace ShittyTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            decimal unitPrice = ImportAndExport.GetUsdUnitPrice("01-01-2019");
            Console.WriteLine(unitPrice.ToString());

            string[] headers = new string[2];
            Console.WriteLine(unitPrice);


            Console.ReadLine();
        }
    }
}
