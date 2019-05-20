using AutomatedTradingV2.Custom;
using AutomatedTradingV2.Models;
using AutomatedTradingV2.Services.Downloaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services
{
    public class DownloadService
    {
        public static DownloadInterface Downloader;

        public static void DownloadAddCandles(List<Symbol> symbols)
        {
            bool isAddition = true;
            //Downloader.DownloadCandles(symbols, isAddition);
        }

        /* Load Candles In Folder */
        public static EnumeratorOnDemand<SimpleCandle, Symbol> CreateCandleLoader(string partialPath)
        {
            return Downloader.CreateCandleLoader(partialPath);
        }
    }
}
