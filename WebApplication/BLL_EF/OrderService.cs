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

namespace BLL_EF
{
    public class OrderService : IOrderService
    {
        private readonly IWebShopRepo webShop;
        public OrderService(IWebShopRepo webShop)
        {
            this.webShop = webShop;
        }
        public OrderGeneratedResponseDTO GenerateOrder(int userId)
        {
            // Sprawdź, czy użytkownik istnieje
            var user = webShop.GetUserById(userId);
            if (user == null)
            {
                // Użytkownik o podanym ID nie został znaleziony
                return new OrderGeneratedResponseDTO();
            }

            // Pobierz koszyk użytkownika
            var basketItems = webShop.GetBasketPositions().Where(bp => bp.UserId == userId).ToList();
            if (basketItems.Count == 0)
            {
                // Koszyk użytkownika jest pusty
                return new OrderGeneratedResponseDTO(); ;
            }

            // Utwórz nowe zamówienie dla użytkownika
            var order = new Order
            {
                UserId = userId,
                Date = DateTime.Now,
                isPaid = false
            };
            webShop.AddOrder(order);

            // Przenieś pozycje z koszyka do zamówienia
            foreach (var basketItem in basketItems)
            {
                var orderPosition = new OrderPosition
                {
                    OrderId = order.Id,
                    Amount = basketItem.Amount,
                    Price = webShop.getPriceOfProduct(basketItem.ProductId)
                };
                webShop.AddOrderPositions(orderPosition);
            }
            webShop.ContextSaveChanges();

            // Usuń elementy z koszyka użytkownika
            webShop.RemoveRangeBasketPositions(basketItems);

            return new OrderGeneratedResponseDTO
            {
                Id = order.Id,
                UserId = userId,
                OrderPositions = webShop.GetAllOrderPositionsForOrder(order.Id).Select(op => new OrderPositionResponseDTO
                {
                    Id = op.Id,
                    OrderId = op.OrderId,
                    Amount = op.Amount,
                    Price = op.Price
                }).ToList()
            };
        }

        public bool PayForOrder(double value)
        {
            throw new NotImplementedException();
        }
    }
}
