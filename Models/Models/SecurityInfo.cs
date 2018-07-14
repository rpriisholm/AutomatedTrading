using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Entity.Models
{
    public class SecurityInfo : IComparable, IComparable<string>
    {
        [Key]
        public string SecurityID { get; set; }

        public List<Candle> Candles { get; set; }

        public SecurityInfo()
        {
            this.Candles = new List<Candle>();
        }

        public override string ToString()
        {
            return this.SecurityID;
        }

        public override bool Equals(object obj)
        {
            return this.SecurityID.Equals(((SecurityInfo) obj).SecurityID);
        }

        public override int GetHashCode()
        {
            return this.SecurityID.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return this.SecurityID.CompareTo(((SecurityInfo) obj).SecurityID);
        }
        

        public int CompareTo(string other)
        {
            return this.SecurityID.CompareTo(other.ToString());
        }


        public bool Equals(string other)
        {
            return this.SecurityID.Equals(other);
        }
    }
}
