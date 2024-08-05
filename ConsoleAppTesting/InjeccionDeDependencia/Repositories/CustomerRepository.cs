using ConsoleAppTesting.InjeccionDeDependencia.Models;
using ConsoleAppTesting.InjeccionDeDependencia.Repositories.Connections;
using System.Data.Common;

namespace ConsoleAppTesting.InjeccionDeDependencia.Repositories
{
    internal class CustomerRepository : IRepository
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Customer> GetCustomers()
        {
            if (_connection.GetType() == typeof(SqlServerConnection))
                Console.WriteLine("Customers from SQL Server");
            else if (_connection.GetType() == typeof(MySqlConnection))
                Console.WriteLine("Customers from MySql");
            else if (_connection.GetType() == typeof(OracleConnection))
                Console.WriteLine("Customers from Oracle");

            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Client_1",
                    Email = "email_1.@try.com",
                    Phone = "111111111"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Client_2",
                    Email = "email_2.@try.com",
                    Phone = "222222222"
                },
                new Customer
                {
                    Id = 3,
                    Name = "Client_3",
                    Email = "email_3.@try.com",
                    Phone = "333333333"
                }
            };
        }
    }
}
