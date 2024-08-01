using System.ComponentModel.DataAnnotations;

namespace BibliotecaDeClases.DTOs
{
    public class AuthLoginDTO
    {
        [Required]
        [EmailAddress]
        [MinLength(6)]
        [MaxLength(100)]  
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
