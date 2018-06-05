using StockSharp.Algo.Indicators;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public class StrategyGeneric : StrategyBasic, IComparable
    {
        public StrategyGeneric(IConnection connection, string securityID, LengthIndicator<decimal> longIndicator, LengthIndicator<decimal> shortIndicator, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant) : base(connection, securityID, longIndicator, shortIndicator, isSellEnabled, isBuyEnabled, loseLimitConstant)
        {

        }

        public StrategyGeneric(IConnection connection, string securityID, OptimizerOptions optimizerOptions) : base(connection, securityID, optimizerOptions.BestIndicatorPair.LongIndicator, optimizerOptions.BestIndicatorPair.ShortIndicator, optimizerOptions.IsSellEnabled, optimizerOptions.IsBuyEnabled, optimizerOptions.LoseLimitConstant)
        {

        }

        public override void ProcessCandle(Candle candle)
        {
            if (!_isRunning)
            {
                throw new Exception("Strategy Haven't Been Started");
            }

            if (!this.IsDisabled)
            {
                Order closeOrderSell = null;
                Order closeOrderBuy = null;

                if (Connection.LoadOrders().ContainsKey(SecurityID))
                {
                    //Change Price On Piece If Order Exists
                    if (Connection.LoadOrders()[SecurityID] != null)
                    {
                        Connection.LoadOrders()[SecurityID].CurrentPieceValue = candle.ClosePrice;
                    }
                }

                //Load candles
                LongIndicator.Process(candle.ClosePrice, true);
                ShortIndicator.Process(candle.ClosePrice, true);

                //Set Indicator Values
                decimal shortValue = ShortIndicator.GetCurrentValue();
                decimal longValue = LongIndicator.GetCurrentValue();

                // calc new values for short and long
                bool isShortLessThenLong = shortValue < longValue;

                decimal connectionSecurityIDProfit = ConnectionSecurityIDProfit();
                decimal portfolioMaxLoseValue = MaxLoseValue();

                //BAD Strategy
                if (connectionSecurityIDProfit < portfolioMaxLoseValue || this.IsDisabled)
                {
                    this.IsDisabled = true;
                    closeOrderSell = Connection.CancelOrder(SecurityID, Sides.Sell, candle.ClosePrice);
                    closeOrderBuy  = Connection.CancelOrder(SecurityID, Sides.Buy, candle.ClosePrice);
                    /*
                    if (IsSellEnabled)
                    {
                        Connection.CancelOrder(SecurityID, Sides.Sell, candle.ClosePrice);
                    }
                    if (IsBuyEnabled)
                    {
                        Connection.CancelOrder(SecurityID, Sides.Buy, candle.ClosePrice);
                    }
                    */
                }
                else
                {

                    // crossing happened
                    if (_isShortLessThenLong != isShortLessThenLong && !this.IsDisabled)
                    {
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

                        if (pieceValueShort != 0 && pieceValueLong != 0)
                        {
                            //decimal differenceBuyPct = (pieceValueShort / pieceValueLong) * 100 - 100;
                            //decimal differenceSellPct = 100 - (pieceValueShort / pieceValueLong) * 100;

                            //Sell Orders - Margin?
                            closeOrderSell = Connection.CancelOrder(SecurityID, Sides.Sell, candle.ClosePrice);
                            closeOrderBuy  = Connection.CancelOrder(SecurityID, Sides.Buy, candle.ClosePrice);


                            // Buy Order
                            if (direction == Sides.Buy)
                            {
                                if (IsBuyEnabled)
                                {
                                    Order order = Connection.MakeOrder(SecurityID, Sides.Buy, CalcLeverage(), candle.ClosePrice);
                                    if (order != null)
                                    {
                                        this.OrderCount++;
                                    }
                                }

                                // Store current values for short and long
                                _isShortLessThenLong = isShortLessThenLong;
                            }

                            // Sell Order
                            if (direction == Sides.Sell)
                            {
                                if (IsSellEnabled)
                                {
                                    Order order = Connection.MakeOrder(SecurityID, Sides.Sell, CalcLeverage(), candle.ClosePrice);
                                    if (order != null)
                                    {
                                        this.OrderCount++;
                                    }
                                }

                                // Store current values for short and long
                                _isShortLessThenLong = isShortLessThenLong;
                            }
                        }
                    }
                }

                if(closeOrderSell != null)
                {
                    if (closeOrderSell.Profit > 0)
                    {
                        this.PositiveOrderCount += 1;
                    }
                }

                if (closeOrderBuy != null)
                {
                    if (closeOrderBuy.Profit > 0)
                    {
                        this.PositiveOrderCount += 1;
                    }
                }
            }
        }

        public override int CalcLeverage()
        {
            int defaultLeverage = 1;
            return defaultLeverage;
        }

        public int CompareTo(Object other)
        {
            return -base.CompareTo((StrategyBasic)other);
        }
    }
}
