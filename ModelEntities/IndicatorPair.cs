using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class IndicatorPair : IComparable<IndicatorPair>, IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual LengthIndicator ShortIndicator { get; set; }
        public virtual LengthIndicator LongIndicator { get; set; }
        public decimal LastResult { get; set; }
        public int Orders { get; set; }
        public int PositiveOrderPct { get; set; }
        public decimal LoseLimitMin { get; set; }
        public decimal LoseLimit { get; set; }

    public IndicatorPair(LengthIndicator shortIndicator, LengthIndicator longIndicator, 
        decimal loseLimit, decimal loseLimitMin)
        {
            this.ShortIndicator = shortIndicator;
            this.LongIndicator = longIndicator;
            this.LoseLimit = loseLimit;
            this.LastResult = decimal.MinValue;
            this.LoseLimitMin = loseLimitMin;
            this.PositiveOrderPct = int.MinValue;
            this.Orders = int.MinValue;
        }

        public int CompareTo(IndicatorPair other)
        {
            if(this.LastResult > other.LastResult)
            {
                return -1;
            }

            if (this.LastResult < other.LastResult)
            {
                return 1;
            } 

            return 0;
        }

        public int CompareTo(object other)
        {
            return this.CompareTo((IndicatorPair) other);
        }
    }
}
