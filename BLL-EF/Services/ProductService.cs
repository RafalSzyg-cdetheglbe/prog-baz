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
    public class ProductService : IProductBLL
    {
        private readonly WebShopContext _context;

        public ProductService(WebShopContext context)
        {
            _context = context;
        }

        public void ActivateProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.IsActive = true;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product not found.");
            }
        }

        public void AddProduct(ProductRequestDTO productRequest)
        {
            if (productRequest.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            var product = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price ,
                GroupId = productRequest.GroupId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeactivateProduct(int productId)
        {
            bool isProductAssociated = _context.BasketPositions.Any(bp => bp.ProductId == productId);

            if (isProductAssociated)
            {
                throw new InvalidOperationException("Product cannot be deactivated.");
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.IsActive = false;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product not found.");
            }
        }

        public void DeleteProduct(int productId)
        {

            bool isProductAssociated = _context.BasketPositions.Any(bp => bp.ProductId == productId);

            if (isProductAssociated)
            {
                throw new InvalidOperationException("Product cannot be deactivated.");
            }

            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product not found.");
            }
        }

        public IEnumerable<ProductResponseDTO> GetProducts(
            string sortBy,
            bool sortAscending,
            string nameFilter = null,
            string groupFilter = null,
            int? groupIdFilter = null,
            bool includeInactive = false)
        {
            var productsQuery = _context.Products.Include(p => p.Group).ThenInclude(x => x.Parent).AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(nameFilter));
            }
            if (!string.IsNullOrEmpty(groupFilter))
            {
                productsQuery = productsQuery.Where(p => p.Group.Name.Contains(groupFilter));
            }
            if (groupIdFilter.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.GroupId == groupIdFilter.Value);
            }
            if (!includeInactive)
            {
                productsQuery = productsQuery.Where(p => p.IsActive);
            }

            switch (sortBy.ToLower())
            {
                case "name":
                    productsQuery = sortAscending ? productsQuery.OrderBy(p => p.Name) : productsQuery.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    productsQuery = sortAscending ? productsQuery.OrderBy(p => p.Price) : productsQuery.OrderByDescending(p => p.Price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.ProductId);
                    break;
            }

            return productsQuery.Select(p => new ProductResponseDTO(
                p.ProductId,
                p.Name,
                p.Price,
                GetFullGroupName(p.Group))).ToList();
        }

        private string GetFullGroupName(ProductGroup group)
        {
            if (group == null)
            {
                return ""; 
            }

            var groupNames = new List<string>();
            while (group != null)
            {
                groupNames.Add(group.Name);
                group = group.Parent; 
            }
            groupNames.Reverse();

            return string.Join(" / ", groupNames);
        }
    }
}
