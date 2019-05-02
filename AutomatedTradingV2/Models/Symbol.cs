using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Models
{
    public class Symbol
    {
        public readonly string Id;
        public readonly string FullName;
        public decimal? LastValue { get; set; }

        public Symbol(string id, string fullName, decimal? lastValue)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            LastValue = lastValue ?? throw new ArgumentNullException(nameof(lastValue));
        }
    }
}
