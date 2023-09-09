using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Admin.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region Constructor

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region ProductCategory

        #region FilterProductCategory

        public async Task<IActionResult> FilterProductCategory(FilterProductCategoriesViewModel filter)
        {
            return View(await _productService.FilterProductCategory(filter));
        }

        #endregion

        #region CreateProductCategory
        [HttpGet]
        public async Task<IActionResult> CreateProductCategory()
        {
            var model = new CreateProductCategoryViewModel();
            ViewData["SelectedCategory"] = await _productService.GetAllProductCategory();

            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryViewModel create,IFormFile image)
        {

            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductCategory(create, image);
                switch (result)
                {
                    case CreateProductCategoryResult.IsExistUrlName:
                        TempData[ErrorMessage] = "این عنوان یکتا تکراری است لطفا  عنوان یکتا خود را تغییر دهید";
                        break;
                    case CreateProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی با موفقیت ثبت شد";
                        return RedirectToAction("FilterProductCategory");
                }
            }
            return View(create);
        }

        #endregion

        #region EditProductCategory
        [HttpGet]
        public async Task<IActionResult> EditProductCategory(long categoryId)
        {
            ViewData["category"] = await _productService.GetAllProductCategory();
            var data = await _productService.GetEditProductCategoryForShow(categoryId);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProductCategory(EditProductCategoryViewModel edit,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProductCategory(edit,image);
                switch (result)
                {
                    case EditProductCategoryResult.IsExistUrlName:
                        TempData[ErrorMessage] = "این عنوان یکتا تکراری است لطفا  عنوان یکتا خود را تغییر دهید";

                        break;
                    case EditProductCategoryResult.NotFound:
                        TempData[WarningMessage] = "دسته بندی یافت نشد";

                        break;
                    case EditProductCategoryResult.Success:
                        TempData[SuccessMessage] = "دسته بندی با موفقیت تغییر یافت";

                        return RedirectToAction("FilterProductCategory");
                }
            }

            return View(edit);
        }


        #endregion

        #endregion

        #region Product

        public async Task<IActionResult> FilterProduct(FilterProductsViewModel filter)
        {
     
            return View(await _productService.FilterProduct(filter));
        }

        #region CreateProduct
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewData["SelectedCategory"] = await _productService.GetAllProductCategory();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel create,IFormFile image1,IFormFile image2)
        {
            //ViewData["SelectedCategory"] = await _productService.GetAllProductCategory();
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(create, image1, image2);
                switch (result)
                {
                    case CreateProductResult.NotImage:
                        TempData[ErrorMessage] = "تصویری انتخاب کنید";
                        break;
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "محصول با موفقیت ثبت شد";
                        return RedirectToAction("FilterProduct");
                }
            }

            return View(create);
        }
        #endregion

        #region EditProduct

        [HttpGet]
        public async Task<IActionResult> EditProduct(long productId)
        {
            TempData["AllProductCategory"] = await _productService.GetAllProductCategory();

            var data = await _productService.EditProductForShow(productId);
            if (data == null) return NotFound();
            return View(data);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductViewModel edit)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProduct(edit);
                switch (result)
                {
                    case EditProductResult.NotFound:
                        TempData[ErrorMessage] = "محصولی یافت نشد";
                        break;
                    case EditProductResult.NotProductSelectedCategoryHasNull:
                        TempData[WarningMessage] = "لطفا دسته بندی محصول را انتخاب کنید";

                        break;
                    case EditProductResult.Success:
                        TempData[SuccessMessage] = "تغییرات محصول با موفقیت انجام شد";


                        return RedirectToAction("FilterProduct");
                }
            }
            return View(edit);
        }
        #endregion

        #region DeleteProduct-RecoverProduct

        public async Task<IActionResult> DeleteProduct(long productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (result)
            {
                TempData[SuccessMessage] = "محصول با موفقیت حذف شد";
               return RedirectToAction("FilterProduct");
            }
            TempData[ErrorMessage] = "خطایی در حذف محصول رخ داده است";
          return  RedirectToAction("FilterProduct");
        }
        public async Task<IActionResult> RecoverProduct(long productId)
        {
            var result = await _productService.RecoverProduct(productId);
            if (result)
            {
                TempData[SuccessMessage] = "محصول با موفقیت بازگردانی  شد";
                return RedirectToAction("FilterProduct");
            }
            TempData[ErrorMessage] = "خطایی در بازگردانی محصول رخ داده است";
            return RedirectToAction("FilterProduct");
        }

        #endregion

        #region ProductGallries

        public async Task<IActionResult> ProductGallries(long productId)
        {
            ViewBag.productId = productId;
            return View();
        }

        public async Task<IActionResult> AddImageToGallery(List<IFormFile> images, long productId)
        {
            var result = await _productService.Addproductgallries(productId, images);
            if (result)
            {
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }

        #region ShowGallery-DeleteGallery

        public async Task<IActionResult> ShowAllProductGallery(long productId)
        {
            var data = await _productService.GetAllProductGallry(productId);
            return View(data);
        }

        public async Task<IActionResult> DeleteGallery(long galleryId)
        {
            await _productService.DeleteImageFromGallery(galleryId);
            TempData[SuccessMessage] = "تصویر با موفقیت حذف شد";

            return RedirectToAction("FilterProduct");
        }

        #endregion

        #endregion

        #region ProductFeature
        [HttpGet]
        public async Task<IActionResult> CreateProductFeature(long productId)
        {
            var model = new CreateProductFeatuersViewModel()
            {
                ProductId = productId
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductFeature(CreateProductFeatuersViewModel create)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProductFeatures(create);
                switch (result)
                {
                    case CreateProductFeatuersResult.Error:
                        TempData[ErrorMessage] = "در ثبت ویژگی محصول خطایی رخ داده است";
                        break;
                    case CreateProductFeatuersResult.Success:
                        TempData[SuccessMessage] = "ثبت ویژگی محصول با موفقیت انجام شد";
                        return RedirectToAction("FilterProduct");

                }

            }
            return View(create);
        }

        #region Feature-Delete

        public async Task<IActionResult> GetProductFeatureForShow(long productId)
        {

            return View(await _productService.GetAllProductFeature(productId));
        }

        public async Task<IActionResult> DeleteFeature(long featureId)
        {
            await _productService.DeleteProductFeature(featureId);
            TempData[SuccessMessage] = "ویژگی با موفقیت حذف شد";
            return RedirectToAction("FilterProduct");
        }

        #endregion

        #endregion

        #endregion
    }
}
