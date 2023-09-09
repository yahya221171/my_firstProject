﻿using System.ComponentModel.DataAnnotations;

namespace Mahya.Domain.ViewModels.Admin.Products
{
    public class CreateProductFeatuersViewModel
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ProductId { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "مقدار ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Value { get; set; }
    }
    public enum CreateProductFeatuersResult
    {
        Error,
        Success
    }
}
