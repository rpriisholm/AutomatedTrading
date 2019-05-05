using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTradingV2.Custom;
using AutomatedTradingV2.Models;

namespace AutomatedTradingV2.Services.Downloaders
{
    public class DownloadIextrading : DownloadInterface
    {
        public EnumeratorOnDemand<SimpleCandle, Symbol> CreateCandleLoader(string partialPath)
        {
            throw new NotImplementedException();
        }

        public void DownloadNewCandles()
        {
            throw new NotImplementedException();
        }

        public void DownloadRowToCandles()
        {
            throw new NotImplementedException();
        }

        public List<Symbol> DownloadSymbols()
        {
            throw new NotImplementedException();
        }

        public List<SimpleCandle> LoadCandles()
        {
            throw new NotImplementedException();
        }
    }
}
