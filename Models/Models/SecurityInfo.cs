using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Entity.Models
{
    public class SecurityInfo : IComparable, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
    {
        [Key]
        public string SecurityID { get; set; }

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
        
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(string other)
        {
            return this.SecurityID.CompareTo(other.ToString());
        }

        public IEnumerator GetEnumerator()
        {
            return this.SecurityID.GetEnumerator();
        }

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return this.SecurityID.GetEnumerator();
        }

        public bool Equals(string other)
        {
            return this.SecurityID.Equals(other);
        }
    }
}
