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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual string SecurityID { get; set; }
        [Required]
        public TimeSpan TimeFrame { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
        [Required]
        public DateTime OpenTime { get; set; }
        [Required]
        public decimal ClosePrice { get; set; }
        [Required]
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
