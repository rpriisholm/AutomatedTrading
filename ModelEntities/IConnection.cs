using SidesEnum;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public interface IConnection
    {
        Portfolio GetPortfolio();
        decimal GetRemainingValue();
        decimal GetInvestedValue();
        decimal GetTotalValue();
        Dictionary<SecurityInfo, decimal> RealizedProfits();
        Dictionary<SecurityInfo, decimal> AlltimeRealizedProfits();
        decimal Profit(SecurityInfo securityID);
        Dictionary<SecurityInfo, Order> LoadOrders();
        Order MakeOrder(SecurityInfo securityCode, Sides direction, int leverage, decimal piecePrice);
        Order CancelOrder(SecurityInfo securityCode, Sides direction, decimal piecePrice);
        void InitializeSecurityID(SecurityInfo securityID);
        decimal CalcPayment();
        //bool CancelOrder();
    }
}
