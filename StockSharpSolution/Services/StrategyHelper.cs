using System;
using System.Collections.Generic;
using System.Text;
using Ecng.Common;
using StockSharp.Algo.Indicators;
using StockSharp.Messages;
using StockSharpSolution.Model;

namespace StockSharpSolution.Services
{
    public static class StrategyHelper
    {
        /*
         
        /// <summary>
        /// To create the initialized order object for buy.
        /// </summary>
        /// <param name="strategy">Strategy.</param>
        /// <param name="price">Price.</param>
        /// <param name="volume">The volume. If <see langword="null" /> value is passed, then <see cref="Strategy.Volume"> value is used.
        /// <returns>The initialized order object.</returns>
        /// <remarks>
        /// The order is not registered, only the object is created.
        /// </remarks>
        public static Order BuyAtLimit(this StrategyBasic<LengthIndicator<decimal>> strategy, decimal price)
        {
            return CreateOrder(strategy, Sides.Buy, price);
        }

        /// <summary>
        /// To create the initialized order object for sell.
        /// </summary>
        /// <param name="strategy">Strategy.</param>
        /// <param name="price">Price.</param>
        /// <param name="volume">The volume. If <see langword="null" /> value is passed, then <see cref="Strategy.Volume"> value is used.
        /// <returns>The initialized order object.</returns>
        /// <remarks>
        /// The order is not registered, only the object is created.
        /// </remarks>
        public static Order SellAtLimit(this StrategyBasic<LengthIndicator<decimal>> strategy, decimal price)
        {
            return CreateOrder(strategy, Sides.Sell, price);
        }

        /// <summary>
        /// To create the initialized order object.
        /// </summary>
        /// <param name="strategy">Strategy.</param>
        /// <param name="direction">Order side.</param>
        /// <param name="price">The price. If <see langword="null" /> value is passed, the order is registered at market price.
        /// <param name="volume">The volume. If <see langword="null" /> value is passed, then <see cref="Strategy.Volume"> value is used.
        /// <returns>The initialized order object.</returns>
        /// <remarks>
        /// The order is not registered, only the object is created.
        /// </remarks>
        public static Order CreateOrder(this StrategyBasic<LengthIndicator<decimal>> strategy, Sides direction, decimal payment)
        {
            return strategy.Connection.MakeOrder(strategy.SecurityID, direction, strategy.CalcLeverage(), payment);
        }
        */
    }
}
