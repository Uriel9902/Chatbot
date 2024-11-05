using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Estatus
    {
        public Estatus()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEstatus { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
