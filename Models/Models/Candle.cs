using Stocks.Service;
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
        public TimeSpan TimeFrame { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
        [Required]
        public DateTime OpenTime { get; set; }

        private decimal ClosePrice_;
        [Required]
        public decimal ClosePrice
        {
            get { return this.ClosePrice_; }
            set
            {
                this.ClosePrice_ = this.UnitPrice * value;
            }
        }
        /* In Wanted Currency, Only works when UnitPrice is set */

        private decimal OpenPrice_;
        [Required]
        public decimal OpenPrice {
            get { return this.OpenPrice_; }
            set
            {
                this.OpenPrice_ = this.UnitPrice * value;
            }
        }

        private decimal LowPrice_;
        public decimal LowPrice
        {
            get { return this.LowPrice_; }
            set
            {
                this.LowPrice_ = this.UnitPrice * value;
            }
        }

        private decimal HighPrice_;
        public decimal HighPrice
        {
            get { return this.HighPrice_; }
            set
            {
                this.HighPrice_ = this.UnitPrice * value;
            }
        }

        private decimal AvgPrice_;
        public decimal AvgPrice
        {
            get { return this.AvgPrice_; }
            set
            {
                this.AvgPrice_ = this.UnitPrice * value;
            }
        }

        private decimal TotalVolume
        {
            get;
            set;
        }

        /* Unconverted ex. US = 6.5 or EUR = 7.5 */
        [Required]
        public decimal UnitPrice
        {
            get
            {
                return ImportAndExport.GetUsdUnitPrice(CloseTime.ToString("dd-MM-yyyy"));
            }
        }

        public int CompareTo(object other)
        {
            return this.OpenTime.CompareTo(((Candle) other).OpenTime);
        }
    }
}
