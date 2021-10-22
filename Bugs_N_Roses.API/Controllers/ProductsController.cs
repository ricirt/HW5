using AutoMapper;
using Bugs_N_Roses.Application.Models.ProductModels;
using Bugs_N_Roses.Application.Services.ProductServices;
using Bugs_N_Roses.Domain.ApplicationFilters;
using Bugs_N_Roses.Domain.Entities;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]ApplicationParameters parameters)
        {
            var result = _productService.GetAll(parameters);

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDTO productCreateDTO)
        {
            
            var result = _productService.Add(productCreateDTO);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            var result = _productService.Update(productUpdateDTO, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            return Ok(result);
        }

        [HttpGet("getbyfilter")]
        public IActionResult GetByFilter([FromQuery] ProductParameters parameters)
        {
            var result = _productService.GetByFilter(parameters);

            return Ok(result);
        }

        [HttpGet("searchbyname")]
        public IActionResult SearchByName([FromQuery] ProductSearchParameters parameters)
        {
            var result = _productService.SearchByName(parameters);

            return Ok(result);
        }

    }
}
