using ClassLibrary_SignalIR.Models;
using ClassLibrary_SignalIR.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace WebApi_SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ConversationRepository _conversationRepository;

        public ChatHub()
        {
            _conversationRepository = new ConversationRepository();
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
            string cnnId = Context.ConnectionId;

            if (userId != null)
            {
                Console.WriteLine($"Usuario conectado: {userId}");
            }

            await base.OnConnectedAsync();
        }

        public async Task GetConversationHistory(string conversationId)
        {
            var conversation = _conversationRepository.GetConversationById(conversationId);
            if (conversation == null)
            {
                await Clients.Caller.SendAsync("Error", "Conversación no encontrada");
                return;
            }

            await Clients.Caller.SendAsync("ReceiveHistory", conversation.Messages);
        }

        public async Task SendMessage(string conversationId, string senderId, string content)
        {
            var message = new Message(senderId, Context.User.Identity.Name, content);

            var conversation = _conversationRepository.GetConversationById(conversationId);
            if (conversation == null)
            {
                await Clients.Caller.SendAsync("Error", "Conversación no encontrada");
                return;
            }

            conversation.Messages.Add(message);

            await Clients.Group(conversationId).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinConversation(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task LeaveConversation(string conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }
    }
}
