using BibliotecaDeClases.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDeClases.DTOs
{
    public class CazadorNenDTO : ICazadorNen
    {
        [Required]
        public int Id_Cazador { get; set; }

        [Required]
        public int Id_Nen { get; set; }
    }
}
