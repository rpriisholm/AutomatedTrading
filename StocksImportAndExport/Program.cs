﻿using Stocks.Import;
using Stocks.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Services.Collector_DailyFull();
            //Services.Collector_DailyCompact();

            ImportAndExport.CollectDailyData();
        }
    }
}