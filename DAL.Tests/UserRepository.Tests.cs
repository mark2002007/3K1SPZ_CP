using _3K1SPZ_CP.DAL;
using System;
using System.Linq;
using _3K1SPZ_CP;
using Xunit;

namespace DAL.Tests
{
    public abstract class UserRepositoryTestsBase : IDisposable
    {
        protected string _connectionString { get; set; }

        protected UserRepositoryTestsBase()
        {
            _connectionString = "Server=DESKTOP-9GGT2EU;Database=3K1SPZ_Tests; Integrated Security=true";
        }
        public void Dispose()
        {
            UserRepository userRepository = new UserRepository(_connectionString);
            userRepository.DeleteAll();
        }
    }

    public class UserRepositoryTests : UserRepositoryTestsBase
    {
        [Fact]
        public void GetId_Test()
        {
            //Arrange
            UserRepository userRepository = new UserRepository(_connectionString);
            UserDTO expected = new UserDTO()
            {
                Login = "mark2002007",
                Password = "1111",
                DispName = "MarKson"
            };
            userRepository.Create(expected);
            //Act
            UserDTO actual = userRepository.Get(userRepository.GetAll().SingleOrDefault().Id);
            //Assert
            Assert.Equal(actual.Login, expected.Login);
            Assert.Equal(actual.Password, expected.Password);
            Assert.Equal(actual.DispName, expected.DispName);
        }
        [Fact]
        public void GetLogin_Test()
        {
            //Arrange
            UserRepository userRepository = new UserRepository(_connectionString);
            UserDTO expected = new UserDTO()
            {
                Login = "mark2002007",
                Password = "1111",
                DispName = "MarKson"
            };
            userRepository.Create(expected);
            //Act
            UserDTO actual = userRepository.Get(userRepository.GetAll().SingleOrDefault().Login);
            //Assert
            Assert.Equal(actual.Login, expected.Login);
            Assert.Equal(actual.Password, expected.Password);
            Assert.Equal(actual.DispName, expected.DispName);
        }
        [Fact]
        public void UpdateDispName_Test()
        {
            //Arrange
            UserRepository userRepository = new UserRepository(_connectionString);
            userRepository.Create(new UserDTO()
            {
                Login = "mark2002007",
                Password = "1111",
                DispName = "MarKson"
            });
            UserDTO expected = new UserDTO
            {
                Login = "mark2002007",
                Password = "1111",
                DispName = "markson"
            };
            //Act
            bool isTrue =
                userRepository.UpdateDispName(userRepository.GetAll().SingleOrDefault().Login, expected.DispName);
            UserDTO actual = userRepository.GetAll().SingleOrDefault();
            //Assert
            Assert.True(isTrue);
            Assert.Equal(actual.Login,expected.Login);
            Assert.Equal(actual.Password, expected.Password);
            Assert.Equal(actual.DispName, expected.DispName);
        }
        [Fact]
        public void UpdatePassword_Test()
        {
            //Arrange
            UserRepository userRepository = new UserRepository(_connectionString);
            userRepository.Create(new UserDTO()
            {
                Login = "mark2002007",
                Password = "1111",
                DispName = "MarKson"
            });
            UserDTO expected = new UserDTO
            {
                Login = "mark2002007",
                Password = "1234",
                DispName = "MarKson"
            };
            //Act
            bool isTrue =
                userRepository.UpdatePassword(userRepository.GetAll().SingleOrDefault().Login, expected.Password);
            UserDTO actual = userRepository.GetAll().SingleOrDefault();
            //Assert
            Assert.True(isTrue);
            Assert.Equal(actual.Login,expected.Login);
            Assert.Equal(actual.Password, expected.Password);
            Assert.Equal(actual.DispName, expected.DispName);
        }
    }
}