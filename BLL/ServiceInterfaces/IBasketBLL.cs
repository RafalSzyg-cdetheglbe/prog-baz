using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IBasketBLL
    {
        void AddToBasket(BasketRequestDTO basketRequestDTO);
        void UpdateBasketItemQuantity(BasketRequestDTO basketRequestDTO);
        void RemoveFromBasket(int productId, int userId);
        void GenerateOrderFromBasket(int userId);
    }
}
