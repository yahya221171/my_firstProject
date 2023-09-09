using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahya.Domain.Models.Account;
using mahya.Domain.Models.BaseEntities;
using Mahya.Domain.Models.Orders;


namespace Mahya.Domain.Models.ProductEntity
{
    public class Product : BaseEntity
    {
        #region properties

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "قیمت جدید محصول")]
        public int NewPrice { get; set; }

        [Display(Name = "تصویر محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ProductImageName { get; set; }
        [Display(Name = "2تصویر محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ProductImageName2 { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "فعال / غیر فعال")]
        public bool Discount { get; set; }
        [Display(Name = "تعداد محصول")]

        public int ProductCount { get; set; }
        #endregion

        #region relations
        public ICollection<ProductGalleries> ProductGalleries { get; set; }
        public ICollection<ProductSelectedCategories> ProductSelectedCategories { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
         public ICollection<OrderDetail> OrderDetails { get; set; }
         public ICollection<UserFavorite> UserFavorites { get; set; }

        #endregion
    }
}
