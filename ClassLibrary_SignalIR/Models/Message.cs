namespace ClassLibrary_SignalIR.Models
{
    public class Message
    {
        public Message(string senderId, string? name, string content)
        {
            SenderId = senderId;
            Name = name;
            Content = content;
            Timestamp = DateTime.UtcNow;
        }

        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public string? Name { get; }
    }
}
