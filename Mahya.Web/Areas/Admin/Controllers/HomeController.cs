using System.Threading.Tasks;
using Mahya.App.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region Constructor

        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion
        #region dashboard
        public async Task<IActionResult> Index()
        {
            ViewData["ResultOrder"] = await _orderService.GetResultOrder();
            ViewData["ResultOrderToday"] = await _orderService.GetResultOrderToday();
            return View();
        }
        #endregion
    }
}
