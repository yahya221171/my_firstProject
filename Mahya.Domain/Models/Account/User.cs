
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using mahya.Domain.Models.BaseEntities;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.Models.ProductEntity;
using Mahya.Domain.Models.Wallet;
using Shop.Domain.Models.Account;

namespace Mahya.Domain.Models.Account
{
    public class User: BaseEntity
    {
        #region properties

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "گذرواژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "آواتار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Avatar { get; set; }

        [Display(Name = "مسدود شده / نشده")]
        public bool IsBlocked { get; set; }

        [Display(Name = "کد احرازهویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MobileActiveCode { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PostAddress { get; set; }

        [Display(Name = "تایید شده / نشده")]
        public bool IsMobileActive { get; set; }

        [Display(Name = "جنسیت")]
        public UserGender UserGender { get; set; }
        #endregion

        #region relations
        public ICollection<UserWallet> UserWallets { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserFavorite> UserFavorites { get; set; }
        #endregion
    }
    public enum UserGender
    {
        [Display(Name = "آقا")]
        Mail,
        [Display(Name = "خانوم")]
        Femail,
        [Display(Name = "نامشخص")]
        Unknown
    }
}
