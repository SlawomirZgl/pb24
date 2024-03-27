using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Model.Model;

namespace BLL_EF
{
    public class ProductService : IProductsService
    {
        private readonly IWebShopRepo webShop;

        public ProductService(IWebShopRepo webShop)
        {
            this.webShop = webShop;
        }

        public bool ActivateProduct(int id)
        {
            bool success = webShop.GetProductById(id).IsActive = true;
            webShop.ContextSaveChanges();
            return success;
        }

        public bool AddNewProduct(ProductAddRequestDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }

            int maxId = webShop.GetProducts().Any() ? webShop.GetProducts().Max(p => p.Id) : 0;

            Product product = new Product
            {
                //Id = maxId + 1,
                Name = productDTO.Name,
                Price = productDTO.Price,
                GroupId = productDTO.GroupId,
                IsActive = true,
                Image = "",
                Group = productDTO.GroupId != null ? webShop.GetGroupById(productDTO.GroupId) : null
            };

            return webShop.AddProduct(product);
        }

        public bool DeactivateProduct(int id)
        {
            var product = webShop.GetProductById(id);
            // to do add logics
            if (product != null)
            {
                product.IsActive = false;
                webShop.ContextSaveChanges();
                return true;
            }
            
            return false;
        }

        public IEnumerable<ProductsListResponseDTO> GetProductsList(ProductListRequestDTO productDTO)
        {
            if (productDTO == null)
            {
                return new List<ProductsListResponseDTO>();
            }
            var products = webShop.GetProducts();

            if (productDTO.filterByName != null)
            {
                products = products.Where(p => p.Name.Contains(productDTO.filterByName));
            }
            if (productDTO.filterByGroupName != null)
            {
                products = products.Where(p => webShop.GetGroupName(p.Id).Contains(productDTO.filterByGroupName));
            }
            if (productDTO.filterByGroupId != null)
            {
                products = products.Where(p => p.GroupId.ToString().Contains(productDTO.filterByGroupId));
            }

            if (productDTO.orderBy != null && products.Any())
            {
                if (products.FirstOrDefault().GetType().GetProperty(productDTO.orderBy) != null)
                {
                    if (productDTO.orderAscending != null && productDTO.orderAscending.Value)
                    {
                        products = products.OrderBy(p => p.GetType().GetProperty(productDTO.orderBy).GetValue(p));
                    }
                    else
                    {
                        products = products.OrderByDescending(p => p.GetType().GetProperty(productDTO.orderBy).GetValue(p));
                    }
                }
            }

            return products.Select(p => new ProductsListResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
                GroupId = p.GroupId,
                GroupName = webShop.GetGroupName(p.GroupId != null ? p.GroupId.Value : -1)
            });
        }
    }
}