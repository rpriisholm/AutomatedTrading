using SidesEnum;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class EmulationConnection : AConnection, IConnection
    {
        public virtual Dictionary<SecurityInfo, decimal> Profits { get; set; }
        public virtual Dictionary<SecurityInfo, decimal> AlltimeProfits { get; set; }
        public decimal RemainingValue { get; set; }

        public EmulationConnection()
        {
            this.AlltimeProfits = new Dictionary<SecurityInfo, decimal>();
            this.Profits = new Dictionary<SecurityInfo, decimal>();
            this.Orders = new Dictionary<SecurityInfo, Order>();
        }

        public EmulationConnection(decimal initialMoney, OrderLimitType orderLimitType, decimal orderLimit, int leverageLimit, int maxInvestedPct) : this()
        {
            this.RemainingValue = initialMoney;
            
            this.Portfolio = new Portfolio(this, orderLimitType, orderLimit, leverageLimit, maxInvestedPct);         
        }

        public override decimal GetRemainingValue()
        {
            return this.RemainingValue;
        }

        public override decimal GetInvestedValue()
        {
            Dictionary<SecurityInfo, Order> orders = LoadOrders();
            decimal investedValue = 0;

            foreach (SecurityInfo securityID in orders.Keys)
            {
                if (orders[securityID] != null)
                {
                    investedValue += orders[securityID].NewestValue;
                }
            }

            return investedValue;
        }

        public override Order MakeOrder(SecurityInfo securityCode, Sides direction, int leverage, decimal piecePrice)
        {
            Order order = null;
            decimal payment = CalcPayment();

            decimal newInvestPct = ((GetInvestedValue() + payment) / GetTotalValue()) * 100;

            if (newInvestPct < GetPortfolio().MaxInvestedPct)
            {
                RemainingValue -= payment;
                order = new Order(direction, securityCode, leverage, payment, piecePrice);
                order.StartPieceValue = piecePrice;
                this.Orders[securityCode] = order;
            }

            Orders[securityCode] = order;
            return order;
        }

        public override Order CancelOrder(SecurityInfo securityCode, Sides direction, decimal piecePrice)
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
                        AlltimeRealizedProfits()[securityCode] += order.Profit;
                        this.Orders.Remove(securityCode);
                    }
                }
            }
            return order;
        }

        public override Dictionary<SecurityInfo, decimal> RealizedProfits()
        {
            return Profits;
        }

        public override Dictionary<SecurityInfo, decimal> AlltimeRealizedProfits()
        {
            return Profits;
        }

        public override decimal Profit(SecurityInfo securityID)
        {
            decimal profit = 0;
            if(LoadOrders().ContainsKey(securityID) && LoadOrders()[securityID] != null)
            {
                profit += LoadOrders()[securityID].Profit;        
            }
            profit += RealizedProfits()[securityID];

            return profit;
        }

        public override decimal CalcPayment()
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

        public override void InitializeSecurityID(SecurityInfo securityID)
        {
            if(!LoadOrders().ContainsKey(securityID))
            {
                LoadOrders()[securityID] = null;
            }

            if(!RealizedProfits().ContainsKey(securityID))
            {
                RealizedProfits()[securityID] = 0m;
            }

            if(!AlltimeRealizedProfits().ContainsKey(securityID))
            {
                AlltimeRealizedProfits()[securityID] = 0m;
            }
        }
    }
}
