using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace TEMP_CODEFIRST
{
    public class DbContextMSSQL : DbContext
    {
        public DbSet<TestDB> Test { get; set; }
    }
}
