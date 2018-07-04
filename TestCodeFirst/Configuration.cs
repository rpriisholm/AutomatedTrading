using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace TestCodeFirst
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbContextMSSQL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DbContextMSSQL context)
        {
            // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application
        }
    }
}
