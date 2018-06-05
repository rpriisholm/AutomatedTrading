using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockSharp.Messages;
using StockSolution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.Model.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void Order_1Leverage()
        {
            Order orderBuy = new Order("Test", Sides.Buy, "Test Code", "Test Name", 1, 1000m, 2m);
            Order orderSell = new Order("Test", Sides.Sell, "Test Code", "Test Name", 1, 1000m, 2m);

            Assert.IsTrue(1000m == orderBuy.NewestValue);
            Assert.IsTrue(0m == orderBuy.Profit);
            Assert.IsTrue(1000m == orderSell.NewestValue);
            Assert.IsTrue(0m == orderSell.Profit);

            orderBuy.CurrentPieceValue = 1m;
            orderSell.CurrentPieceValue = 1m;
            Assert.IsTrue(500m == orderBuy.NewestValue);
            Assert.IsTrue(-500m == orderBuy.Profit);
            Assert.IsTrue(1500m == orderSell.NewestValue);
            Assert.IsTrue(500m == orderSell.Profit);

            orderBuy.CurrentPieceValue = 4m;
            orderSell.CurrentPieceValue = 4m;
            Assert.IsTrue(2000m == orderBuy.NewestValue);
            Assert.IsTrue(1000m == orderBuy.Profit);
            Assert.IsTrue(0m == orderSell.NewestValue);
            Assert.IsTrue(-1000m == orderSell.Profit);

            Assert.IsTrue(0 == orderBuy.LoanedValue);
            Assert.IsTrue(0 == orderSell.LoanedValue);
        }

        [TestMethod()]
        public void Order_2Leverage()
        {
            Order orderBuy = new Order("Test", Sides.Buy, "Test Code", "Test Name", 2, 1000m, 2m);
            Order orderSell = new Order("Test", Sides.Sell, "Test Code", "Test Name", 2, 1000m, 2m);

            Assert.IsTrue(1000m == orderBuy.NewestValue);
            Assert.IsTrue(0m == orderBuy.Profit);
            Assert.IsTrue(1000m == orderSell.NewestValue);
            Assert.IsTrue(0m == orderSell.Profit);

            orderBuy.CurrentPieceValue = 1m;
            orderSell.CurrentPieceValue = 1m;
            Assert.IsTrue(0m == orderBuy.NewestValue);
            Assert.IsTrue(-1000m == orderBuy.Profit);
            Assert.IsTrue(2000m == orderSell.NewestValue);
            Assert.IsTrue(1000m == orderSell.Profit);

            orderBuy.CurrentPieceValue = 4m;
            orderSell.CurrentPieceValue = 4m;
            Assert.IsTrue(3000m == orderBuy.NewestValue);
            Assert.IsTrue(2000m == orderBuy.Profit);
            Assert.IsTrue(-1000m == orderSell.NewestValue);
            Assert.IsTrue(-2000m == orderSell.Profit);

            Assert.IsTrue(1000 == orderBuy.LoanedValue);
            Assert.IsTrue(1000 == orderSell.LoanedValue);
        }
    }
}