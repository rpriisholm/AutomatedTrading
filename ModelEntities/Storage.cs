using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class Storage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string StoragePath { get; set; }
        public List<Candle> Candles { get; set; }

        public List<Candle> LoadCandles(DateTime startTime, DateTime stopTime)
        {
            List<Candle> candles = new List<Candle>();
            return null;
        }
    }
}
