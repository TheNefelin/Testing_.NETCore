using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class LoginRequestDTO
    {
        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }

        [MinLength(6)]
        [MaxLength(25)]
        public required string Password { get; set; }
    }
}
