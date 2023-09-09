﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Mahya.Domain.ViewModels.Admin.Products
{
    public class EditProductViewModel
    {

        public long ProductId { get; set; }

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

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "فعال / غیر فعال")]
        public bool Discount { get; set; }

        public List<long> ProductSelectedCategory { get; set; }
        public string ProductImageName1 { get; set; }
        public string ProductImageName2 { get; set; }
        public IFormFile ProductImage1 { get; set; }
        public IFormFile ProductImage2 { get; set; }
    }

    public enum EditProductResult
    {
        NotFound,
        NotProductSelectedCategoryHasNull,
        Success
    }
}
