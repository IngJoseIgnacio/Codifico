using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_SalesDatePrediction.Dtos;
using Api_SalesDatePrediction.Interfaces;
using Api_SalesDatePrediction.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace Api_SalesDatePrediction.Persistences
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string _connectionString;

        public System.Data.IDbConnection Object { get; }

        public OrderRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public OrderRepository(System.Data.IDbConnection @object)
        {
            Object = @object;
        }

        public async Task<IEnumerable<OrdersDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.Custid == customerId) // Filtrar por cliente
                .OrderBy(o => o.Orderid) // Ordenar por OrderID
                .Select(o => new OrdersDto
                {
                    OrderId = o.Orderid,
                    RequiredDate = o.Requireddate,
                    ShippedDate = o.Shippeddate,
                    ShipName = o.Shipname,
                    ShipAddress = o.Shipaddress,
                    ShipCity = o.Shipcity
                })
                .ToListAsync();

           /* using var connection = new SqlConnection(_connectionString);
            string query = @"
                SELECT orderid, requireddate, shippeddate, shipname, shipaddress, shipcity
                FROM Sales.Orders
                WHERE custid = @CustId ORDER BY 1";

            return await connection.QueryAsync<OrdersDto>(query, new { CustId = customerId });*/

        }

        public async Task<IEnumerable<OrdersCustomersDto>> GetOrdersByCustomerId(int custId)
        {
            return await _context.Customers
                .Join(
                    _context.Orders,
                    c => c.Custid,
                    o => o.Custid,
                    (c, o) => new OrdersCustomersDto
                    {
                        Custid = c.Custid,
                        Companyname = c.Companyname,
                        OrderId = o.Orderid,
                        RequiredDate = o.Requireddate,
                        ShippedDate = o.Shippeddate,
                        ShipName = o.Shipname,
                        ShipAddress = o.Shipaddress,
                        ShipCity = o.Shipcity
                    }
                )
                .Where(o => o.Custid == custId) // Filtrar por cliente
                .OrderBy(o => o.Custid)
                .ThenByDescending(o => o.RequiredDate)
                .ToListAsync();

            /*
            using var connection = new SqlConnection(_connectionString);
            string query = @"
            SELECT 
                c.custId, c.CompanyName, o.orderid, o.requireddate, o.shippeddate, 
                o.shipname, o.shipaddress, o.shipcity
            FROM Sales.Customers c 
            JOIN Sales.Orders o ON c.custId = o.custId
            WHERE o.custid = @CustId
            ORDER BY c.custId, o.OrderDate DESC;";

            return await connection.QueryAsync<OrdersCustomersDto>(query, new { CustId = custId });*/
        }
        public async Task<int> CreateOrderAsync(OrderDto order)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                // Insertar la orden y obtener el ID generado
                string insertOrderQuery = @"
                    INSERT INTO Sales.Orders (CustID, EmpID, OrderDate, RequiredDate, ShippedDate, ShipperID, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry)
                    VALUES (@CustID, @EmpID, @OrderDate, @RequiredDate, @ShippedDate, @ShipperID, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int newOrderId = await connection.ExecuteScalarAsync<int>(insertOrderQuery, order, transaction);

                // Insertar detalles de la orden
                string insertDetailQuery = @"
                    INSERT INTO Sales.OrderDetails (OrderID, ProductID, UnitPrice, Qty, Discount)
                    VALUES (@OrderID, @ProductID, @UnitPrice, @Qty, @Discount);";

                foreach (var detail in order.OrderDetails)
                {
                    await connection.ExecuteAsync(insertDetailQuery, new
                    {
                        OrderID = newOrderId,
                        detail.ProductID,
                        detail.UnitPrice,
                        detail.Qty,
                        detail.Discount
                    }, transaction);
                }

                transaction.Commit();
                return newOrderId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}