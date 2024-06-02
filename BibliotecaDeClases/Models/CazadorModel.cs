using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.Models
{
    public class CazadorModel : ICazador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<CazadorNenModel> cazador_Nen { get; set; } = [];
    }
}
