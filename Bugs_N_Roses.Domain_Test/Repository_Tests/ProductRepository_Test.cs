using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Bugs_N_Roses.Domain.Repositories;
using Bugs_N_Roses.Domain.Entities;
using System.Linq;

namespace Bugs_N_Roses.Domain_Test.Repository_Tests
{
    public class ProductRepository_Test
    {
        [Fact]
        public void GetAll_Return_ProductList()
        {

            //Arrange
            var mock = new Mock<IProductRepository>();
            var productList = GetAllProducts();
            mock.Setup(repository => repository.GetAll()).Returns(productList);
            IProductRepository productRepository = mock.Object;
            var product = productList[0];

            //Act
            var result = productRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(productList.Count, result.Count);
            Assert.True(!string.IsNullOrWhiteSpace(product.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(product.ProductName));
        }

        [Fact]
        public void GetAll_ThrowException()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            List<Product> productsList = new List<Product>();
            mock.Setup(repository => repository.GetAll()).Returns(() =>
            {
                if (productsList.Count == 0)
                {
                    throw new ApplicationException("Products not found");
                }
                else
                {
                    return productsList;
                }
            });
            IProductRepository productRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => productRepository.GetAll());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_Return_Product(int id)
        {
            var mock = new Mock<IProductRepository>();
            var productList = GetAllProducts();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns(() =>
            {
                var product = productList.FirstOrDefault(p => p.Id == id);
                return product;
            });
            IProductRepository productRepository = mock.Object;

            //Act
            var result = productRepository.Get(id);

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(result.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(result.ProductName));
        }

        [Fact]
        public void Get_ThrowException()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            var productList = GetAllProducts();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var product = productList.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    throw new ApplicationException("not found");
                }
                else
                {
                    return product;
                }
            });
            IProductRepository productRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => productRepository.Get(productList.Count + 1));
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            var productList = GetAllProducts();
            int productListCount = productList.Count;
            mock.Setup(repository => repository.Add(It.IsAny<Product>()));
            Product product = new Product
            {
                Id = productListCount,
                ProductName = $"{productListCount} ProductName",
                CategoryName = $"{productListCount} CategoryName",
                Price = productListCount,
                SKU = "123456",
                Stock = productListCount
            };
            productList.Add(product);
            IProductRepository productRepository = mock.Object;

            //Act
            productRepository.Add(product);

            //Assert
            Assert.True(productListCount < productList.Count);
            Assert.NotNull(product);
            Assert.True(!string.IsNullOrWhiteSpace(product.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(product.ProductName));
        }

        [Fact]
        public void Add_ThrowException()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();

            Product product = new Product();
            mock.Setup(repository => repository.Add(It.IsAny<Product>())).Callback(() =>
            {
                if (product.ProductName == null)
                {
                    throw new ApplicationException("Product dont added.");
                }
                else
                {
                    return;
                }
            });
            IProductRepository productRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => productRepository.Add(product));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Update(int id)
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            var productList = GetAllProducts();
            mock.Setup(repository => repository.Update(It.IsAny<Product>()));
            Product product = new Product
            {
                Id = id,
                ProductName = $"{id} ProductName",
                CategoryName = $"{id} CategoryName",
                Price = id * 10,
                SKU = "123456",
                Stock = id * 5
            };
            var updatedProduct = productList.FirstOrDefault(p => p.Id == product.Id);
            updatedProduct = product;
            IProductRepository productRepository = mock.Object;

            //Act
            productRepository.Update(product);

            //Assert
            Assert.Same(product, updatedProduct);

        }

        [Fact]
        public void Update_ThrowException()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            Product product = new Product();
            mock.Setup(repository => repository.Update(It.IsAny<Product>())).Callback(() =>
            {
                if (product.ProductName == null)
                {
                    throw new ApplicationException("Product dont be updated.");
                }
                else
                {
                    return;
                }
            });
            IProductRepository productRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => productRepository.Update(product));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete(int id)
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            var list = GetAllProducts();
            int productCount = list.Count;
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Callback(() =>
            {

                var product = list.FirstOrDefault(p => p.Id == id);
                list.Remove(product);


            });

            IProductRepository productRepository = mock.Object;

            //Act
            productRepository.Delete(id);

            //Assert
            Assert.True(productCount > list.Count);
        }


        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < 5; i++)
            {
                Product product = new Product();
                product.Id = i;
                product.ProductName = $"{i} ProductName";
                product.CategoryName = $"{i} CategoryName";
                product.Price = i * 10;
                product.SKU = "123456";
                product.Stock = i * 5;

                products.Add(product);
            }
            return products;
        }
    }
}
