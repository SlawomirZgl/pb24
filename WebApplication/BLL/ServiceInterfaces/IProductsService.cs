using BLL.DTOModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    internal interface IProductsService
    {

        IEnumerable<ProductsListResponseDTO> GetProductsList(string sortBy, bool sortAscending, bool getNotActive = false);

        bool AddNewProduct(ProductAddRequestDTO productDTO);

        bool DeactivateProduct(int id);
        bool DeleteProduct(int id);
        bool ActivateProduct(int id);
    }
}
