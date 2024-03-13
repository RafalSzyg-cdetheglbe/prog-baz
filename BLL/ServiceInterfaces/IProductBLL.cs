using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IProductBLL
    {
        IEnumerable<ProductResponseDTO> GetProducts(string sortBy, bool sortAscending, string nameFilter = null, string groupFilter = null, int? groupIdFilter = null, bool includeInactive = false);
        void AddProduct(ProductRequestDTO productRequest);
        void DeactivateProduct(int productId);
        void ActivateProduct(int productId);
        void DeleteProduct(int productId);
    }
}
