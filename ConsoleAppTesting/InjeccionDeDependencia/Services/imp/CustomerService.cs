using ConsoleAppTesting.InjeccionDeDependencia.Models;
using ConsoleAppTesting.InjeccionDeDependencia.Repositories;

namespace ConsoleAppTesting.InjeccionDeDependencia.Services.imp
{
    internal class CustomerService
    {
        private readonly IRepository _repository;

        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Customer> GetCustomers()
        {
            return _repository.GetCustomers();
        }
    }
}
