using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    internal interface IBasketService
    {
        //bool GenerateBasketPosition(int productId, int userId);
        bool GenerateBasketPosition(BasketAddRequestDTO basketDTO);
        bool ChangeAmountOfProductsInBasket(int basketPositionId, int amount);
        bool DeleteBasketPosition(int basketPositionId);
    }
}
