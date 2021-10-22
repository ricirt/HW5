using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Add(Order order)
        {
            var sql = "INSERT INTO Orders (ProductId,UserId,Quantity,OrderDate) VALUES (@ProductId,@UserId,@Quantity,@OrderDate)";
            var orderLastQuery = $"SELECT * FROM Orders WHERE UserId = {order.UserId}";
            var productQuery = $"SELECT * FROM Products WHERE Id = {order.ProductId}";
            var userQuery = $"SELECT * FROM Users WHERE Id = {order.UserId}";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, order);
                var orderResult = connection.Query<Order>(orderLastQuery).LastOrDefault();
                if (orderResult == null)
                {
                    return;
                }
                var productResult = connection.Query<Product>(productQuery).FirstOrDefault();
                var userResult = connection.Query<User>(userQuery).FirstOrDefault();
                MongoClient client = new MongoClient(_configuration.GetConnectionString("MongoDb"));
                var mongoDb = client.GetDatabase("HW4Mongo");
                var collection = mongoDb.GetCollection<OrderDetail>("OrderDetails");

                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderResult.Id,
                    UserId = userResult.Id,
                    ProductId = productResult.Id,
                    ProductName = productResult.ProductName,
                    UserFirstName = userResult.FirstName,
                    UserLastName = userResult.LastName,
                    TotalPrice = productResult.Price * order.Quantity,
                    OrderDate = order.OrderDate
                };
                collection.InsertOne(orderDetail);
                
            }

        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Orders WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, new { Id = id });
            }
        }

        public Order Get(int id)
        {
            var sql = "SELECT * FROM Orders WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { Id = id });
                return result;
            }
        }

        public IList<Order> GetAll()
        {
            var sql = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.Query<Order>(sql);
                return result.ToList();
            }
        }

        public void Update(Order order)
        {
            var sql = "UPDATE Orders SET ProductId = @ProductId, UserId = @UserId, Quantity = @Quantity, OrderDate = @OrderDate WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, order);
            }
        }
    }
}
