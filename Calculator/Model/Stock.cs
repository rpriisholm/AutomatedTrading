using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public class StockHist
    {
        public string Symbol
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int IntervalSec
        {
            get;
            set;
        }

        public Dictionary<DateTime, StockInfo> StockInfos
        {
            get;
            set;
        }

        private List<DateTime> _Keys;

        public List<DateTime> Keys
        {
            get
            {
                return _Keys;
            }

            set
            {
                _Keys = value;
                _Keys.Sort();
            }
        }

        public StockInfo this[DateTime key]
        {
            get
            {
                return StockInfos[key];
            }

            set
            {
                StockInfos[key] = value;
            }
        }
    }

    public class StockInfo
    {
        public double Low
        {
            get;
            set;
        }

        public double High
        {
            get;
            set;
        }

        public double Start
        {
            get;
            set;
        }

        public double End
        {
            get;
            set;
        }
    }
}
