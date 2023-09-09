using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.ViewModels.Admin.Order;

namespace Mahya.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveChanges();
        Task AddOrder(Order order);
        Task AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrder(Order order);
        void UpdateOrderDetail(OrderDetail orderDetail);
        Task<Order> GetOrderById(long orderId);
        Task<Order> GetOrderById(long orderId,long userId);
        Task<Order>CheckUserOrder(long userId);
        Task<OrderDetail>CheckOrderDetail(long orderId,long productId);
        Task<int> OrderSumPrice(long orderId);
        Task<Order>GetUserBasket(long userId,long orderId);
        Task<Order>GetUserBasket(long userId);
        Task<OrderDetail> GetOrderDetailById(long id);
        Task<ResultOrderStateViewModel> GetResultOrder();
        Task<ResultOrderStateViewModel> GetResultOrderToday();
        Task<FilterOrdersViewModel>FilterOrders(FilterOrdersViewModel filterOrders);
        Task<Order> GetOrderDetailForAdmin(long orderId);
    }
}
