
using System.ComponentModel.DataAnnotations;
using Mahya.Domain.ViewModels.Site;

namespace Mahya.Domain.ViewModels.Account
{
    public class ActiveAccountViewModel:Recaptcha
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ActiveCode { get; set; }
    }
    public enum ActiveAccountResult
    {
        Error,
        Success,
        NotFound
    }
}
