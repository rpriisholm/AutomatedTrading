using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharpSolution.Model
{
    public abstract class StrategyBasic : IComparable<StrategyBasic>
    {
        public IConnection Connection { get; protected set; }
        public decimal CurrentPosition { get; set; }
        public LengthIndicator<decimal> LongIndicator { get; }
        public LengthIndicator<decimal> ShortIndicator { get; }
        public string SecurityID { get; }
        public decimal MarginSellPct { get; set; }
        public decimal MarginBuyPct { get; set; }
        protected bool _isRunning = false;
        protected bool _isShortLessThenLong;

        public StrategyBasic(IConnection connection, string securityID, LengthIndicator<decimal> longIndicator, LengthIndicator<decimal> shortIndicator, decimal marginSellPct, decimal marginBuyPct)
        {
            this.Connection = connection;
            this.SecurityID = securityID;
            this.LongIndicator = longIndicator;
            this.ShortIndicator = shortIndicator;
            this.MarginSellPct = marginSellPct;
            this.MarginBuyPct = marginBuyPct;
        }

        public void Start()
        {
            _isRunning = true;
            //_isShortLessThenLong = ShortIndicator.GetCurrentValue() < LongIndicator.GetCurrentValue();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public abstract void ProcessCandle(Candle candle);

        public abstract int CalcLeverage();

        public int CompareTo(StrategyBasic other)
        {
            return this.Connection.GetTotalValue().CompareTo(other.Connection.GetTotalValue());
        }
    }
}
