using Bugs_N_Roses.Domain.ApplicationFilters;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Add(Product product)
        {
            var sql = "INSERT INTO Products (ProductName,SKU,Price,Stock,CategoryName) VALUES (@ProductName,@SKU,@Price,@Stock,@CategoryName)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, product);

                if (product == null)
                {
                    throw new ApplicationException("Product dont added.");
                }
                else
                {
                    return;
                }
            }
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, new { Id = id});
            }
        }

        public Product Get(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });

                if (result == null)
                {
                    throw new ApplicationException("Product not found.");
                }
                else
                {
                    return result;
                }
                
            }
        }

        public IList<Product> GetAll(ApplicationParameters parameters)
        {
            
            var sql = $"SELECT * FROM Products ORDER BY Id OFFSET {parameters.PageSize * (parameters.PageNumber - 1)} ROWS FETCH NEXT {parameters.PageSize} ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.Query<Product>(sql);

                if (result == null)
                {
                    throw new ApplicationException("Products not found");
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public IList<Product> GetByFilter(ProductParameters parameters)
        {
            

            var sql = $"SELECT * FROM Products WHERE Price BETWEEN {parameters.MinPrice} AND {parameters.MaxPrice} ORDER BY Id OFFSET {parameters.PageSize * (parameters.PageNumber - 1)} ROWS FETCH NEXT {parameters.PageSize} ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.Query<Product>(sql);

                if (result == null)
                {
                    throw new ApplicationException("Products not found");
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public IList<Product> SearchByName(ProductSearchParameters parameters)
        {
            var sql = $"SELECT * FROM Products WHERE ProductName LIKE '%{parameters.SearchString}%' ORDER BY Id OFFSET {parameters.PageSize * (parameters.PageNumber - 1)} ROWS FETCH NEXT {parameters.PageSize} ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.Query<Product>(sql);

                if (result == null)
                {
                    throw new ApplicationException("Products not found");
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        public void Update(Product product)
        {
            var sql = "UPDATE Products SET ProductName = @ProductName, SKU = @SKU, Price = @Price, Stock = @Stock, CategoryName = @CategoryName  WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, product);

                if (product == null)
                {
                    throw new ApplicationException("Product dont updated.");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
