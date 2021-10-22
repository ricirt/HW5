using AutoMapper;
using Bugs_N_Roses.Application.Models.ProductModels;
using Bugs_N_Roses.Domain.ApplicationFilters;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_N_Roses.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public bool Add(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            _productRepository.Add(product);
            return true;
        }

        public bool Delete(int id)
        {
            _productRepository.Delete(id);
            return true;
        }

        public List<ProductDTO> GetAll(ApplicationParameters parameters)
        {
            var products = _productRepository.GetAll(parameters);
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return dtos;
        }

        public List<ProductDTO> GetByFilter(ProductParameters parameters)
        {
            var products = _productRepository.GetByFilter(parameters);
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return dtos;
        }

        public ProductDTO GetById(int id)
        {
            var product = _productRepository.Get(id);
            var mappedProduct = _mapper.Map<ProductDTO>(product);
            return mappedProduct;
        }

        public List<ProductDTO> SearchByName(ProductSearchParameters parameters)
        {
            var products = _productRepository.SearchByName(parameters);
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return dtos;
        }

        public bool Update(ProductUpdateDTO productUpdateDTO, int id)
        {
            var mappedProduct = _mapper.Map<Product>(productUpdateDTO);
            var updatedProduct = _productRepository.Get(id);

            updatedProduct.CategoryName = mappedProduct.CategoryName;
            updatedProduct.Price = mappedProduct.Price;
            updatedProduct.ProductName = mappedProduct.ProductName;
            updatedProduct.SKU = mappedProduct.SKU;
            updatedProduct.Stock = mappedProduct.Stock;
            _productRepository.Update(updatedProduct);
            return true;
        }
    }
}
