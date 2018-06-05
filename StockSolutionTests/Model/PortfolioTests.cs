using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockSolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Model.Tests
{
    [TestClass()]
    public class PortfolioTests
    {
        [TestMethod()]
        public void PortfolioTest()
        {
            EmulationConnection emulationConnection = new EmulationConnection(100000,OrderLimitType.Value, 10000, 1, 80);
            Portfolio portfolio = emulationConnection.GetPortfolio();
        }
    }
}