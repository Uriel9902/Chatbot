using System;
using System.Collections.Generic;

namespace API_SISDE.Data
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string? Numero { get; set; }
        public string? Sexo { get; set; }
        public string? Nombre { get; set; }
        public string? ApPaterno { get; set; }
        public string? ApMaterno { get; set; }
        public string? FNacimiento { get; set; }
        public int? IdEstado { get; set; }
        public string? Curp { get; set; }
        public string? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public TimeSpan? HoraAtencion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public TimeSpan? HoraFinalizacion { get; set; }
        public long? IdPaso { get; set; }
        public string? IdSucursal { get; set; }
        public string? Servicio { get; set; }
        public bool? Pvte { get; set; }
        public string? Fam { get; set; }
        public int? IdEstatus { get; set; }
        public long? IdCallcenter { get; set; }
        public long? IdCita { get; set; }
        public int? IdRazon { get; set; }

        public virtual Estatus? IdEstatusNavigation { get; set; }
        public virtual Paso? IdPasoNavigation { get; set; }
        public virtual RazonesCancelar? IdRazonNavigation { get; set; }
    }
}
