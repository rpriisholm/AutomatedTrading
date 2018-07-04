using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class IndicatorPair : IComparable<IndicatorPair>, IComparable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public LengthIndicator ShortIndicator;
        public LengthIndicator LongIndicator;
        public decimal LastResult { get; set; }
        public int Orders { get; set; }
        public int PositiveOrderPct { get; set; }

        public IndicatorPair(LengthIndicator shortIndicator, LengthIndicator longIndicator)
        {
            this.ShortIndicator = shortIndicator;
            this.LongIndicator = longIndicator;
            this.LastResult = decimal.MinValue;
            this.Orders = int.MinValue;
            this.PositiveOrderPct = int.MinValue;
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
