using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.ViewModels.Site.Products
{
    public class ProductDetailViewModel
    {
        #region properties
        public long ProductId { get; set; }

        [Display(Name = "نام محصول")]
        public string Name { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "قیمت محصول")]
        public int Price { get; set; }
        [Display(Name = "قیمت جدید محصول")]
        public int NewPrice { get; set; }

        [Display(Name = "تصویر1 محصول")]
        public string ProductImageName1 { get; set; }
        [Display(Name = "تصویر2 محصول")]
        public string ProductImageName2 { get; set; }
        public bool Discount { get; set; }
        public int ProductComment { get; set; }
        public int ProductCount { get; set; }
        public List<string> ProductImages { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public List<ProductFeature> ProductFeatures { get; set; }
        #endregion
    }
}
