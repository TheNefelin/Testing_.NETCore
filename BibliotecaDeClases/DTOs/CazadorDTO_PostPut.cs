using BibliotecaDeClases.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDeClases.DTOs
{
    public class CazadorDTO_PostPut : ICazador
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }
    }
}
