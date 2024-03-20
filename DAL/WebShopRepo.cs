using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class WebShopRepo : IWebShopRepo
    {

        private WebShopContext _context;

        public WebShopRepo(WebShopContext context)
        {
            _context = context;
        }

        public string GetGroupName(int groupId)
        {
            if (groupId < 0)
            {
                return "";
            }

            var group = _context.ProductGroups.First(pg => pg.Id == groupId);


            return group.ParentId != null ?
                GetGroupName(group.ParentId.Value) + " / " + group.Name :
                group.Name;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public bool AddProduct(Product product)
        {
            bool success = _context.Products.Add(product) != null;

            _context.SaveChanges();
            return success;
        }

        public ProductGroup GetGroupById(int? groupId)
        {
            return _context.ProductGroups.FirstOrDefault(pg => pg.Id == groupId);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool DeleteProduct(int id)
        {
            bool success = _context.Products.Remove(GetProductById(id)) != null;

            _context.SaveChanges();

            return success;
        }

        public void ContextSaveChanges()
        {
            _context.SaveChanges();
        }

        public Order GetOrderByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public bool AddBasketPosition(BasketPosition basketPosition)
        {
            bool success = _context.BasketPositions.Add(basketPosition) != null;
            _context.SaveChanges();
            return success;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(p => p.Id == userId);
        }

        public IEnumerable<BasketPosition> GetBasketPositions()
        {
            return _context.BasketPositions.ToList();
        }

        public bool DeleteBasketPosition(int id)
        {
            bool success = _context.BasketPositions.Remove(GetBasketPositiontById(id)) != null;

            _context.SaveChanges();

            return success;
        }

        public BasketPosition GetBasketPositiontById(int id)
        {
            return _context.BasketPositions.FirstOrDefault(p => p.Id == id);
        }

        public bool AddOrder(Order order)
        {
            bool success = _context.Orders.Add(order) != null;
            _context.SaveChanges();
            return success;
        }

        public double getPriceOfProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                return product.Price;
            }
            else
            {
                return 0;
            }
        }

        public bool AddOrderPositions(OrderPosition orderPosition)
        {
            bool success = _context.OrderPositions.Add(orderPosition) != null;
            return success;
        }

        public void RemoveRangeBasketPositions(IEnumerable<BasketPosition> basketPositions)
        {
            _context.BasketPositions.RemoveRange(basketPositions);
            _context.SaveChanges();
        }

        public IEnumerable<OrderPosition> GetAllOrderPositionsForOrder(int orderId)
        {
            return _context.OrderPositions.Where(op => op.OrderId == orderId).ToList();
        }
    }
}
