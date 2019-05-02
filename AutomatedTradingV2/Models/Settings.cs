using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Models
{
    public class Settings
    {
        public DateTime? StrategiesCreatedAt { get; set; }

        public int ExpiriationAfterXTicks { get; set; }

        public Dictionary<Symbol, StrategyAbstract> Strategies {get;set;}

        public Settings(DateTime? strategiesCreatedAt, Dictionary<Symbol, StrategyAbstract> strategies, int expiriationAfterXTicks)
        {
            this.StrategiesCreatedAt = strategiesCreatedAt ?? throw new ArgumentNullException(nameof(strategiesCreatedAt));
            this.Strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
            this.ExpiriationAfterXTicks = expiriationAfterXTicks;
        }
    }
}
