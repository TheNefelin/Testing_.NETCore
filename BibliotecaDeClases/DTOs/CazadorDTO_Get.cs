using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class CazadorDTO_Get : IKeyBase, ICazador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
    }
}
