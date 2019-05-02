using AutomatedTradingV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services
{
    public static class CandleService 
    {
        public static DownloaderAbstract Downloader;

        public static void DownloadAddCandles(List<string> symbols)
        {
            bool isAddition = true;
            Downloader.DownloadCandles(symbols, isAddition);
        }

        public static void DownloadAllCandles()
        {
            Downloader.DownloadCandles();
        }

        /* Load Candles In Folder */
        public static List<SimpleCandle> LoadCandles()
        {
            return Downloader.LoadCandles();
        }
    }
}
