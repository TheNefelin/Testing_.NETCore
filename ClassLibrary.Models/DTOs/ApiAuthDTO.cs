namespace ClassLibrary.Models.DTOs
{
    public class ApiAuthDTO
    {
        public required Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required double ExpiresMin { get; set; }
    }
}
