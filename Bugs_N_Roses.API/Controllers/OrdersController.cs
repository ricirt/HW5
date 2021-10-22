using Bugs_N_Roses.Application.Models.OrderModels;
using Bugs_N_Roses.Application.Services.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bugs_N_Roses.API.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrders()
        {
            var orders=_orderService.GetAll();
            return Ok(orders);
        }
        
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetOrder(int orderId)
        {
            var order=_orderService.GetById(orderId);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateDTO orderCreateDTO)
        {

            var result = _orderService.Add(orderCreateDTO);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            var result = _orderService.Update(orderUpdateDTO, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _orderService.Delete(id);
            return Ok(result);
        }


    }
}