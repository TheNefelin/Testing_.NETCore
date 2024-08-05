using ConsoleAppTesting.InjeccionDeDependencia.Models;

namespace ConsoleAppTesting.InjeccionDeDependencia.Services
{
    internal interface ISenderService
    {
        public void Send(Customer customer, string message);
    }
}
