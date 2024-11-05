using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Aparatologium
    {
        public byte Id { get; set; }
        public string? Clave { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? RutaAudio { get; set; }
        public string? RutaPrecios { get; set; }
    }
}
