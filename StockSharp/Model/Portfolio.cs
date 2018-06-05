using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharp.Model
{
    public enum OrderLimitType
    {
        Procent,
        Value
    }

    public class Portfolio
    {
        private IConnection Connection { get; set; }
        public decimal TotalValue {get {return this.MoneyCurrent + this.InvestedMoney;}}
        public decimal MoneyCurrent { get { return Connection.GetRemainingValue(); } }
        public decimal InvestedMoney { get { return Connection.GetInvestedValue(); } }
        public OrderLimitType OrderLimitType { get; set; }
        public decimal OrderLimit { get; set; }
        public int LeverageLimit { get; set; }
        public int MaxInvestedPct { get; set; }
        public decimal InitialValue { get; set; }  

        public Portfolio(IConnection connection, OrderLimitType orderLimitType, decimal orderLimit, int leverageLimit, int maxInvestedPct)
        {
            this.Connection = connection;
            this.OrderLimitType = orderLimitType;
            this.OrderLimit = orderLimit;
            this.LeverageLimit = leverageLimit;
            this.MaxInvestedPct = maxInvestedPct;
        }

        public List<Order> Orders
        {
            get
            {
                List<Order> orders = new List<Order>();
                Connection.LoadOrders();
                return orders;
            }
        }
    }
}
