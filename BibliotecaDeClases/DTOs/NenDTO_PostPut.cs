using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class NenDTO_PostPut : INen
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
