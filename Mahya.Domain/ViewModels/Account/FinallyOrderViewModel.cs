using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahya.Domain.ViewModels.Account
{
    public class FinallyOrderViewModel
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public bool PayWithWallet { get; set; }
    }
    public enum FinallyOrderResult
    {
        HasNotUser,
        NotFound,
        Error,
        Suceess
    }
}
