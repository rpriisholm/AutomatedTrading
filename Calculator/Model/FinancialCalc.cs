using Calculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class FinancialCalc
    {
        public StockHist Hist
        {
            get;
            set;
        }

        public int GeneralRisk
        {
            get;
            set;
        }

        public int CurrentRisk
        {
            get;
            set;
        }

        public double AllTimeLow
        {
            get;
            set;
        }

        public double AllTimeHigh
        {
            get;
            set;
        }

        public double CurrentValue
        {
            get;
            set;
        }

        //Constructor
        public FinancialCalc()
        {

        }

        //In Procentage
        public double GainLossEstimate
        {
            get;
            set;
        }

        //Calcs And Sets GainLossEstimate
        public double CalcGainLossEstimate()
        {
            return 0;
        }

        public double CalcGeneralRisk()
        {
            return 0;
        }

        public double CalcCurrentRisk()
        {
            return 0;
        }

        public void AvgLowAndHigh(int interval, out double avgLow, out double avgHigh)
        {
            List<DateTime> times = new List<DateTime>();
            avgLow = 0;
            avgHigh = 0;

            foreach (DateTime time in Hist.Keys)
            {
                if (time.Subtract(times[times.Count - 1]).TotalSeconds == interval || times.Count == 0)
                {
                    times.Add(time);
                }
                avgLow += Hist[time].Low;
                avgHigh += Hist[time].High;
            }

            avgLow /= times.Count;
            avgHigh /= times.Count;
        }

        public void CalcPeriodIncrease()
        {

        }

        public void CalcPeriodDecrease(int days)
        {
            foreach(DateTime key in Hist.Keys)
            {

            }
        }

        public void CalcMaxLeverage()
        {

        }
    }
}
