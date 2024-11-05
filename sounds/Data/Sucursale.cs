using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Sucursale
    {
        public Sucursale()
        {
            Horarios = new HashSet<Horario>();
            Promociones = new HashSet<Promocione>();
        }

        public byte Id { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }

        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<Promocione> Promociones { get; set; }
    }
}
