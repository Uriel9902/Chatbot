using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Estado
    {
        public byte IdEstado { get; set; }
        public byte? IdPais { get; set; }
        public string? CEstado { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? Clave { get; set; }
    }
}
