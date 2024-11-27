namespace ClassLibrary.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Hash1 { get; set; }
        public required string Salt1 { get; set; }
    }
}
