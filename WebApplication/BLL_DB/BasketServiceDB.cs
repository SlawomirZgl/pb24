using BLL.DTOModels;
using BLL.ServiceInterfaces;

namespace BLL_DB
{
    public class BasketServiceDB : IBasketService
    {
        public bool ChangeAmountOfProductsInBasket(int basketPositionId, int amount)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBasketPosition(int basketPositionId)
        {
            throw new NotImplementedException();
        }

        public bool GenerateBasketPosition(BasketAddRequestDTO basketDTO)
        {
            throw new NotImplementedException();
        }
    }
}