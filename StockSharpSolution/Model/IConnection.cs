using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharpSolution.Model
{
    public interface IConnection
    {
        Portfolio GetPortfolio();
        decimal GetRemainingValue();
        decimal GetInvestedValue();
        decimal GetTotalValue();
        Dictionary<string, Order> LoadOrders();
        Order MakeOrder(string securityCode, Sides direction, int leverage, decimal piecePrice);
        void CancelOrder(string securityCode, Sides direction, decimal piecePrice);
        //bool CancelOrder();
    }
}
