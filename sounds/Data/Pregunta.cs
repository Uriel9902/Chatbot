using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Pregunta
    {
        public byte Id { get; set; }
        public string? Clave { get; set; }
        public string? Pregunta1 { get; set; }
        public string? Descripcion { get; set; }
    }
}
