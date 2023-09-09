
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahya.Domain.Models.Account;
using mahya.Domain.Models.BaseEntities;

namespace Mahya.Domain.Models.Orders
{
    public class Order : BaseEntity
    {
        #region properties

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }

        public OrderState OrderState { get; set; }
        #endregion


        #region relations
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
    public enum OrderState
    {
       
        [Display(Name = "درخواست شده")]
        Requested,
        [Display(Name = "در حال بررسی")]
        Processing,
        [Display(Name = "ارسال شده")]
        Sent,
        [Display(Name = "لغو شده")]
        Cancel
    }
}
