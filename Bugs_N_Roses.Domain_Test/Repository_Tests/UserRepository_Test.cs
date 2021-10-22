using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Bugs_N_Roses.Domain.Entities;
using Bugs_N_Roses.Domain.Repositories;

namespace Bugs_N_Roses.Domain_Test.Repository_Tests
{
    public class UserRepository_Test
    {
        [Fact]
        public void GetAll_Return_UserList()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var userList = GetAllUsers();
            mock.Setup(repository => repository.GetAll()).Returns(userList);
            IUserRepository userRepository = mock.Object;
            var user = userList[0];

            //Act
            var result = userRepository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(userList.Count, result.Count);
            Assert.True(!string.IsNullOrWhiteSpace(user.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(user.FirstName));
            Assert.True(!string.IsNullOrWhiteSpace(user.LastName));
        }

        [Fact]
        public void GetAll_ThrowException()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            List<User> usersList = new List<User>();
            mock.Setup(repository => repository.GetAll()).Returns(() =>
            {
                if (usersList.Count == 0)
                {
                    throw new ApplicationException("Users not found");
                }
                else
                {
                    return usersList;
                }
            });
            IUserRepository userRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => userRepository.GetAll());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_Return_User(int id)
        {
            var mock = new Mock<IUserRepository>();
            var userList = GetAllUsers();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns(() =>
            {
                var user = userList.FirstOrDefault(u => u.Id == id);
                return user;
            });
            IUserRepository userRepository = mock.Object;

            //Act
            var result = userRepository.Get(id);

            //Assert
            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(result.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(result.FirstName));
            Assert.True(!string.IsNullOrWhiteSpace(result.LastName));

        }

        [Fact]
        public void Get_ThrowException()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var userList = GetAllUsers();
            mock.Setup(repository => repository.Get(It.IsAny<int>())).Returns((int id) =>
            {
                var user = userList.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }
                else
                {
                    return user;
                }
            });
            IUserRepository userRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => userRepository.Get(userList.Count + 1));
        }

        [Fact]
        public void Add()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var userList = GetAllUsers();
            int userListCount = userList.Count;
            mock.Setup(repository => repository.Add(It.IsAny<User>()));
            User user = new User
            {
                Id = userListCount,
                FirstName = $"{userListCount} FirstName",
                LastName = $"{userListCount} LastName",
                Adress = $"{userListCount} Adress",
                Email = $"{userListCount} Email",
                PhoneNumber = $"{userListCount} PhoneNumber",
                IsActive = true
            };
            userList.Add(user);
            IUserRepository userRepository = mock.Object;

            //Act
            userRepository.Add(user);

            //Assert
            Assert.True(userListCount < userList.Count);
            Assert.NotNull(user);
            Assert.True(!string.IsNullOrWhiteSpace(user.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(user.FirstName));
            Assert.True(!string.IsNullOrWhiteSpace(user.LastName));

        }

        [Fact]
        public void Add_ThrowException()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();

            User user = new User();
            mock.Setup(repository => repository.Add(It.IsAny<User>())).Callback(() =>
            {
                if (user.FirstName == null)
                {
                    throw new ApplicationException("User dont added.");
                }
                else
                {
                    return;
                }
            });
            IUserRepository userRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => userRepository.Add(user));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Update(int id)
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var userList = GetAllUsers();
            mock.Setup(repository => repository.Update(It.IsAny<User>()));
            User user = new User
            {
                Id = id,
                FirstName = $"{id} FirstName",
                LastName = $"{id} LastName",
                Email = $"{id} Email",
                Adress = $"{id} Adress",
                PhoneNumber = $"{id} PhoneNumber",
                IsActive = true
            };
            var updatedUser = userList.FirstOrDefault(p => p.Id == user.Id);
            updatedUser = user;
            IUserRepository userRepository = mock.Object;

            //Act
            userRepository.Update(user);

            //Assert
            Assert.Same(user, updatedUser);

        }

        [Fact]
        public void Update_ThrowException()
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            User user = new User();
            mock.Setup(repository => repository.Update(It.IsAny<User>())).Callback(() =>
            {
                if (user.FirstName == null)
                {
                    throw new ApplicationException("User dont be updated.");
                }
                else
                {
                    return;
                }
            });
            IUserRepository userRepository = mock.Object;

            //Assert
            Assert.Throws<ApplicationException>(() => userRepository.Update(user));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete(int id)
        {
            //Arrange
            var mock = new Mock<IUserRepository>();
            var list = GetAllUsers();
            int userCount = list.Count;
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Callback(() =>
            {

                var user = list.FirstOrDefault(p => p.Id == id);
                list.Remove(user);


            });

            IUserRepository userRepository = mock.Object;

            //Act
            userRepository.Delete(id);

            //Assert
            Assert.True(userCount > list.Count);
        }


        private List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                User user = new User();
                user.Id = i;
                user.FirstName = $"{i} FirstName";
                user.LastName = $"{i} LastName";
                user.PhoneNumber = $"{i} PhoneNumber";
                user.Email = $"{i}gmail.com";
                user.Adress = $"{i} Adress";
                user.IsActive = true;
                

                users.Add(user);
            }
            return users;
        }
    }
}
