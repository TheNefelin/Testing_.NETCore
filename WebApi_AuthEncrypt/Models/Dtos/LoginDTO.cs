using System.ComponentModel.DataAnnotations;

namespace WebApi_AuthEncrypt.Models.Dtos
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
