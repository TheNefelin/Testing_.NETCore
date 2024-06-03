using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class NenDTO_Get : IKeyBase, INen
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
