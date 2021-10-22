using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bugs_N_Roses.Domain_Test.Repository_Tests
{
    public class OrderRepository_Test
    {
        [Fact]
        public void GetAll_Return_OrderList()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var orderList = GetAllOrders();
            mock.Setup(repository => repository.GetAll()).Returns(orderList);
            IOrderRepository orderRepository = mock.Object;
            var order = orderList[0];

            //Act
            var result = orderRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(orderList.Count, result.Count);
            Assert.True(!string.IsNullOrWhiteSpace(order.Id.ToString()));
        }

        [Fact]
        public void GetAll_ThrowException()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            List<Order> orderList = new List<Order>();
            mock.Setup(repository => repository.GetAll()).Returns(() =>
            {
                if (orderList.Count == 0)
                {
                    throw new ApplicationException("Orders not found");
                }
                else
                {
                    return orderList;
                }
            });
            IOrderRepository orderRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => orderRepository.GetAll());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_Return_Order(int id)
        {
            var mock = new Mock<IOrderRepository>();
            var orderList = GetAllOrders();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns(() =>
            {
                var order = orderList.FirstOrDefault(p => p.Id == id);
                return order;
            });
            IOrderRepository orderRepository = mock.Object;

            //Act
            var result = orderRepository.Get(id);

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(result.Id.ToString()));
        }

        [Fact]
        public void Get_ThrowException()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var orderList = GetAllOrders();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var order = orderList.FirstOrDefault(p => p.Id == id);

                if (order == null)
                {
                    throw new ApplicationException("Order not found");
                }
                else
                {
                    return order;
                }
            });
            IOrderRepository orderRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => orderRepository.Get(orderList.Count + 1));
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var orderList = GetAllOrders();
            int orderListCount = orderList.Count;
            mock.Setup(repository => repository.Add(It.IsAny<Order>()));
            Order order = new Order
            {
                Id = orderListCount,
                ProductId = orderListCount,
                Quantity = orderListCount,
                UserId = orderListCount
            };
            orderList.Add(order);
            IOrderRepository orderRepository = mock.Object;

            //Act
            orderRepository.Add(order);

            //Assert
            Assert.True(orderListCount < orderList.Count);
            Assert.NotNull(order);
            Assert.True(!string.IsNullOrWhiteSpace(order.Id.ToString()));
        }

        [Fact]
        public void Add_ThrowException()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();

            Order order = new Order();
            mock.Setup(repository => repository.Add(It.IsAny<Order>())).Callback(() =>
            {
                if (order.ProductId == 0)
                {
                    throw new ApplicationException("Order dont added.");
                }
                else
                {
                    return;
                }
            });
            IOrderRepository orderRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => orderRepository.Add(order));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Update(int id)
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var orderList = GetAllOrders();
            mock.Setup(repository => repository.Update(It.IsAny<Order>()));
            Order order = new Order
            {
                Id = id,
                ProductId = id,
                UserId = id,
                Quantity = id * 10
            };
            var updatedOrder = orderList.FirstOrDefault(p => p.Id == order.Id);
            updatedOrder = order;
            IOrderRepository orderRepository = mock.Object;

            //Act
            orderRepository.Update(order);

            //Assert
            Assert.Same(order, updatedOrder);

        }

        [Fact]
        public void Update_ThrowException()
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            Order order = new Order();
            mock.Setup(repository => repository.Update(It.IsAny<Order>())).Callback(() =>
            {
                if (order.ProductId == 0)
                {
                    throw new ApplicationException("Order dont be updated.");
                }
                else
                {
                    return;
                }
            });
            IOrderRepository orderRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => orderRepository.Update(order));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete(int id)
        {
            //Arrange
            var mock = new Mock<IOrderRepository>();
            var list = GetAllOrders();
            int orderCount = list.Count;
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Callback(() =>
            {

                var order = list.FirstOrDefault(p => p.Id == id);
                list.Remove(order);


            });

            IOrderRepository orderRepository = mock.Object;

            //Act
            orderRepository.Delete(id);

            //Assert
            Assert.True(orderCount > list.Count);
        }


        private List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 5; i++)
            {
                Order order = new Order();
                order.Id = i;
                order.ProductId = i + 1;
                order.Quantity = i;
                order.UserId = i + 2;

                orders.Add(order);
            }
            return orders;
        }
    }
}
