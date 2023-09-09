using System.Collections.Generic;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.ViewModels.Site.Products
{
    public class ProductItemViewModel
    {
        #region properties
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName1 { get; set; }
        public string ProductImageName2 { get; set; }
        public int Price { get; set; }
        public int NewPrice { get; set; }
        public int ProductComments { get; set; }
        public bool Discount { get; set; }
        public bool IsActive { get; set; }
        public ProductCategory ProductCategory { get; set; }
       // public int CommentCount { get; set; }
        public ProductFeature ProductFeature { get; set; }
        public List<string> Images { get; set; }
        #endregion
    }
}
