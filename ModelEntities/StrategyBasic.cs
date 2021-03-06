﻿using SidesEnum;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public abstract class StrategyBasic : IComparable<StrategyBasic>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual AConnection Connection { get; set; }
        //public decimal CurrentPosition { get; set; }
        [Required]
        public virtual IndicatorPair IndicatorPair { get; set; }
        [Required]
        public SecurityInfo SecurityID { get; set; }
        protected bool _isRunning = false;
        protected bool _isShortLessThenLong;
        public bool IsSellEnabled { get; set; }
        public bool IsBuyEnabled { get; set; }
        public int OrderCount { get; set; }
        public int PositiveOrderCount { get; set; }
        public bool IsDisabled { get; set; }
        public decimal LoseLimitConstant { get; set; }
        public decimal LoseLimitMin = decimal.MaxValue;
        public DateTime LastExecution { get; set; }
        public bool IsStrategyExpiring = false;
        public bool IsActive = true;

        //Set when using OptimizerOptions
        public decimal LastTestResult = decimal.MinValue;

        public StrategyBasic(IConnection connection, SecurityInfo securityID, IndicatorPair indicatorPair, decimal loseLimitConstant)
        {
            this.Connection = connection as AConnection;
            this.SecurityID = securityID;
            this.IndicatorPair = indicatorPair;
            this.IsSellEnabled = false;
            this.IsBuyEnabled = true;
            this.Connection.InitializeSecurityID(securityID);
            this.IsDisabled = false;
            this.LoseLimitConstant = loseLimitConstant;
            this.LastExecution = DateTime.MinValue;
        }

        public StrategyBasic(IConnection connection, SecurityInfo securityID, IndicatorPair indicatorPair, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant) : this(connection, securityID, indicatorPair, loseLimitConstant)
        {
            this.IsSellEnabled = isSellEnabled;
            this.IsBuyEnabled = isBuyEnabled;
        }

        public decimal MaxLoseValue()
        {
            return this.Connection.CalcPayment() * this.LoseLimitConstant;
        }

        public void Start()
        {
            if (!IsDisabled)
            {
                _isRunning = true;

                //TODO - CHECK
                _isShortLessThenLong = IndicatorPair.ShortIndicator.GetCurrentValue() < IndicatorPair.LongIndicator.GetCurrentValue();

            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public int AllPositiveOrders()
        {
            if (Connection.LoadOrders().ContainsKey(this.SecurityID))
            {
                return PositiveOrderCount + 1;
            }

            return PositiveOrderCount;
        }

        public decimal AllPositiveOrdersPct()
        {
            if(OrderCount != 0)
            {
                return ((decimal)AllPositiveOrders()/ (decimal)this.OrderCount) * 100;
            } else
            {
                return 0m;
            }
        }

        public abstract void ProcessCandle(Candle candle);

        public abstract int CalcLeverage();

        public int CompareTo(StrategyBasic other)
        {
            return this.Connection.GetTotalValue().CompareTo(other.Connection.GetTotalValue());
        }

        public decimal ConnectionSecurityIDProfit()
        {
            decimal profit = Connection.Profit(this.SecurityID);
            decimal payment = this.Connection.CalcPayment();
            decimal currentLosePct = profit / payment;
            if (currentLosePct < LoseLimitMin)
            {
                this.LoseLimitMin = currentLosePct;
            }

            return Connection.Profit(this.SecurityID);
        }

        //IS Buy Or Sell Active
        public Sides GetDirection()
        {
            return (IndicatorPair.ShortIndicator.GetCurrentValue() < IndicatorPair.LongIndicator.GetCurrentValue()) ? Sides.Sell : Sides.Buy;
        }
    }
}
