using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.ViewModels.Site;

namespace Mahya.Domain.ViewModels.Account
{
    public class ForgotPasswordViewModel:Recaptcha
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }

    public enum ForgotPasswordResult
    {
        Success,
        NotFound,
        EmailNotExist
    }
    public class ResetPasswordViewModel : Recaptcha
    {
        public string MobileActiveCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    public enum ResetPasswordResult
    {
        Success,
        NotFound
    }
}
