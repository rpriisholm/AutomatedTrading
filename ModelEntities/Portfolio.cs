using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public enum OrderLimitType
    {
        Procent,
        Value
    }

    public class Portfolio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        private IConnection Connection { get; set; }
        public decimal TotalValue {get {return this.MoneyCurrent + this.InvestedMoney;}}
        public decimal MoneyCurrent { get { return Connection.GetRemainingValue(); } }
        public decimal InvestedMoney { get { return Connection.GetInvestedValue(); } }
        public OrderLimitType OrderLimitType { get; set; }
        public decimal OrderLimit { get; set; }
        private decimal OrderLimitValue
        {
            get
            {
                if(OrderLimitType.Value == OrderLimitType)
                {
                    return this.OrderLimit;
                }
                
                if(OrderLimitType.Procent == OrderLimitType)
                {
                    return this.TotalValue * this.OrderLimit/100;
                }

                throw new NotSupportedException();
            }     
        }
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
