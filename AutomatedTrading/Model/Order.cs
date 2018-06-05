using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public class Order
    {
        public string Id { get; private set; }
        public string SecurityCode { get; private set; }
        public string SecurityName { get; private set; }
        public int Leverage { get; private set; }
        public decimal StartPieceValue { get; set; }
        public decimal CurrentPieceValue { get; set; }
        public decimal StartValue { get; private set; }

        /* CHECK CALCS + ORDER ESSPACALY ON SALE */
        public decimal Profit
        {
            get
            {
                return this.NewestValue-this.StartValue;
            }
        }

        private decimal Volume { get { return (this.StartValue + this.LoanedValue) / this.StartPieceValue; } }

        /* CHECK CALCS + ORDER ESSPACALY ON SALE */
        private decimal _NewestValue;
        public decimal NewestValue
        {
            get
            {  
                if (Sides.Buy == Direction)
                {
                    //Calc new price using volume and substract loaned value
                    this._NewestValue = (this.Volume * this.CurrentPieceValue) - this.LoanedValue;
                }

                if (Sides.Sell == Direction)
                {
                    //For _NewestValue difference is calculated since full volume is used there no reason to subtract loaned value
                    this._NewestValue = (this.Volume * (this.StartPieceValue - this.CurrentPieceValue) + this.Volume * this.StartPieceValue - this.LoanedValue);
                }
                
                return _NewestValue;
            }
        }

        public decimal LoanedValue { get { return StartValue * Leverage - StartValue; } }

        public Sides Direction { get; set; }

        public Order(string id, Sides direction, string securityCode, string securityName, int leverage, decimal startValue, decimal startPieceValue)
        {
            this.Id = id;
            this.SecurityCode = securityCode;
            this.SecurityName = securityName;
            this.Leverage = leverage;
            this.StartValue = startValue;
            this.Direction = direction;
            this.StartPieceValue = startPieceValue;
            this.CurrentPieceValue = startPieceValue;
        }
    }
}
