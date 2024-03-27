using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Model.Model;
using System.Data.SqlClient;
using System.Data;

namespace BLL_DB
{
    public class ProductServiceDB : IProductsService
    {
        private WebShopContext _context;

        public ProductServiceDB(WebShopContext context)
        {
            _context = context;
        }
        public bool ActivateProduct(int productId)
        {
            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                // Prepare SQL statement to update the IsActive flag
                string sql = "UPDATE Products SET IsActive = 1 WHERE Id = @ProductId";

                using (var command = new SqlCommand(sql, connection))
                {
                    // Add parameter for productId
                    command.Parameters.AddWithValue("@ProductId", productId);

                    // Execute the SQL command
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Product not found or no rows affected
                        Console.WriteLine("Product not found or no rows affected.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Product activated successfully.");
                        return true;
                    }
                }
            }
        }

        public bool AddNewProduct(ProductAddRequestDTO productDTO)
        {
            // Check if productDTO is null
            if (productDTO == null)
            {
                return false;
            }

            // Prepare SQL statement to insert a new product
            string sql = @"
        INSERT INTO Products (Name, Price, IsActive, GroupId)
        VALUES (@Name, @Price, @IsActive, @GroupId);
    ";

            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                // Create a new SqlCommand with the SQL statement and connection
                using (var command = new SqlCommand(sql, connection))
                {

                    // Add parameters for the product properties
                    command.Parameters.AddWithValue("@Name", productDTO.Name);
                    command.Parameters.AddWithValue("@Price", productDTO.Price);
                    command.Parameters.AddWithValue("@IsActive", true);
                    command.Parameters.AddWithValue("@GroupId", productDTO.GroupId);

                    try
                    {
                        // Execute the SQL command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the product was successfully inserted
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Product inserted successfully.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Product not inserted.");
                            return false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle SQL exceptions
                        Console.WriteLine("An error occurred: " + ex.Message);
                        return false;
                    }
                }
            }
        }



        public bool DeactivateProduct(int productId)
        {
            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                // Prepare SQL statement
                string sql = "UPDATE dbo.Products SET IsActive = 0 WHERE Id = @ProductId";

                using (var command = new SqlCommand(sql, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ProductId", productId);

                    // Execute the SQL command
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {

                        // Product not found or no rows affected
                        Console.WriteLine("Product not found or no rows affected.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Product deactivated successfully.");
                        return true;
                    }
                }
            }
        }

        public IEnumerable<ProductsListResponseDTO> GetProductsList(ProductListRequestDTO request)
        {
            var orderBy = request.orderBy ?? "Id"; // Default orderBy to Id if not specified
            var orderAscending = request.orderAscending ?? true; // Default orderAscending to true if not specified

            var results = new List<Dictionary<string, object>>();

            // Establish connection to the database
            using (var connection = new SqlConnection(_context.connectionString))
            {
                connection.Open();

                // Create a command to execute the stored procedure
                using (var command = new SqlCommand("GetFilteredProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@orderBy", orderBy);
                    command.Parameters.AddWithValue("@orderAscending", orderAscending);
                    command.Parameters.AddWithValue("@getNotActive", request.getNotActive);
                    command.Parameters.AddWithValue("@filterByName", request.filterByName);
                    command.Parameters.AddWithValue("@filterByGroupName", request.filterByGroupName);
                    command.Parameters.AddWithValue("@filterByGroupId", request.filterByGroupId);

                    // Execute the command and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            results.Add(row);
                        }
                    }
                }
            }

            // Map the results to ProductsListResponseDTO
            var mappedResults = results.Select(r => new ProductsListResponseDTO
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"],
                Price = (double)r["Price"],
                IsActive = (bool)r["IsActive"],
                GroupId = r["GroupId"] != DBNull.Value ? (int)r["GroupId"] : null,
                GroupName = r["GroupName"] != DBNull.Value ? (string)r["GroupName"] : null
            }).ToList();

            return mappedResults;
        }
    }
}
