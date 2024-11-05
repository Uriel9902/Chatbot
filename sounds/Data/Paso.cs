using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Paso
    {
        public Paso()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public long Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
