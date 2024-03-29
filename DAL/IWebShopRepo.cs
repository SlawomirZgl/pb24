﻿using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public interface IWebShopRepo
    {
        void ContextSaveChanges();
        // Product functions
        IEnumerable<Product> GetProducts();
        bool AddProduct(Product product);
        Product GetProductById(int id);
        bool DeleteProduct(int id);

        // Group functions
        string GetGroupName(int groupId);
        ProductGroup GetGroupById(int? groupId);

        // Order functions
        Order GetOrderByUserId(int userId);
        bool AddOrder(Order order);

        // OrderPosition functions
        bool AddOrderPositions(OrderPosition orderPosition);
        public IEnumerable<OrderPosition> GetAllOrderPositionsForOrder(int orderId);


        // Basket functions
        bool AddBasketPosition(BasketPosition basketPosition);
        IEnumerable<BasketPosition> GetBasketPositions();
        bool DeleteBasketPosition(int id);
        BasketPosition GetBasketPositiontById(int id);
        double getPriceOfProduct(int id);
        void RemoveRangeBasketPositions(IEnumerable<BasketPosition> basketPositions);

        // User functions
        User GetUserById(int userId);
    }
}
