
using mahya.Domain.Models.BaseEntities;



namespace Mahya.Domain.Models.ProductEntity
{
    public class ProductSelectedCategories:BaseEntity
    {
        #region properties
        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }
        #endregion

        #region relations
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        #endregion
    }
}
