using AutoMapper;
using Bugs_N_Roses.Application.Models.OrderModels;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public bool Add(OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<Order>(orderCreateDTO);
            _orderRepository.Add(order);
            return true;
        }

        public bool Delete(int id)
        {
            _orderRepository.Delete(id);
            return true;
        }

        public List<OrderDTO> GetAll()
        {
            var orders = _orderRepository.GetAll();
            var dtos = _mapper.Map<List<OrderDTO>>(orders);
            return dtos;
        }

        public OrderDTO GetById(int id)
        {
            var order = _orderRepository.Get(id);
            var mappedOrder = _mapper.Map<OrderDTO>(order);
             return mappedOrder;
        }

        public bool Update(OrderUpdateDTO orderUpdateDTO, int id)
        {
            var mappedOrder = _mapper.Map<Order>(orderUpdateDTO);
            var updatedOrder = _orderRepository.Get(id);

            updatedOrder.ProductId = mappedOrder.ProductId;
            updatedOrder.Quantity = mappedOrder.Quantity;
            updatedOrder.UserId = mappedOrder.UserId;
            _orderRepository.Update(updatedOrder);
            return true;
        }
    }
}
