using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class CazadorNenDTO : ICazadorNen
    {
        public int Id_Cazador { get; set; }
        public int Id_Nen { get; set; }
    }
}
