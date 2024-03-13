using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF.Services
{
    public class BasketService : IBasketBLL
    {
        private readonly WebShopContext _context;

        public BasketService(WebShopContext context)
        {
            _context = context;
        }

        public void AddToBasket(BasketRequestDTO basketRequestDTO)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == basketRequestDTO.ProductId);

            if (product == null || !product.IsActive)
            {
                throw new InvalidOperationException("Product is not active and cannot be added to the basket.");
            }

            var existingBasketItem = _context.BasketPositions
                .FirstOrDefault(b => b.ProductId == basketRequestDTO.ProductId && b.UserId == basketRequestDTO.UserId);

            if (existingBasketItem != null)
            {
                existingBasketItem.Amount += basketRequestDTO.Quantity;
            }
            else
            {
                var newBasketItem = new BasketPosition
                {
                    ProductId = basketRequestDTO.ProductId,
                    UserId = basketRequestDTO.UserId,
                    Amount = basketRequestDTO.Quantity
                };

                _context.BasketPositions.Add(newBasketItem);
            }

            _context.SaveChanges();
        }

        public void RemoveFromBasket(int productId, int userId)
        {
            var basketItemToRemove = _context.BasketPositions
                .FirstOrDefault(b => b.ProductId == productId && b.UserId == userId);

            if (basketItemToRemove != null)
            {
                _context.BasketPositions.Remove(basketItemToRemove);
                _context.SaveChanges();
            }
        }

        public void UpdateBasketItemQuantity(BasketRequestDTO basketRequestDTO)
        {
            if (basketRequestDTO.Quantity <= 0)
            {
                throw new Exception("No nie da rady");
            }

            var basketItemToUpdate = _context.BasketPositions
                .FirstOrDefault(b => b.ProductId == basketRequestDTO.ProductId && b.UserId == basketRequestDTO.UserId);

            
            if (basketItemToUpdate != null)
            {
                basketItemToUpdate.Amount = basketRequestDTO.Quantity;
                _context.SaveChanges();
            }
        }

        public void GenerateOrderFromBasket(int userId)
        {
            var basketItems = _context.BasketPositions
                .Where(b => b.UserId == userId)
                .Include(b => b.Product) 
                .ToList();

            if (basketItems.Any())
            {
       
                var order = new Order
                {
                    UserId = userId,
                    Date = DateTime.Now 
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var basketItem in basketItems)
                {
                    var orderPosition = new OrderPosition
                    {
                        OrderId = order.OrderId,
                        Amount = basketItem.Amount,
                        Price = basketItem.Product.Price 
                    };

                    _context.OrderPositions.Add(orderPosition);
                }

                _context.BasketPositions.RemoveRange(basketItems);
                _context.SaveChanges();
            }
        }
    }
}
