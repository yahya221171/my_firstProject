using Mahya.Domain.Models.Account;
using mahya.Domain.Models.BaseEntities;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.Models.ProductEntity
{
    public class ProductComment:BaseEntity
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }

        #region relation
        public Product Product { get; set; }
        public User User { get; set; }
        #endregion
    }
}
