using AutoMapper;
using Bugs_N_Roses.Application.Models.OrderDetailModels;
using Bugs_N_Roses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        
        public OrderDetailDTO GetByOrderId(int id)
        {
            var orderDetail = _orderDetailRepository.GetByOrderId(id);
            var mappedOrderDetail = _mapper.Map<OrderDetailDTO>(orderDetail);
            return mappedOrderDetail;
        }

        public List<OrderDetailDTO> GetByUserId(int id)
        {
            var orderDetails = _orderDetailRepository.GetByUserId(id);
            var dtos = _mapper.Map<List<OrderDetailDTO>>(orderDetails);
            return dtos;
        }
    }
}
