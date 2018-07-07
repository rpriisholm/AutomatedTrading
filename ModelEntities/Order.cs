using SidesEnum;
using StockSolution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockSolution.ModelEntities.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public SecurityInfo SecurityId { get; set; }
        [Required]
        public int Leverage { get; private set; }
        [Required]
        public decimal StartPieceValue { get; set; }
        [Required]
        public decimal CurrentPieceValue { get; set; }
        [Required]
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

        public virtual Sides Direction { get; set; }

        public Order(Sides direction, SecurityInfo securityInfo, int leverage, decimal startValue, decimal startPieceValue)
        {
            this.SecurityId = securityInfo;
            this.Leverage = leverage;
            this.StartValue = startValue;
            this.Direction = direction;
            this.StartPieceValue = startPieceValue;
            this.CurrentPieceValue = startPieceValue;
        }
    }
}
