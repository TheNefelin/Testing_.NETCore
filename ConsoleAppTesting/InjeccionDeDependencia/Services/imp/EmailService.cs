using ConsoleAppTesting.InjeccionDeDependencia.Models;

namespace ConsoleAppTesting.InjeccionDeDependencia.Services.imp
{
    internal class EmailService : ISenderService
    {
        public void Send(Customer customer, string message)
        {
            Console.WriteLine($"Message sent to {customer.Name} by Email");
        }
    }
}
