using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace TestCodeFirst
{
    public class DbContextMSSQL : DbContext
    {
        public DbSet<TestDB> Candles { get; set; }
    }
}
