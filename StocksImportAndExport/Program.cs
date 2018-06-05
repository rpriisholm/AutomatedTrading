using Stocks.Import;
using Stocks.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stocks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Services.Collector_DailyFull();
            //Services.Collector_DailyCompact();

            ImportAndExport.CollectDailyData();
        }

        private static void DummyCodeMultiThreading()
        {
            int numOfThreads = 32;
            int currentNumOfThreads = 0;
            WaitHandle[] waitHandles = new WaitHandle[numOfThreads];

            while (false /*Needs to be replaced */)
            {
                EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);

                Thread exportThread = new Thread(() =>
                {
                    try
                    {
                        // Code
                    }
                    finally
                    {
                        handle.Set();
                    }
                });
                exportThread.Start();

                if (numOfThreads <= currentNumOfThreads)
                {
                    waitHandles[currentNumOfThreads] = handle;
                    currentNumOfThreads += 1;
                }
                else
                {
                    waitHandles[currentNumOfThreads] = handle;
                    while (WaitHandle.WaitAll(waitHandles, 10))
                    {
                        //Do nothing
                    }

                    waitHandles = new WaitHandle[numOfThreads];
                    currentNumOfThreads = 0;
                }
            }
        }
    }
}
