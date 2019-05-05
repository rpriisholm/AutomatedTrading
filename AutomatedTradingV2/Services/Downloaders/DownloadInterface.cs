using AutomatedTradingV2.Custom;
using AutomatedTradingV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services.Downloaders
{
    public interface DownloadInterface
    {
        List<Symbol> DownloadSymbols();

        void DownloadRowToCandles();

        void DownloadNewCandles();

        /* Addition Means Add Row Instead Of Downloading All From Scratch */
        EnumeratorOnDemand<SimpleCandle, Symbol> CreateCandleLoader(string partialPath);

        List<SimpleCandle> LoadCandles();
    }
}
