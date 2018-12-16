using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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
        public decimal LoseLimit { get; set; }
        public decimal LoseLimitMin { get; set; }
        public int OrdersCount { get; set; }

    public IndicatorPair(LengthIndicator shortIndicator, LengthIndicator longIndicator, decimal LoseLimit)
        {
            this.ShortIndicator = shortIndicator;
            this.LongIndicator = longIndicator;
            this.LoseLimit = LoseLimit;
            this.LastResult = decimal.MinValue;
            this.OrdersCount = 0;
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

        public override string ToString()
        {
            return ShortIndicator.ToString() + " - " + LongIndicator.ToString()  + " - " + (-LoseLimit).ToString(new CultureInfo("en-US"));
        }
    }
}
