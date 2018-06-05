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
    public class EmulationConnectionTests
    {
        [TestMethod()]
        public void EmulationConnectorBuy_1LeverageTest()
        {
            EmulationConnection emulationConnection = new EmulationConnection(100000, OrderLimitType.Value, 10000, 2, 20);
            emulationConnection.InitializeSecurityID("TestOrder");
            emulationConnection.MakeOrder("TestOrder",Sides.Buy, 1, 2);
            
            Assert.IsTrue(100000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(10000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(0m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 1;
            Assert.IsTrue(95000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(5000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(-5000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 4;
            Assert.IsTrue(110000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(20000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(10000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(90000m == emulationConnection.GetRemainingValue());

            emulationConnection.CancelOrder("TestOrder", Sides.Buy, 8);
            Assert.IsTrue(130000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(30000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(30000m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(130000m == emulationConnection.GetRemainingValue());
        }

        [TestMethod()]
        public void EmulationConnectorBuy_2LeverageTest()
        {
            EmulationConnection emulationConnection = new EmulationConnection(100000, OrderLimitType.Value, 10000, 2, 20);
            emulationConnection.InitializeSecurityID("TestOrder");
            emulationConnection.MakeOrder("TestOrder", Sides.Buy, 2, 2);

            Assert.IsTrue(100000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(10000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(0m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 1;
            Assert.IsTrue(90000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(-10000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 4;
            Assert.IsTrue(120000m == emulationConnection.GetTotalValue());
            //40000 Real - 10000 Loaned
            Assert.IsTrue(30000m == emulationConnection.GetInvestedValue());
            //40000 Real - 10000 Loaned - 10000 Start
            Assert.IsTrue(20000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(90000m == emulationConnection.GetRemainingValue());

            emulationConnection.CancelOrder("TestOrder", Sides.Buy, 8);
            Assert.IsTrue(160000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            //80000 Real - 10000 Loaned - 10000 Start
            Assert.IsTrue(60000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(60000m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(160000m == emulationConnection.GetRemainingValue());
        }

        [TestMethod()]
        public void EmulationConnectorSell_1LeverageTest()
        {
            EmulationConnection emulationConnection = new EmulationConnection(100000, OrderLimitType.Value, 10000, 2, 20);
            emulationConnection.InitializeSecurityID("TestOrder");
            emulationConnection.MakeOrder("TestOrder", Sides.Sell, 1, 2);

            Assert.IsTrue(100000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(10000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(0m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 3;
            Assert.IsTrue(95000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(5000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(-5000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 1m;
            Assert.IsTrue(105000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(15000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(5000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(90000m == emulationConnection.GetRemainingValue());

            emulationConnection.CancelOrder("TestOrder", Sides.Sell, 0m);
            Assert.IsTrue(110000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(10000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(10000m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(110000m == emulationConnection.GetRemainingValue());
        }

        [TestMethod()]
        public void EmulationConnectorSell_2LeverageTest()
        {
            EmulationConnection emulationConnection = new EmulationConnection(100000, OrderLimitType.Value, 10000, 2, 20);
            emulationConnection.InitializeSecurityID("TestOrder");
            emulationConnection.MakeOrder("TestOrder", Sides.Sell, 2, 2);

            Assert.IsTrue(100000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(10000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(0m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 3;
            Assert.IsTrue(90000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(-10000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);

            emulationConnection.LoadOrders()["TestOrder"].CurrentPieceValue = 1m;
            Assert.IsTrue(110000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(20000m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(10000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(0m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(90000m == emulationConnection.GetRemainingValue());

            emulationConnection.CancelOrder("TestOrder", Sides.Sell, 0m);
            Assert.IsTrue(120000m == emulationConnection.GetTotalValue());
            Assert.IsTrue(0m == emulationConnection.GetInvestedValue());
            Assert.IsTrue(20000m == emulationConnection.Profit("TestOrder"));
            Assert.IsTrue(20000m == emulationConnection.RealizedProfits()["TestOrder"]);
            Assert.IsTrue(120000m == emulationConnection.GetRemainingValue());
        }
    }
}