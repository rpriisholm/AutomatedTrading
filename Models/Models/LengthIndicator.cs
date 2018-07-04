using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StockSharp.Algo.Indicators;

namespace StockSolution.Entity.Models
{
    public class LengthIndicator
    {
        public static readonly Dictionary<string, Type> IndicatorNames = new Dictionary<string, Type>()
        {
            { (new SimpleMovingAverage()).ToString(), (new SimpleMovingAverage()).GetType()},
            { (new ExponentialMovingAverage()).ToString(), (new ExponentialMovingAverage()).GetType()},
            { (new Highest()).ToString(), (new Highest()).GetType()},
            { (new HullMovingAverage()).ToString(), (new HullMovingAverage()).GetType()},
            { (new JurikMovingAverage()).ToString(), (new JurikMovingAverage()).GetType()},
            { (new KaufmannAdaptiveMovingAverage()).ToString(), (new KaufmannAdaptiveMovingAverage()).GetType()},
            { (new LinearReg()).ToString(), (new Lowest()).GetType()},
            { (new Lowest()).ToString(), (new Lowest()).GetType()},
            { (new MeanDeviation()).ToString(), (new MeanDeviation()).GetType()},
            { (new Momentum()).ToString(), (new Momentum()).GetType()}
            
            /**  Other Types - Might Need extra things to work  
                baseIndicators[i].Add(new LinearRegSlope() { Length = i });
                baseIndicators[i].Add(new IchimokuChinkouLine() { Length = i });
                baseIndicators[i].Add(new DoubleExponentialMovingAverage() { Length = i });
                baseIndicators[i].Add(new DetrendedPriceOscillator() { Length = i });
                baseIndicators[i].Add(new CommodityChannelIndex() { Length = i });
                baseIndicators[i].Add(new ChandeMomentumOscillator() { Length = i });
                baseIndicators[i].Add(new AlligatorLine() { Length = i });
                baseIndicators[i].Add(new MoneyFlowIndex() { Length = i });
                baseIndicators[i].Add(new NickRypockTrailingReverse() { Length = i });
                baseIndicators[i].Add(new OptimalTracking() { Length = i });
                baseIndicators[i].Add(new RelativeStrengthIndex() { Length = i });
                baseIndicators[i].Add(new RelativeVigorIndexAverage() { Length = i });
                baseIndicators[i].Add(new RelativeVigorIndexSignal() { Length = i });
                baseIndicators[i].Add(new RSquared() { Length = i });
                baseIndicators[i].Add(new StandardDeviation() { Length = i });
                baseIndicators[i].Add(new StandardError() { Length = i });
                baseIndicators[i].Add(new StochasticK() { Length = i });
                baseIndicators[i].Add(new Sum() { Length = i });
                baseIndicators[i].Add(new TripleExponentialMovingAverage() { Length = i });
                baseIndicators[i].Add(new VerticalHorizontalFilter() { Length = i });
                baseIndicators[i].Add(new Vidya() { Length = i });
                baseIndicators[i].Add(new VolumeWeightedMovingAverage() { Length = i });
                baseIndicators[i].Add(new WeightedMovingAverage() { Length = i });
                baseIndicators[i].Add(new WilliamsR() { Length = i });
            */
    };


        public List<Candle> Candles { get; set; }

        [NotMapped]
        public LengthIndicator<decimal> Indicator { get; set; }
        
        public string IndicatorAdapted
        {
            get
            {
                string result = null;
                if(Indicator != null)
                {
                    result = Indicator.ToString();
                }
                return result;
            }

            set
            {
                LengthIndicator<decimal> indicator = null;

                string type = value.Split(' ')[0];
                int length = int.Parse(value.Split(' ')[1]);

                foreach(string key in IndicatorNames.Keys)
                {
                    if(key == type)
                    {
                        indicator = Activator.CreateInstance(IndicatorNames[key]) as LengthIndicator<decimal>;
                        indicator.Length = length;
                    }
                }

                this.Indicator = indicator;
            }
        }

        public LengthIndicator()
        {
            this.Candles = new List<Candle>();
        }

        public LengthIndicator(LengthIndicator<decimal> indicator) : this()
        {
            this.Indicator = indicator;
        }

        public LengthIndicator(IList<Candle> candles, LengthIndicator<decimal> indicator) : this(indicator)
        {
            foreach(var candle in candles)
            {
                this.Candles.Add(candle);
                this.Process(candle.ClosePrice, true);
            }
        }

        public void Process(decimal price, bool isFinal)
        {
            Indicator.Process(price, isFinal);
        }

        public override string ToString()
        {
            return IndicatorAdapted;
        }
    }
}
