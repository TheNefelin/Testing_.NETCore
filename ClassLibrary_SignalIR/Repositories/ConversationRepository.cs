using ClassLibrary_SignalIR.Models;

namespace ClassLibrary_SignalIR.Repositories
{
    public class ConversationRepository
    {
        public Dictionary<string, Conversation> Conversations { get; private set; } = new Dictionary<string, Conversation>();

        public Conversation GetConversationById(string conversationId)
        {
            return Conversations.TryGetValue(conversationId, out var conversation) ? conversation : null;
        }

        public void AddConversation(Conversation conversation)
        {
            Conversations[conversation.ConversationId] = conversation;
        }
    }
}
