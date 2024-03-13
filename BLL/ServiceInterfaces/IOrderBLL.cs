using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IOrderBLL
    {
        void MakePayment(PaymentRequestDTO paymentRequestDTO);
        IEnumerable<OrderDTO> GetOrders(
          string sortBy,
          bool sortAscending,
          int? orderIdFilter = null,
          bool? isPaidFilter = null
      );

        IEnumerable<OrderItemDTO> GetOrderItemsPosition(int orderId);

    }
}
