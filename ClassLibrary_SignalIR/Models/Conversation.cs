namespace ClassLibrary_SignalIR.Models
{
    public class Conversation
    {
        public string ConversationId { get; set; }
        public List<User> Participants { get; set; } = new List<User>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
