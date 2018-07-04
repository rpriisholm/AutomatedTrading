using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace TestCodeFirstEntity
{
    public class DbContextMSSQL
    {
        public DbSet<TestDB> Candles { get; set; }
    }
}
