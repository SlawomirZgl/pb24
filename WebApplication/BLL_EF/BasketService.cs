using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    internal class BasketService : IBasketService
    {
        private readonly IWebShopRepo webShop;
        public BasketService(IWebShopRepo webShop)
        {
            this.webShop = webShop;
        }
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
            var basketPosition = new BasketPosition
            {
                Id = 12,
                ProductId = basketDTO.ProductId,
                UserId = basketDTO.UserId,
                Amount = basketDTO.Amount,
                Product = webShop.GetProductById(basketDTO.ProductId),
                User = webShop.GetUserById(basketDTO.UserId)
            };
            return webShop.AddBasketPosition(basketPosition);
        }
    }
}
