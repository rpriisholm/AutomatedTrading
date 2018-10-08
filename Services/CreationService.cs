using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using StockSharp.ITCH;

namespace StockSolution.Services
{
    public class CreationService
    {
        public static HistoryEmulationConnector CreateHistoryEmulationConnector(StorageRegistry storageRegistry, Security security, Portfolio portfolio, TimeSpan timeFrame, DateTime startTime, DateTime stopTime)
        {
            HistoryEmulationConnector connector = new HistoryEmulationConnector(new[] { security }, new[] { portfolio }, storageRegistry)
            {
                EmulationAdapter =
                {
                    Emulator =
                    {
                        Settings =
                        {
                            // match order if historical price touched our limit order price. 
                            // It is terned off, and price should go through limit order price level
                            // (more "severe" test mode)
                            MatchOnTouch = false,

                        }
                    }
                },

                //UseExternalCandleSource = emulationInfo.UseCandleTimeFrame != null,

                //CreateDepthFromOrdersLog = true,
                //CreateTradesFromOrdersLog = true,

                HistoryMessageAdapter =
                {
                    StorageRegistry = storageRegistry,

                    // set history range
                    StartDate = startTime,
                    StopDate = stopTime,

                    //Is this nessary 
                    OrderLogMarketDepthBuilders =
                    {
                        {
                            security.ToSecurityId(),(IOrderLogMarketDepthBuilder)new ItchOrderLogMarketDepthBuilder(security.ToSecurityId())
                        }
                    }
                },

                // set market time freq as time frame
                MarketTimeChangedInterval = timeFrame,
            };

            return connector;
        }
    }
}
