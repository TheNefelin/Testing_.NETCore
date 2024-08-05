using ConsoleAppTesting.InjeccionDeDependencia.Models;

namespace ConsoleAppTesting.InjeccionDeDependencia.Services.imp
{
    internal class CommunicationService
    {
        private readonly ISenderService _senderService;

        public CommunicationService(ISenderService senderService)
        {
            _senderService = senderService;
        }

        public void SendMessage(Customer customer, string message)
        {
            _senderService.Send(customer, message);
        }
    }
}
