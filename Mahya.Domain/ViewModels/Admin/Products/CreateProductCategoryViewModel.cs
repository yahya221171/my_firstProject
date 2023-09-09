﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.ViewModels.Admin.Products
{
    public class CreateProductCategoryViewModel
    {
        #region properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "عنوان url")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UrlName { get; set; }

        public long categorySelecteId { get; set; }

        public List<long> SelectedCategory { get; set; }

        //[Display(Name = "تصویر")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        //public string ImageName { get; set; }
        #endregion

    }
    public enum CreateProductCategoryResult
    {
        IsExistUrlName,
        Success
    }
}
