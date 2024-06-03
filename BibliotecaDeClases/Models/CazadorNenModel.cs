using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.Models
{
    public class CazadorNenModel : ICazadorNen
    {
        public int Id_Cazador { get; set; }
        public int Id_Nen { get; set; }
        public CazadorModel Cazador { get; set; }
        public NenModel Nen { get; set; }
    }
}
