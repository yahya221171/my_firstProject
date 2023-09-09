
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mahya.App.IServices;

namespace Mahya.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Constructor

        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            ViewData["newProduct"] = await _productService.NewProduct();
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
