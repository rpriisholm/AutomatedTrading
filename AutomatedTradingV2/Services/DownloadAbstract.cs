using AutomatedTradingV2.Custom;
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

        public abstract List<Symbol> DownloadSymbols();

        public void DownloadCandles()
        {
            DownloadCandles(DownloadSymbols(), false);
        }

        /* Addition Means Add Row Instead Of Downloading All From Scratch */
        public abstract EnumeratorOnDemand<SimpleCandle, Symbol> CreateCandleLoader(string partialPath);

        public abstract List<SimpleCandle> LoadCandles();
    }
}
