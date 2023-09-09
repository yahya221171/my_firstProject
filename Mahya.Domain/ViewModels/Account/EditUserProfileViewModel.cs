﻿using Shop.Domain.Models.Account;
using System.ComponentModel.DataAnnotations;
using Mahya.Domain.Models.Account;

namespace Mahya.Domain.ViewModels.Account
{
    public class EditUserProfileViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        public string PostAddress { get; set; }

        [Display(Name = "جنسیت")]
        public UserGender UserGender { get; set; }

    }
    public enum EditUserProfileResult
    {
        NotFound,
        Success
    }
}
