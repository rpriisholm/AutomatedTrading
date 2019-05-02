using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Models
{
    public abstract class StrategyAbstract
    {
        //Portfolie or account (might me null), instead of connection. 
        //Positive Profit PCT
        public Symbol Symbol { get; set; }
        public DateTime LastExecution { get; set; }
        public LengthIndicator<decimal> ShortIndicator { get; set; }
        public LengthIndicator<decimal> LongIndicator { get; set; }
        public int? LoseLimit { get; set;}   
 
        public int OrderCount { get; set; }

        public decimal? StartPiecePrice { get; set; }
        public decimal? PiecePrice { get; set; }


        /* 1.00 = 100% */
        public decimal ProfitDecimalRelative
        {
            get
            {
                decimal? profitDecimalRelative = ProfitDecimal;

                if (StartPiecePrice != null)
                {
                    profitDecimalRelative += (this.PiecePrice - this.StartPiecePrice) / this.StartPiecePrice;
                }

                return (decimal) profitDecimalRelative;
            }
        }
        public decimal ProfitDecimal { get; set; }

        public void ProcessCandle(SimpleCandle candle)
        {
            this.PiecePrice = candle.LastPrice;
            this.LastExecution = candle.CloseTime;

            bool isBuy = this.ShortIndicator.GetCurrentValue() >= this.LongIndicator.GetCurrentValue();

            this.ShortIndicator.Process((decimal) this.PiecePrice);
            this.LongIndicator.Process((decimal) this.PiecePrice);
            bool isCurrentBuy = this.ShortIndicator.GetCurrentValue() >= this.LongIndicator.GetCurrentValue();

            if(isBuy != isCurrentBuy)
            {
                if (isCurrentBuy)
                {
                    Buy();
                    OrderCount += 1;
                }
                else
                {
                    Sell();
                }
            }
        }

        public void Buy()
        {
            this.StartPiecePrice = this.PiecePrice;
        }

        public void Sell()
        {
            if (StartPiecePrice != null)
            {
                this.ProfitDecimal += (decimal) ((this.PiecePrice - this.StartPiecePrice) / this.StartPiecePrice);
            }

            this.StartPiecePrice = null;
        }
    }
}
