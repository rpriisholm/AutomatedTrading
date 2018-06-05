using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public abstract class StrategyBasic : IComparable<StrategyBasic>
    {
        public IConnection Connection { get; protected set; }
        //public decimal CurrentPosition { get; set; }
        public LengthIndicator<decimal> LongIndicator { get; }
        public LengthIndicator<decimal> ShortIndicator { get; }
        public string SecurityID { get; }
        protected bool _isRunning = false;
        protected bool _isShortLessThenLong;
        public bool IsSellEnabled { get; set; }
        public bool IsBuyEnabled { get; set; }
        public int OrderCount { get; set; }
        public int PositiveOrderCount { get; set; }
        public bool IsDisabled { get; set; }
        public decimal LoseLimitConstant { get; set; }

        public StrategyBasic(IConnection connection, string securityID, LengthIndicator<decimal> longIndicator, LengthIndicator<decimal> shortIndicator, decimal loseLimitConstant)
        {
            this.Connection = connection;
            this.SecurityID = securityID;
            this.LongIndicator = longIndicator;
            this.ShortIndicator = shortIndicator;
            this.IsSellEnabled = false;
            this.IsBuyEnabled = true;
            this.Connection.InitializeSecurityID(securityID);
            this.IsDisabled = false;
            this.LoseLimitConstant = loseLimitConstant;
        }

        public StrategyBasic(IConnection connection, string securityID, LengthIndicator<decimal> longIndicator, LengthIndicator<decimal> shortIndicator, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant) : this(connection, securityID, longIndicator, shortIndicator, loseLimitConstant)
        {
            this.IsSellEnabled = isSellEnabled;
            this.IsBuyEnabled = isBuyEnabled;
        }

        public decimal MaxLoseValue()
        {
            return this.Connection.CalcPayment() * this.LoseLimitConstant;
        }

        public void Start()
        {
            _isRunning = true;

            //TODO - CHECK
            _isShortLessThenLong = ShortIndicator.GetCurrentValue() < LongIndicator.GetCurrentValue();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public int AllPositiveOrders()
        {
            if (Connection.LoadOrders().ContainsKey(this.SecurityID))
            {
                return PositiveOrderCount + 1;
            }

            return PositiveOrderCount;
        }

        public decimal AllPositiveOrdersPct()
        {
            if(OrderCount != 0)
            {
                return ((decimal)AllPositiveOrders()/ (decimal)this.OrderCount) * 100;
            } else
            {
                return 0m;
            }
        }

        public abstract void ProcessCandle(Candle candle);

        public abstract int CalcLeverage();

        public int CompareTo(StrategyBasic other)
        {
            return this.Connection.GetTotalValue().CompareTo(other.Connection.GetTotalValue());
        }

        public decimal ConnectionSecurityIDProfit()
        {
            return Connection.Profit(this.SecurityID);
        }
    }
}
