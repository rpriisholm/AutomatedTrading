﻿using StockSolution.Entity.Models;
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
        public virtual LengthIndicator LongIndicator { get; set; }
        public virtual LengthIndicator ShortIndicator { get; set; }
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

        public StrategyBasic(IConnection connection, SecurityInfo securityID, LengthIndicator longIndicator, LengthIndicator shortIndicator, decimal loseLimitConstant)
        {
            this.Connection = connection as AConnection;
            this.SecurityID = securityID;
            this.LongIndicator = longIndicator;
            this.ShortIndicator = shortIndicator;
            this.IsSellEnabled = false;
            this.IsBuyEnabled = true;
            this.Connection.InitializeSecurityID(securityID);
            this.IsDisabled = false;
            this.LoseLimitConstant = loseLimitConstant;
        }

        public StrategyBasic(IConnection connection, SecurityInfo securityID, LengthIndicator longIndicator, LengthIndicator shortIndicator, bool isSellEnabled, bool isBuyEnabled, decimal loseLimitConstant) : this(connection, securityID, longIndicator, shortIndicator, loseLimitConstant)
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
            _isRunning = true;

            //TODO - CHECK
            _isShortLessThenLong = ShortIndicator.GetCurrentValue() < LongIndicator.GetCurrentValue();
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
            return Connection.Profit(this.SecurityID);
        }
    }
}