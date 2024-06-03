using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class CazadorDTO_PostPut : ICazador
    {
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
    }
}
