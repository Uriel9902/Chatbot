using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class RazonesCancelar
    {
        public RazonesCancelar()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRazon { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
