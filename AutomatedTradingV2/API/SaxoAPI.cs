using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server

namespace AutomatedTradingV2.API
{
    public class SaxoAPI
    {
        public List<string> GenerateTokenRequest(l)
        {
            return "";
        }

        public class SymbolSaxo
        {
            public string Symbol { get; set; }

            public string FullName { get; set; }

            public SymbolSaxo(string symbol, string fullName)
            {
                int index = symbol.IndexOf(":");
                if (index > 0)
                {
                    this.Symbol = symbol.ToLower().Substring(0, index - 1);
                }
                else
                {
                    throw new Exception("Symbol Syntax Incorrect");
                }
                this.FullName = fullName.ToLower();
            }

            public bool IsSame(string symbolOrFullName)
            {
                if(this.Symbol.CompareTo(symbolOrFullName) == 0 || 
                    this.FullName.CompareTo(symbolOrFullName) == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
