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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Add(User user)
        {
            var sql = "INSERT INTO Users (FirstName,LastName,Adress,Email,PhoneNumber,IsActive) VALUES (@FirstName,@LastName,@Adress,@Email,@PhoneNumber,@IsActive)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, user);
            }
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, new { Id = id });
            }
        }

        public User Get(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<User>(sql, new { Id = id });
                return result;
            }
        }

        public IList<User> GetAll()
        {
            var sql = "SELECT * FROM Users";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = connection.Query<User>(sql);
                return result.ToList();
            }
        }

        public void Update(User user)
        {
            
            var sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Adress = @Adress, Email = @Email, PhoneNumber = @PhoneNumber, IsActive = @IsActive  WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                connection.Execute(sql, user);
            }
        }
    }
}
