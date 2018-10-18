using StockSolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcStrategies2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Empty Console  
            TextWriter outTextWriter = Console.Out;
            TextWriter errTextWriter = Console.Error;
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
            #endregion

            Optimizer optimizer = new Optimizer();
            optimizer.SimulationIndicatorPairsAndSave(16, 45, 2, 48, 2, false, true);

        }
    }
}
