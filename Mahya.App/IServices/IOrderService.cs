using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Order;

namespace Mahya.App.IServices
{
    public interface IOrderService
    {
        Task<long> AddOrder(long productId, long userId);
        Task UpdatePriceOrder(long orderId);
        Task<Order> GetUserBasket(long userId, long orderId);
        Task<FinallyOrderResult>FinallyOrder(FinallyOrderViewModel finallyOrder, long userId);
        void UpdateOrder(Order order);
        Task<bool> DeleteOrderDetail(long orderDetailId);
        Task<Order> GetOrderById(long orderId);
        Task PayOnlineInFinallyOrder(long orderId);
        Task<Order> GetUserBasket(long userId);
        Task<ResultOrderStateViewModel> GetResultOrder();
        Task<ResultOrderStateViewModel> GetResultOrderToday();
        Task<FilterOrdersViewModel> FilterOrders(FilterOrdersViewModel filterOrders);
        Task<bool> ChangeOrderStateToSend(long orderId);
        Task<Order> GetOrderDetailForAdmin(long orderId);

    }
}
