using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class OrderService : IOrderBLL
    {
        private readonly WebShopContext _context;

        public OrderService(WebShopContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderItemDTO> GetOrderItemsPosition(int orderId)
        {
            var orderItems = _context.OrderPositions
        .Where(op => op.OrderId == orderId)
        .Select(op => new OrderItemDTO
        {
        })
        .ToList();

            return orderItems;
        }

        public IEnumerable<OrderDTO> GetOrders(string sortBy, bool sortAscending, int? orderIdFilter = null, bool? isPaidFilter = null)
        {
            var query = _context.Orders.AsQueryable();

            if (orderIdFilter.HasValue)
            {
                query = query.Where(o => o.OrderId == orderIdFilter);
            }

            if (isPaidFilter.HasValue)
            {
                query = query.Where(o => o.isPayed == isPaidFilter);
            }

            switch (sortBy.ToLower())
            {
                case "date":
                    query = sortAscending ? query.OrderBy(o => o.Date) : query.OrderByDescending(o => o.Date);
                    break;
                default:
                    query = query.OrderBy(o => o.OrderId);
                    break;
            }

            var orders = query.Select(o => new OrderDTO
            {
            }).ToList();

            return orders;
        }

        public void MakePayment(PaymentRequestDTO paymentRequestDTO)
        {
            var order = _context.Orders
                .Include(o => o.OrderPositions)
                .FirstOrDefault(o => o.OrderId == paymentRequestDTO.orderId && !o.isPayed);

            if (order == null)
            {
                throw new ArgumentException("Unpaid order not found.");
            }

            decimal totalOrderValue = (decimal)order.OrderPositions.Sum(op => op.Price * op.Amount);

            if ((decimal)paymentRequestDTO.amount != totalOrderValue)
            {
                throw new ArgumentException("Payment amount does not match the total order value.");
            }

            order.isPayed = true;
            order.Date = DateTime.Now; 

            _context.SaveChanges();
        }
    }
}
