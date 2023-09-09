using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Site.Products;
using Mahya.Domain.ViewModels.Site.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.ViewComponents
{
    public class SiteHeaderViewComponent:ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public SiteHeaderViewComponent(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserId());
                ViewBag.User = user;
                ViewBag.order = await _orderService.GetUserBasket(User.GetUserId());
                ViewBag.UserFavorite = await _userService.UserFavoriteCount(User.GetUserId());
            }

            return View("SiteHeader");
        }
    }
    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }

    #region Slider

    public class SliderViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;

        public SliderViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderViewModel = new FilterSlidersViewModel()
            {
                TakeEntity = 10
            };

            var data = await _siteService.FilterSlider(sliderViewModel);
            return View("_Slider",data);
        }
    }

    #endregion

    #region AllProductInSlider

    public class AllProductInSliderViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public AllProductInSliderViewComponent( IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var data = await _productService.GetAllProductInSlider();
            return View("_AllProductInSlider", data);
        }
    }

    #endregion

    #region AllProductInCategoryForMan

    public class AllProductInCategoryForManViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public AllProductInCategoryForManViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var data = await _productService.GetAllProductInCategory("forman",2);
            return View("_AllProductInCategoryForMan", data);
        }
    }

    #endregion

    #region AllProductInCategoryForWoman

    public class AllProductInCategoryForWomanViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public AllProductInCategoryForWomanViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var data = await _productService.GetAllProductInCategory("forwoman",3);
            return View("_AllProductInCategoryForWoman", data);
        }
    }

    #endregion

    #region ProductComments

    public class ProductCommentsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductCommentsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(long productId)
        {

            var data = await _productService.GetAllCommentByProductId(productId);
            return View("_ProductComments", data);
        }
    }

    #endregion

    #region ProductRelated

    public class ProductRelatedViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductRelatedViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(long productId)
        {
        
            var newdata = await _productService.ShowProductDetail(productId);
            var data = await _productService.RelatedProduct(newdata.ProductCategory.UrlName, productId);
            return View("_ProductRelated", data);
        }
    }

    #endregion
}
