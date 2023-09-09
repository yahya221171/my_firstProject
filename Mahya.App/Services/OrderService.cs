using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.App.IServices;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.Models.Wallet;
using Mahya.Domain.ViewModels.Account;
using Mahya.Domain.ViewModels.Admin.Order;

namespace Mahya.App.Services
{
    public class OrderService : IOrderService
    {
        #region Constructor

        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWalletRepository _walletRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IWalletRepository walletRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _walletRepository = walletRepository;
        }

        #endregion

        #region Order

        public async Task<long> AddOrder(long productId, long userId)
        {
            var product = await _productRepository.GetProductById(productId);
            var order = await _orderRepository.CheckUserOrder(userId);

            if (order == null)
            {
                //createOrder
                order = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    OrderState = OrderState.Processing,
                    OrderSum = (product.Discount ? product.NewPrice : product.Price),
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            Count = 1,
                            ProductId = productId,
                            Price = (product.Discount ? product.NewPrice : product.Price),
                        }
                    }
                };
                await _orderRepository.AddOrder(order);
                await _orderRepository.SaveChanges();

            }
            else
            {
                //UpdateOrder
                var detail = await _orderRepository.CheckOrderDetail(order.Id, product.Id);
                if (detail != null)
                {
                    detail.Count += 1;
                    _orderRepository.UpdateOrderDetail(detail);

                }
                else
                {
                    detail = new OrderDetail()
                    {
                        ProductId = productId,
                        OrderId = order.Id,
                        Count = 1,
                        Price = (int) (product.Discount ? product.NewPrice : product.Price),
                    };
                    await _orderRepository.AddOrderDetail(detail);
                }

                await _orderRepository.SaveChanges();

            }

            await UpdatePriceOrder(order.Id);
            return order.Id;
        }

        public async Task UpdatePriceOrder(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            order.OrderSum = await _orderRepository.OrderSumPrice(orderId);
            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChanges();
        }

        public async Task<Order> GetUserBasket(long userId, long orderId)
        {
            return await _orderRepository.GetUserBasket(userId, orderId);
        }

        public async Task<FinallyOrderResult> FinallyOrder(FinallyOrderViewModel finallyOrder, long userId)
        {
            if (userId != finallyOrder.UserId) return FinallyOrderResult.HasNotUser;
            var order = await _orderRepository.GetOrderById(finallyOrder.OrderId, userId);
            if (order == null || order.IsFinaly) return FinallyOrderResult.NotFound;
            if (await _walletRepository.GetUserWalletAmount(userId) >= order.OrderSum)
            {
                order.IsFinaly = true;
                order.OrderState = OrderState.Requested;

                var wallet = new UserWallet()
                {
                    UserId = userId,
                    WalletType = WalletType.Bardasht,
                    Description = $"برداشت مبلع برای فاکتور{order.Id}",
                    IsPay = true,
                    Amount = order.OrderSum
                };
                await _walletRepository.CreateWallet(wallet);
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChanges();
               return FinallyOrderResult.Suceess;
            }
            return FinallyOrderResult.Error;
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }

        public async Task<bool> DeleteOrderDetail(long orderDetailId)
        {
            
            var detail = await _orderRepository.GetOrderDetailById(orderDetailId);
            var order = await _orderRepository.GetOrderById(detail.OrderId);
            if (detail != null)
            {
                detail.IsDelete = true;
                _orderRepository.UpdateOrderDetail(detail);
                order.OrderSum = (await _orderRepository.OrderSumPrice(order.Id)) - detail.Price;
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await _orderRepository.GetOrderById(orderId);
        }

        public async Task PayOnlineInFinallyOrder(long orderId)
        {
            var order = await GetOrderById(orderId);
            if (order != null)
            {
                order.IsFinaly = true;
                order.OrderState = OrderState.Requested;
                UpdateOrder(order);
                await _orderRepository.SaveChanges();
            }
        }

        public async Task<Order> GetUserBasket(long userId)
        {
            return await _orderRepository.GetUserBasket(userId);
        }

        public async Task<ResultOrderStateViewModel> GetResultOrder()
        {
            return await _orderRepository.GetResultOrder();
        }

        public async Task<ResultOrderStateViewModel> GetResultOrderToday()
        {
            return await _orderRepository.GetResultOrderToday();

        }

        public async Task<FilterOrdersViewModel> FilterOrders(FilterOrdersViewModel filterOrders)
        {
            return await _orderRepository.FilterOrders(filterOrders);
        }

        public async Task<bool> ChangeOrderStateToSend(long orderId)
        {
            var currentOrder = await _orderRepository.GetOrderById(orderId);
            if (currentOrder != null)
            {
                currentOrder.OrderState = OrderState.Sent;
                UpdateOrder(currentOrder);
                await _orderRepository.SaveChanges();

            }

            return false;
        }

        public async Task<Order> GetOrderDetailForAdmin(long orderId)
        {
            return await _orderRepository.GetOrderDetailForAdmin(orderId);
        }

        #endregion


    }
}
