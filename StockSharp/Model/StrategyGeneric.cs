using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharp.Model
{
    public class StrategyGeneric : StrategyBasic, IComparable
    {
        public StrategyGeneric(IConnection connection, string securityID, LengthIndicator<decimal> longIndicator, LengthIndicator<decimal> shortIndicator, decimal marginSellPct, decimal marginBuyPct) : base(connection, securityID, longIndicator, shortIndicator, marginSellPct, marginBuyPct)
        {

        }

        public override void ProcessCandle(Candle candle)
        {
            if (!_isRunning)
            {
                throw new Exception("Strategy Haven't Been Started");
            }

            //Set Indicator Values
            decimal shortValue = ShortIndicator.GetCurrentValue();
            decimal longValue = ShortIndicator.GetCurrentValue();

            // store current values for short and long
            _isShortLessThenLong = shortValue < longValue;

            //Load candles
            LongIndicator.Process(candle.ClosePrice, true);
            ShortIndicator.Process(candle.ClosePrice, true);

            //Set Indicator Values
            shortValue = ShortIndicator.GetCurrentValue();
            longValue = ShortIndicator.GetCurrentValue();

            // calc new values for short and long
            bool isShortLessThenLong = shortValue < longValue;

            // crossing happened
            if (_isShortLessThenLong != isShortLessThenLong)
            {
                if (Connection.LoadOrders().ContainsKey(SecurityID))
                {
                    Connection.LoadOrders()[SecurityID].CurrentPieceValue = candle.ClosePrice;
                }

                // if short less than long, the sale, otherwise buy
                Sides direction = isShortLessThenLong ? Sides.Sell : Sides.Buy;

                // calc size for open position or revert
                //var volume = Position == 0 ? Volume : Position.Abs() * 2;

                /*
                 * Calc Limit or do it in strategy
                Portfolio portfolio = strategy.Connection.GetPortfolio();
                portfolio.LeverageLimit;
                */

                decimal pieceValueShort = ShortIndicator.GetCurrentValue();
                decimal pieceValueLong = LongIndicator.GetCurrentValue();

                try
                {
                    decimal differenceBuyPct = (pieceValueShort / pieceValueLong) * 100 - 100;
                    decimal differenceSellPct = 100 - (pieceValueShort / pieceValueLong) * 100;

                    // Buy Order
                    if (this.MarginBuyPct < differenceBuyPct && direction == Sides.Buy)
                    {
                        Connection.CancelOrder(SecurityID, Sides.Buy, candle.ClosePrice);
                    }

                    // Sell Order
                    if (this.MarginSellPct < differenceSellPct && direction == Sides.Sell)
                    {
                        Connection.CancelOrder(SecurityID, Sides.Sell, candle.ClosePrice);
                    }

                    // Register order (limit order)
                    Connection.MakeOrder(SecurityID, direction, CalcLeverage(), candle.ClosePrice);

                    // Store current values for short and long
                    _isShortLessThenLong = isShortLessThenLong;
                }
                catch { }
            }
        }

        public override int CalcLeverage()
        {
            int defaultLeverage = 1;
            return defaultLeverage;
        }

        public int CompareTo(Object other)
        {
            return base.CompareTo((StrategyBasic) other);               
        }
    }
}
