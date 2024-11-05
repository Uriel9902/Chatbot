using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Log
    {
        public long Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Ubicacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
