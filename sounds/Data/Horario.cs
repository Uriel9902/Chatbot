using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Horario
    {
        public long Id { get; set; }
        public byte IdSucursal { get; set; }
        public string Dias { get; set; } = null!;
        public string? Horario1 { get; set; }

        public virtual Sucursale IdSucursalNavigation { get; set; } = null!;
    }
}
