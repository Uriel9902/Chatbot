using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Promocione
    {
        public long Id { get; set; }
        public string? RutaImagen { get; set; }
        public string? Nombre { get; set; }
        public string? Clave { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public string? ClaveSucursal { get; set; }
        public byte? IdSucursal { get; set; }

        public virtual Sucursale? IdSucursalNavigation { get; set; }
    }
}
