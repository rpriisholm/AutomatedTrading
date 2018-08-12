using SidesEnum;
using StockSolution.Entity.Models;
using StockSolution.ModelEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSolution.ModelEntities.Models
{
    public abstract class AConnection : IConnection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public abstract Dictionary<SecurityInfo, decimal> AlltimeRealizedProfits();
        public abstract decimal CalcPayment();
        public abstract Order CancelOrder(SecurityInfo securityCode, Sides direction, decimal piecePrice);
        public abstract Order CancelOrder(SecurityInfo securityCode);
        public abstract decimal GetInvestedValue();
        public virtual Portfolio Portfolio { get; set; }
        public Portfolio GetPortfolio() { return Portfolio; }
        public abstract decimal GetRemainingValue();
        public decimal GetTotalValue() { return GetRemainingValue() + GetInvestedValue(); }
        public abstract void InitializeSecurityID(SecurityInfo securityID);
        public virtual Dictionary<SecurityInfo, Order> Orders { get; set; }
        public Dictionary<SecurityInfo, Order> LoadOrders()
        {
            return this.Orders;
        }
        public abstract Order MakeOrder(SecurityInfo securityCode, Sides direction, int leverage, decimal piecePrice);
        public abstract decimal Profit(SecurityInfo securityID);
        public abstract Dictionary<SecurityInfo, decimal> RealizedProfits();
    }
}
