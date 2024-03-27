using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DB
{
    public class OrderServiceDB : IOrderService
    {
        private WebShopContext _context;

        public OrderServiceDB(WebShopContext context)
        {
            _context = context;
        }
        public OrderGeneratedResponseDTO GenerateOrder(int userId)
        {
            OrderGeneratedResponseDTO orderResponse = null;

            using (SqlConnection connection = new SqlConnection(_context.connectionString))
            {
                using (SqlCommand command = new SqlCommand("GenerateOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Dodajemy parametr @UserId
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orderResponse = new OrderGeneratedResponseDTO
                            {
                                Id = Convert.ToInt32(reader["OrderId"]),
                                UserId = userId,
                                OrderPositions = new List<OrderPositionResponseDTO>()
                            };

                            do
                            {
                                OrderPositionResponseDTO orderPosition = new OrderPositionResponseDTO
                                {
                                    Id = Convert.ToInt32(reader["OrderPositionId"]),
                                    OrderId = orderResponse.Id,
                                    Amount = Convert.ToInt32(reader["Amount"]),
                                    Price = Convert.ToDouble(reader["Price"])
                                };

                                orderResponse.OrderPositions.Add(orderPosition);
                            } while (reader.Read());
                        }
                    }
                }
            }

            return orderResponse;
        }



        public bool PayForOrder(double value)
        {
            throw new NotImplementedException();
        }
    }
}
