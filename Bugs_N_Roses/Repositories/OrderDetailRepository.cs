using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Infrastructure.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly IConfiguration _configuration;

        public OrderDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public OrderDetail GetByOrderId(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("MongoDb"));
            var mongoDb = client.GetDatabase("HW4Mongo");
            var collection = mongoDb.GetCollection<OrderDetail>("OrderDetails");
            var orderdetail = collection.Find(x => x.OrderId == id).ToList().SingleOrDefault();
            return orderdetail;
        }

        public IList<OrderDetail> GetByUserId(int id)
        {
            MongoClient client = new MongoClient(_configuration.GetConnectionString("MongoDb"));
            var mongoDb = client.GetDatabase("HW4Mongo");
            var collection = mongoDb.GetCollection<OrderDetail>("OrderDetails");
            var orderdetail = collection.Find(x => x.UserId == id).ToList();
            return orderdetail;
        }
    }
}
