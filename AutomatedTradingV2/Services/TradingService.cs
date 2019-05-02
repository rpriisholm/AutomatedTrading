using AutomatedTradingV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services
{
    public static class TradingService
    {
        

        public static void FindTradingStrategies()
        {
            DownloadService.DownloadAllCandles();
            DownloadService.Downloader.PartialPath;

            List<SimpleCandle> candles = DownloadService.LoadCandles();


        }

        public static void ContinueTrading()
        {

        }

        public static void SimulateTrading()
        {

        }
    }
}
