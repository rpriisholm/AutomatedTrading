using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.ModelEntities.Models
{
    public class DbContextMSSQL : DbContext
    {
        public DbSet<Candle> Candles { get; set; }
        public DbSet<IConnection> Connections { get; set; }
        public DbSet<IndicatorPair> IndicatorPairs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<StrategyGeneric> StrategyGenerics { get; set; }
    }
}
