﻿using BibliotecaDeClases.Interfaces;

namespace BibliotecaDeClases.Models
{
    public class CazadorModel : IKeyBase, ICazador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<CazadorNenModel> Cazador_Nen { get; set; }
    }
}
