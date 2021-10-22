using Bugs_N_Roses.Application.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.OrderServices
{
    public interface IOrderService
    {
        OrderDTO GetById(int id);
        List<OrderDTO> GetAll();
        bool Add(OrderCreateDTO orderCreateDTO);
        bool Update(OrderUpdateDTO orderUpdateDTO, int id);
        bool Delete(int id);
    }
}
