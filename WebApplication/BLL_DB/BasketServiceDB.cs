using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BLL_DB
{
    public class BasketServiceDB : IBasketService
    {
        private WebShopContext _context;

        public BasketServiceDB(WebShopContext context)
        {
            _context = context;
        }
        public bool ChangeAmountOfProductsInBasket(int basketPositionId, int amount)
        {
            if (amount > 0)
            {
                using (var connection = new SqlConnection(_context.connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("ChangeAmountOfProductsInBasket", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BasketPositionId", basketPositionId);
                        command.Parameters.AddWithValue("@Amount", amount);

                        int rowsAffected = (int)command.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            // Changes applied successfully
                            return true;
                        }
                        else
                        {
                            // No rows affected
                            Console.WriteLine("No rows affected.");
                            return false;
                        }
                    }
                }
            }
            else
            {
                // Invalid amount
                return false;
            }
        }


        public bool DeleteBasketPosition(int basketPositionId)
        {
            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM BasketPositions WHERE Id = @BasketPositionId";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BasketPositionId", basketPositionId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Basket position deleted successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Basket position not found or no rows affected.");
                        return false;
                    }
                }
            }
        }

        public bool GenerateBasketPosition(BasketAddRequestDTO basketDTO)
        {
            if (basketDTO.Amount <= 0)
            {
                Console.WriteLine("Amount must be a positive number.");
                return false;
            }

            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO BasketPositions (ProductId, UserId, Amount) VALUES (@ProductId, @UserId, @Amount)";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", basketDTO.ProductId);
                    command.Parameters.AddWithValue("@UserId", basketDTO.UserId);
                    command.Parameters.AddWithValue("@Amount", basketDTO.Amount);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Basket position added successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to add basket position.");
                        return false;
                    }
                }
            }
        }
    }
}