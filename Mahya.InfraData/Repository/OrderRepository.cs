using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahya.Domain.Interfaces;
using Mahya.Domain.Models.Orders;
using Mahya.Domain.ViewModels.Admin.Order;
using Mahya.Domain.ViewModels.Paging;
using Mahya.InfraData.Context;
using Microsoft.EntityFrameworkCore;

namespace Mahya.InfraData.Repository
{
    public class OrderRepository:IOrderRepository
    {
        #region Constructor

        private readonly MahyaDbContext _context;

        public OrderRepository(MahyaDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Order-Detail

        

        #endregion

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == orderId);
        }

        public async Task<Order> GetOrderById(long orderId, long userId)
        {
            return await _context.Orders.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == orderId && c.UserId == userId);
        }

        public async Task<Order> CheckUserOrder(long userId)
        {
            return await _context.Orders.AsQueryable()
                .Where(c => c.UserId == userId&&!c.IsFinaly)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> CheckOrderDetail(long orderId, long productId)
        {
            return await _context.OrderDetails.AsQueryable()
                .Where(c => c.ProductId == productId && c.OrderId == orderId&&!c.IsDelete)
                .FirstOrDefaultAsync();
        }

        public async Task<int> OrderSumPrice(long orderId)
        {
            return await _context.OrderDetails.AsQueryable()
                .Where(c => c.OrderId == orderId && !c.IsDelete).SumAsync(c => c.Price*c.Count);
        }

        public async Task<Order> GetUserBasket(long userId, long orderId)
        {
            return await _context.Orders.AsQueryable()
                .Include(s => s.User)
                .Where(c => c.UserId == userId && c.Id == orderId)
                .Select(c => new Order()
                {
                    UserId = c.UserId,
                    Id = c.Id,
                    OrderSum = c.OrderSum,
                    IsFinaly = false,
                    OrderDetails = _context.OrderDetails.Include(s=>s.Product)
                        .Where(s=>!s.IsDelete&&!s.Order.IsFinaly&&s.Order.UserId==userId).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<Order> GetUserBasket(long userId)
        {
            return await _context.Orders.AsQueryable()
                .Include(c => c.OrderDetails).ThenInclude(c => c.Product)
                .Where(c => c.UserId == userId && !c.IsFinaly && !c.IsDelete&&c.OrderState == OrderState.Processing)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(long id)
        {
            return await _context.OrderDetails.AsQueryable()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ResultOrderStateViewModel> GetResultOrder()
        {
            return new ResultOrderStateViewModel()
            {
                CancelCount = await _context.Orders.Where(c => c.OrderState == OrderState.Cancel).CountAsync(),
                ProcessingCount = await _context.Orders.Where(c => c.OrderState == OrderState.Processing).CountAsync(),
                RequestCount = await _context.Orders.Where(c => c.OrderState == OrderState.Requested).CountAsync(),
                SentCount = await _context.Orders.Where(c => c.OrderState == OrderState.Sent).CountAsync(),
            };
        }

        public async Task<ResultOrderStateViewModel> GetResultOrderToday()
        {
            return new ResultOrderStateViewModel()
            {
                CancelCount = await _context.Orders.Where(c => c.OrderState == OrderState.Cancel&&c.CreateDate.Day==DateTime.Now.Day).CountAsync(),
                ProcessingCount = await _context.Orders.Where(c => c.OrderState == OrderState.Processing && c.CreateDate.Day == DateTime.Now.Day).CountAsync(),
                RequestCount = await _context.Orders.Where(c => c.OrderState == OrderState.Requested && c.CreateDate.Day == DateTime.Now.Day).CountAsync(),
                SentCount = await _context.Orders.Where(c => c.OrderState == OrderState.Sent && c.CreateDate.Day == DateTime.Now.Day).CountAsync(),
            };
        }

        public async Task<FilterOrdersViewModel> FilterOrders(FilterOrdersViewModel filterOrders)
        {
            var query = _context.Orders
                .Include(c=>c.OrderDetails)
                .ThenInclude(c=>c.Product)
                .AsQueryable();
            #region filter
            if (filterOrders.UserId.HasValue && filterOrders.UserId != 0)
            {
                query = query.Where(c => c.UserId == filterOrders.UserId);
            }
            #endregion

            #region state
            switch (filterOrders.OrderStateFilter)
            {
                case OrderStateFilter.All:
                    break;
                case OrderStateFilter.Requested:
                    query = query.Where(c => c.OrderState == OrderState.Requested);
                    break;
                case OrderStateFilter.Processing:
                    query = query.Where(c => c.OrderState == OrderState.Processing);

                    break;
                case OrderStateFilter.Sent:
                    query = query.Where(c => c.OrderState == OrderState.Sent);

                    break;
                case OrderStateFilter.Cancel:
                    query = query.Where(c => c.OrderState == OrderState.Cancel);

                    break;
            }
            #endregion


            var pager = Pager.Build(filterOrders.PageId,
                await query.CountAsync(),
                filterOrders.TakeEntity, filterOrders.CountForShowAfterAndBefor);
            var allData = await query.Paging(pager).ToListAsync();
            return filterOrders.SetPaging(pager).SetOrders(allData);
        }

        public async Task<Order> GetOrderDetailForAdmin(long orderId)
        {
            return await _context.Orders.AsQueryable()
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.Product)
                .Include(c=>c.User)
                .Where(c => c.Id == orderId)
                .SingleOrDefaultAsync();
        }
    }
}
