using System.Threading.Tasks;
using Mahya.App.Extenstion;
using Mahya.App.IServices;
using Mahya.Domain.ViewModels.Admin.Order;
using Microsoft.AspNetCore.Mvc;

namespace Mahya.Web.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        #region Constructor

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region FilterOrder

        public async Task<IActionResult> FilterOrder(FilterOrdersViewModel filterOrders)
        {
            return View(await _orderService.FilterOrders(filterOrders));
        }

        #endregion

        #region ChangeStateToSent

        public async Task<IActionResult> ChangeOrderStateToSent(long orderId)
        {
            var result = await _orderService.ChangeOrderStateToSend(orderId);
            if (result)
            {
                return JsonResponseStatus.Success();
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region Orderdetail

        public async Task<IActionResult> ResultOrderDetail(long orderId)
        {
            var data = await _orderService.GetOrderDetailForAdmin(orderId);
            if (data == null) return NotFound();
            return View(data);
        }

        #endregion
    }
}
