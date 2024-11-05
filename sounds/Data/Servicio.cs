using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Servicio
    {
        public byte Id { get; set; }
        public string? Clave { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategory { get; set; }
    }
}
