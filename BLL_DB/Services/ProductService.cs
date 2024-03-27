using BLL.DTOModels;
using BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DB.Services
{
    public class ProductService : IProductBLL
    {
        public void ActivateProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(ProductRequestDTO productRequest)
        {
            throw new NotImplementedException();
        }

        public int AddProduct(string name, object image, double price, int groupId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductResponseDTO> GetProducts(string sortBy, bool sortAscending, string nameFilter = null, string groupFilter = null, int? groupIdFilter = null, bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        public object GetProducts(string? name, int? groupId, bool? activeOnly, bool descending)
        {
            throw new NotImplementedException();
        }
    }
}
