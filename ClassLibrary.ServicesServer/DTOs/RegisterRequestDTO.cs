namespace ClassLibrary.ServicesServer.DTOs
{
    public class RegisterRequestDTO
    {
        public required string Email { get; set; }
        public required string Password1 { get; set; }
        public required string Password2 { get; set; }
    }
}
