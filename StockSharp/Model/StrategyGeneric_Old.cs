using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Strategies;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using StockSharp.BusinessEntities;

namespace StockSharp.Model
{
    public class StrategyGeneric_Old<T> : Strategy where T : LengthIndicator<decimal>
    {
        private readonly ICandleManager _candleManager;
        private readonly CandleSeries _series;
        private bool _isShortLessThenLong;
        private readonly StrategyParam<int> _longIndicatorPeriod;
        private readonly StrategyParam<int> _shortIndicatorPeriod;
        public int LongIndicatorPeriod => _longIndicatorPeriod.Value;
        public int ShortIndicatorPeriod => _shortIndicatorPeriod.Value;
        public T LongIndicator { get; }
        public T ShortIndicator { get; }
        public decimal PnL_Total { get; private set; }

        public StrategyGeneric_Old(ICandleManager candleManager, CandleSeries series, T longIndicator, T shortIndicator)
        {
            this._candleManager = candleManager;
            this._series = series;
            this.LongIndicator = longIndicator;
            this.ShortIndicator = shortIndicator;
            this._longIndicatorPeriod = this.Param("LongSmaPeriod", longIndicator.Length);
            this._shortIndicatorPeriod = this.Param("ShortSmaPeriod", shortIndicator.Length);
            this.PnL_Total = 0;

            this.PnLChanged += () =>
            {
                PnL_Total += this.PnL;
            };
        }

        protected override void OnStarted()
        {
            /*
            _candleManager
                .WhenCandlesFinished(_series)
                .Do(ProcessCandle)
                .Apply(this);

            this
                .GetCandleManager()
                .Start(_series);

            // store current values for short and long
            _isShortLessThenLong = ShortIndicator.GetCurrentValue() < LongIndicator.GetCurrentValue();

            base.OnStarted();
            */
        }

        private void ProcessCandle(Candle candle)
        {
            // strategy are stopping
            if (ProcessState == ProcessStates.Stopping)
            {
                CancelActiveOrders();
                return;
            }

            // process new candle
            LongIndicator.Process(candle);
            ShortIndicator.Process(candle);

            // calc new values for short and long
            var isShortLessThenLong = ShortIndicator.GetCurrentValue() < LongIndicator.GetCurrentValue();

            // crossing happened
            if (_isShortLessThenLong != isShortLessThenLong)
            {
                // if short less than long, the sale, otherwise buy
                var direction = isShortLessThenLong ? Sides.Sell : Sides.Buy;

                // calc size for open position or revert
                var volume = Position == 0 ? Volume : Position.Abs() * 2;

                // register order (limit order)
                RegisterOrder(this.CreateOrder(direction, (decimal)(Security.GetCurrentPrice(this, direction) ?? 0), volume));

                // or revert position via market quoting
                //var strategy = new MarketQuotingStrategy(direction, volume);
                //ChildStrategies.Add(strategy);

                // store current values for short and long
                _isShortLessThenLong = isShortLessThenLong;
            }
        }

        public override void RegisterOrder(StockSharp.BusinessEntities.Order order)
        {
            base.RegisterOrder(order);
        }
    }
}
