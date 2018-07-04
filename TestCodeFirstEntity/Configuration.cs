using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Text;

namespace TestCodeFirstEntity
{
    internal sealed class Configuration : DbMigrationsConfiguration<TestDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestDB context)
        {
            // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application
        }
    }
}
