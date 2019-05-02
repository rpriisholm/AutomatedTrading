using AutomatedTradingV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services
{
    public abstract class DownloaderAbstract
    {
        public string PartialPath { set; get; }

        public abstract List<string> DownloadSymbols();

        public void DownloadCandles()
        {
            DownloadCandles(DownloadSymbols(), false);
        }

        /* Addition Means Add Row Instead Of Downloading All From Scratch */ 
        public abstract void DownloadCandles(List<string> symbols, bool isAddition);

        public abstract List<SimpleCandle> LoadCandles();
    }
}
