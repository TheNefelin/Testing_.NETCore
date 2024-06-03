using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.DTOs
{
    public class RepoCazadorDTO : IKeyBase, ICazador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<NenDTO_Get> Nen { get; set; } = [];
    }
}
