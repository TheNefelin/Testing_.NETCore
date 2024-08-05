using ConsoleAppTesting.InjeccionDeDependencia.Models;

namespace ConsoleAppTesting.InjeccionDeDependencia.Repositories
{
    internal interface IRepository
    {
        public List<Customer> GetCustomers();
    }
}
