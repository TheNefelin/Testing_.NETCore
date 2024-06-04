using BibliotecaDeClases.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDeClases.DTOs
{
    public class NenDTO_PostPut : INen
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Descripcion { get; set; } = string.Empty;
    }
}
