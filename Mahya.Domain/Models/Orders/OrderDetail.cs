using System.ComponentModel.DataAnnotations;
using mahya.Domain.Models.BaseEntities;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.Models.Orders
{
    public class OrderDetail:BaseEntity
    {
        #region properties
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long OrderId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Count { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        #endregion

        #region relations
        public Order Order { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
