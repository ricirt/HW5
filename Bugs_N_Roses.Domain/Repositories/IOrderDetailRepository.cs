using Bugs_N_Roses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Domain.Repositories
{
    public interface IOrderDetailRepository
    {
        IList<OrderDetail> GetByUserId(int id);
        OrderDetail GetByOrderId(int id);
    }
}
