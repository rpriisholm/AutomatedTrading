using System;
using System.Collections.Generic;
using System.Text;
using StockSharpSolution.Model;

namespace StockSharpSolution.Model
{
    public class Emulator
    {
        public void Start()
        {
            decimal initialMoney = 100000;
            IConnection connection = new EmulationConnecton(initialMoney, OrderLimitType.Value,10000,1,80);
            connection.GetPortfolio().InitialValue = initialMoney;

        }
    }
}
