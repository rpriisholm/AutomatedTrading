using SidesEnum;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class StrategyGeneric : StrategyBasic, IComparable
    {
        public StrategyGeneric(IConnection connection, SecurityInfo securityID, IndicatorPair indicatorPair, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant) : base(connection, securityID, indicatorPair, isSellEnabled, isBuyEnabled, loseLimitConstant)
        {
            if (!(indicatorPair != null))
            {
                try
                {
                    throw new ArgumentNullException();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Data.ToString());
                    throw e;
                }
            }
        }

        public StrategyGeneric(IConnection connection, SecurityInfo securityID, OptimizerOptions optimizerOptions, decimal loseLimit) :
            //base(connection, securityID, optimizerOptions.BestIndicatorPair.LongIndicator, optimizerOptions.BestIndicatorPair.ShortIndicator, optimizerOptions.IsSellEnabled, optimizerOptions.IsBuyEnabled, optimizerOptions.LoseLimit)
            base(connection, securityID, optimizerOptions.BestIndicatorPair, OptimizerOptions.IsSellEnabled, OptimizerOptions.IsBuyEnabled, loseLimit)
        {
            if(!(this.IndicatorPair != null))
            {
                try
                {
                    throw new ArgumentNullException();
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.Data.ToString());
                    throw e;
                }
            }

            this.LastTestResult = optimizerOptions.BestIndicatorPair.LastResult;
            //optimizerOptions.BestIndicatorPair.ShortIndicator.Candles = securityID.Candles;
            //optimizerOptions.BestIndicatorPair.LongIndicator.Candles = securityID.Candles;
            this.LastExecution = securityID.Candles[securityID.Candles.Count - 1].CloseTime;
        }

        public override void ProcessCandle(Candle candle)
        {
            if (!_isRunning)
            {
                throw new Exception("Strategy Haven't Been Started");
            }

            this.LastExecution = candle.CloseTime;

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
                IndicatorPair.LongIndicator.Process(candle.ClosePrice, true);
                IndicatorPair.ShortIndicator.Process(candle.ClosePrice, true);

                //Set Indicator Values
                decimal shortValue = IndicatorPair.ShortIndicator.GetCurrentValue();
                decimal longValue = IndicatorPair.LongIndicator.GetCurrentValue();

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
                }
                else
                {

                    // crossing happened
                    if (_isShortLessThenLong != isShortLessThenLong && !this.IsDisabled)
                    {
                        // if short less than long, the sale, otherwise buy
                        Sides direction = isShortLessThenLong ? Sides.Sell : Sides.Buy;

                        decimal pieceValueShort = IndicatorPair.ShortIndicator.GetCurrentValue();
                        decimal pieceValueLong = IndicatorPair.LongIndicator.GetCurrentValue();

                        if (pieceValueShort != 0 && pieceValueLong != 0)
                        {
                            //decimal differenceBuyPct = (pieceValueShort / pieceValueLong) * 100 - 100;
                            //decimal differenceSellPct = 100 - (pieceValueShort / pieceValueLong) * 100;

                            //Sell Orders - Margin?
                            closeOrderSell = Connection.CancelOrder(SecurityID, Sides.Sell, candle.ClosePrice);
                            closeOrderBuy  = Connection.CancelOrder(SecurityID, Sides.Buy, candle.ClosePrice);

                            if (this.IsStrategyExpiring == false)
                            {
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
                            else
                            {
                                this.IsDisabled = true;
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
