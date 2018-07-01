using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public interface IConnection
    {
        Portfolio GetPortfolio();
        decimal GetRemainingValue();
        decimal GetInvestedValue();
        decimal GetTotalValue();
        Dictionary<string, decimal> RealizedProfits();
        Dictionary<string, decimal> AlltimeRealizedProfits();
        decimal Profit(string securityID);
        Dictionary<string, Order> LoadOrders();
        Order MakeOrder(string securityCode, Sides direction, int leverage, decimal piecePrice);
        Order CancelOrder(string securityCode, Sides direction, decimal piecePrice);
        void InitializeSecurityID(string securityID);
        decimal CalcPayment();
        //bool CancelOrder();
    }
}
