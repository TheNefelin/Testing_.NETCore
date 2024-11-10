namespace ClassLibrary_SignalIR.Models
{
    public class UserChat
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
    }
}
