using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Persistences;
using Dapper;
using Moq.Dapper;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Api_SalesDatePrediction.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly Mock<IDbTransaction> _mockTransaction;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c.GetConnectionString("DefaultConnection")).Returns("FakeConnectionString");

            _mockDbConnection = new Mock<IDbConnection>();
            _mockTransaction = new Mock<IDbTransaction>();

            // Se pasa la conexión simulada al repositorio
            _orderRepository = new OrderRepository(_mockDbConnection.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldInsertOrderAndDetails()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustID = 1,
                EmpID = 2,
                OrderDate = DateTime.UtcNow,
                RequiredDate = DateTime.UtcNow.AddDays(7),
                ShippedDate = null,
                ShipperID = 3,
                Freight = 10.5m,
                ShipName = "Test Ship",
                ShipAddress = "123 Test St",
                ShipCity = "Test City",
                ShipRegion = "TR",
                ShipPostalCode = "12345",
                ShipCountry = "Testland",
                OrderDetails = new List<OrderDetailDto>
                {
                    new OrderDetailDto { ProductID = 1, UnitPrice = 20, Qty = 2, Discount = 0 },
                    new OrderDetailDto { ProductID = 2, UnitPrice = 15, Qty = 1, Discount = 0 }
                }
            };

            _mockDbConnection.Setup(c => c.BeginTransaction()).Returns(_mockTransaction.Object);

            _mockDbConnection.SetupDapperAsync(c =>
                c.ExecuteScalarAsync<int>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
                .ReturnsAsync(1001); // Simula la inserción y devuelve un ID de orden

            _mockDbConnection.SetupDapperAsync(c =>
                c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
                .ReturnsAsync(1); // Simula la inserción de detalles de la orden

            // Act
            var result = await _orderRepository.CreateOrderAsync(orderDto);

            // Assert
            Assert.Equal(1001, result);

            _mockDbConnection.Verify(c => c.BeginTransaction(), Times.Once);
            _mockDbConnection.Verify(c => c.ExecuteScalarAsync<int>(It.IsAny<string>(), It.IsAny<object>(), _mockTransaction.Object, null, null), Times.Once);
            _mockDbConnection.Verify(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), _mockTransaction.Object, null, null), Times.Exactly(orderDto.OrderDetails.Count));
            _mockTransaction.Verify(t => t.Commit(), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldRollbackOnError()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustID = 1,
                EmpID = 2,
                OrderDate = DateTime.UtcNow,
                RequiredDate = DateTime.UtcNow.AddDays(7),
                OrderDetails = new List<OrderDetailDto>
                {
                    new OrderDetailDto { ProductID = 1, UnitPrice = 20, Qty = 2, Discount = 0 }
                }
            };

            _mockDbConnection.Setup(c => c.BeginTransaction()).Returns(_mockTransaction.Object);
            _mockDbConnection.SetupDapperAsync(c =>
                c.ExecuteScalarAsync<int>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), null, null))
                .ThrowsAsync(new Exception("DB Error")); // Simular error en la base de datos

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderRepository.CreateOrderAsync(orderDto));

            _mockTransaction.Verify(t => t.Rollback(), Times.Once);
        }
    }
}
