using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharpSolution.Model
{
    public class EmulationConnection : IConnection
    {
        private static int _GeneratedId = 0;
        private static int GeneratedId { get { return _GeneratedId++; } }
        private Dictionary<string, Order> Orders { get; set; }
        private decimal RemainingValue { get; set; }
        public decimal GetTotalValue() { return GetRemainingValue() + GetInvestedValue(); }
        private Portfolio Portfolio;
        public Portfolio GetPortfolio() { return Portfolio; }

        public EmulationConnection(decimal initialMoney, OrderLimitType orderLimitType, decimal orderLimit, int leverageLimit, int maxInvestedPct)
        {
            this.RemainingValue = initialMoney;
            this.Orders = new Dictionary<string, Order>();
            this.Portfolio = new Portfolio(this, orderLimitType, orderLimit, leverageLimit, maxInvestedPct);
        }

        public decimal GetRemainingValue()
        {
            return this.RemainingValue;
        }

        public decimal GetInvestedValue()
        {
            Dictionary<string, Order> orders = LoadOrders();
            decimal investedValue = 0;

            foreach(string securityID in orders.Keys)
            {
                investedValue += orders[securityID].NewestValue;
            }
            
            return investedValue;
        }

        public Dictionary<string, Order> LoadOrders()
        {
            return this.Orders;
        }

        public Order MakeOrder(string securityCode, Sides direction, int leverage, decimal piecePrice)
        {
            Order order = null;
            decimal payment = 0;

            if (OrderLimitType.Value == GetPortfolio().OrderLimitType)
            {
                payment = GetPortfolio().OrderLimit;
            }

            if (OrderLimitType.Procent == GetPortfolio().OrderLimitType)
            {
                payment = (GetPortfolio().TotalValue*GetPortfolio().OrderLimit)/100;
            }

            decimal newInvestPct = ((GetInvestedValue() + payment) / GetTotalValue()) * 100;

            if(newInvestPct < GetPortfolio().MaxInvestedPct)
            {
                RemainingValue -= payment;
                order = new Order(GeneratedId.ToString(), direction, securityCode,securityCode, leverage, payment, piecePrice);
                order.StartPieceValue = piecePrice;
                this.Orders[securityCode] = order;
            }

            Orders[securityCode] = order;

            return order;
        }

        public void CancelOrder(string securityCode, Sides direction, decimal piecePrice)
        {
            if(LoadOrders().ContainsKey(securityCode))
            {
                //this.Orders[securityCode].CurrentPieceValue = piecePrice;
                this.RemainingValue += this.Orders[securityCode].NewestValue;
                this.Orders.Remove(securityCode);
            }
        }
    }
}
