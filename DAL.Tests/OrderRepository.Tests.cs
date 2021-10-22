using _3K1SPZ_CP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3K1SPZ_CP.DAL;
using Xunit;

namespace DAL.Tests
{
    public abstract class OrderRepositoryTestsBase : IDisposable
    {
        protected string _connectionString { get; set; }

        protected OrderRepositoryTestsBase()
        {
            _connectionString = "Server=DESKTOP-9GGT2EU;Database=3K1SPZ_Tests; Integrated Security=true";
        }
        public void Dispose()
        {
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            orderRepository.DeleteAll();
            UserRepository userRepository = new UserRepository(_connectionString);
            userRepository.DeleteAll();
        }
    }
    public class OrderRepositoryTests : OrderRepositoryTestsBase
    {
        [Fact]
        public void GetOrderHistory_Test()
        {
            OrderRepository orderRepository = new OrderRepository(_connectionString);
            UserRepository userRepository = new UserRepository(_connectionString);

        }
    }
}
