using BLL.DTOModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IProductsService
    {

        IEnumerable<ProductsListResponseDTO> GetProductsList(ProductListRequestDTO productDTO);

        bool AddNewProduct(ProductAddRequestDTO productDTO);

        bool DeactivateProduct(int id);
        bool DeleteProduct(int id);
        bool ActivateProduct(int id);
    }
}
