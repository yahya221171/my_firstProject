using System;
using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Admin.Products;
using Mahya.Domain.ViewModels.Site.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Constructor

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ProductController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        #endregion
        [HttpGet("products")]
        public async Task<IActionResult> Products(FilterProductsViewModel filter)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategory();
            filter.TakeEntity = 12;
            filter.ProductBox = ProductBox.ItemBoxInSite;
            var data = await _productService.FilterProduct(filter);
            return View(data);
        }

        [HttpGet("showdetail/{productId}")]
        public async Task<IActionResult> ShowProductDetail(long productId)
        {
            var data = await _productService.ShowProductDetail(productId);
            if (data == null) return NotFound();
      
            return View(data);
        }

        [HttpPost("add-comment"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductComment(CreateProductCommentViewModel create)
        {
            if (!string.IsNullOrEmpty(create.Text))
            {
                var result = await _productService.CreateProductComments(create, User.GetUserId());

                switch (result)
                {
                    case CreateProductCommentResult.CheckUser:
                        TempData[ErrorMessage] = "کاربری یافت نشد";
                        break;
                    case CreateProductCommentResult.CheckProduct:
                        TempData[ErrorMessage] = "محصولی یافت نشد";

                        break;
                    case CreateProductCommentResult.Suucess:
                        TempData[SuccessMessage] = "نظر شما با موفقیت ثبت شد";
                        return RedirectToAction("ShowProductDetail", new {productId = create.ProductId});
                }
            }
            TempData[WarningMessage] = "لطفا نظر خود را وارد کنید";
            return RedirectToAction("ShowProductDetail", new { productId = create.ProductId });
        }

        [Authorize]
        public async Task<IActionResult> BuyProduct(long productId)
        {
            var orderId = await _orderService.AddOrder(productId, User.GetUserId());
            return Redirect("/User/basket/"+orderId);
        }
    }
}
