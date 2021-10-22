using Bugs_N_Roses.Application.Services.OrderDetailServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bugs_N_Roses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            var result = _orderDetailService.GetByUserId(id);

            return Ok(result);
        }

        [HttpGet("getbyorderid")]
        public IActionResult GetByOrderId(int id)
        {
            var result = _orderDetailService.GetByOrderId(id);

            return Ok(result);
        }

    }
}
