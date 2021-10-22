using Bugs_N_Roses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
        Order Get(int id);
        IList<Order> GetAll();
    }
}
