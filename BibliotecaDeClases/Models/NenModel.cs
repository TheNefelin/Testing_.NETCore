using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.Models
{
    public class NenModel : INen
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<CazadorNenModel> Cazador_Nen { get; set; }
    }
}
