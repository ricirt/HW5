using Bugs_N_Roses.Application.Models.OrderDetailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.OrderDetailServices
{
    public interface IOrderDetailService
    {
        List<OrderDetailDTO> GetByUserId(int id);
        OrderDetailDTO GetByOrderId(int id);
    }
}
