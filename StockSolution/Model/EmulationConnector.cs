using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public class EmulationConnection : IConnection
    {
        private static int _GeneratedId = 0;
        private static int GeneratedId { get { return _GeneratedId++; } }
        private Dictionary<string, Order> Orders { get; set; }
        private Dictionary<string, decimal> Profits { get; set; }
        private decimal RemainingValue { get; set; }
        public decimal GetTotalValue() { return GetRemainingValue() + GetInvestedValue(); }
        private Portfolio Portfolio;
        public Portfolio GetPortfolio() { return Portfolio; }

        public EmulationConnection(decimal initialMoney, OrderLimitType orderLimitType, decimal orderLimit, int leverageLimit, int maxInvestedPct)
        {
            this.RemainingValue = initialMoney;
            this.Orders = new Dictionary<string, Order>();
            this.Portfolio = new Portfolio(this, orderLimitType, orderLimit, leverageLimit, maxInvestedPct);
            this.Profits = new Dictionary<string, decimal>();
        }

        public decimal GetRemainingValue()
        {
            return this.RemainingValue;
        }

        public decimal GetInvestedValue()
        {
            Dictionary<string, Order> orders = LoadOrders();
            decimal investedValue = 0;

            foreach (string securityID in orders.Keys)
            {
                if (orders[securityID] != null)
                {
                    investedValue += orders[securityID].NewestValue;
                }
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
            decimal payment = CalcPayment();

            decimal newInvestPct = ((GetInvestedValue() + payment) / GetTotalValue()) * 100;

            if (newInvestPct < GetPortfolio().MaxInvestedPct)
            {
                RemainingValue -= payment;
                order = new Order(GeneratedId.ToString(), direction, securityCode, securityCode, leverage, payment, piecePrice);
                order.StartPieceValue = piecePrice;
                this.Orders[securityCode] = order;
            }

            Orders[securityCode] = order;
            return order;
        }

        public Order CancelOrder(string securityCode, Sides direction, decimal piecePrice)
        {
            Order order = null;
            if (LoadOrders().ContainsKey(securityCode))
            {
                if (this.Orders[securityCode] != null)
                {
                    if (this.Orders[securityCode].Direction == direction)
                    {
                        this.Orders[securityCode].CurrentPieceValue = piecePrice;
                        this.RemainingValue += this.Orders[securityCode].NewestValue;
                        order = Orders[securityCode];
                        RealizedProfits()[securityCode] += order.Profit;
                        this.Orders.Remove(securityCode);
                    }
                }
            }
            return order;
        }

        public Dictionary<string, decimal> RealizedProfits()
        {
            return Profits;
        }

        public decimal Profit(string securityID)
        {
            decimal profit = 0;
            if(LoadOrders().ContainsKey(securityID) && LoadOrders()[securityID] != null)
            {
                profit += LoadOrders()[securityID].Profit;        
            }
            profit += RealizedProfits()[securityID];

            return profit;
        }

        public decimal CalcPayment()
        {
            decimal payment = 0m;

            if (OrderLimitType.Value == GetPortfolio().OrderLimitType)
            {
                payment = GetPortfolio().OrderLimit;
            }

            if (OrderLimitType.Procent == GetPortfolio().OrderLimitType)
            {
                payment = (GetPortfolio().TotalValue * GetPortfolio().OrderLimit) / 100;
            }

            return payment;
        }

        public void InitializeSecurityID(string securityID)
        {
            if(!LoadOrders().ContainsKey(securityID))
            {
                LoadOrders()[securityID] = null;
            }

            if(!RealizedProfits().ContainsKey(securityID))
            {
                RealizedProfits()[securityID] = 0m;
            }
        }
    }
}
