using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.Entity.Models
{
    public class Candle : IComparable
    {
        [Key]
        public string SecurityID { get; set; }
        public TimeSpan TimeFrame { get; set; }
        public DateTime CloseTime { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal TotalVolume {get;set;}
        public decimal AvgPrice { get; set; }

        public int CompareTo(object other)
        {
            return this.OpenTime.CompareTo(((Candle) other).OpenTime);
        }
    }
}
