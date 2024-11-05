using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_SISDE.Data
{
    public partial class CHATPRContext : DbContext
    {
        public CHATPRContext()
        {
        }

        public CHATPRContext(DbContextOptions<CHATPRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aparatologium> Aparatologia { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Estatus> Estatuses { get; set; } = null!;
        public virtual DbSet<Faciale> Faciales { get; set; } = null!;
        public virtual DbSet<Fam> Fams { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Mensaje> Mensajes { get; set; } = null!;
        public virtual DbSet<Paso> Pasos { get; set; } = null!;
        public virtual DbSet<Pregunta> Preguntas { get; set; } = null!;
        public virtual DbSet<Promocione> Promociones { get; set; } = null!;
        public virtual DbSet<RazonesCancelar> RazonesCancelars { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Sucursale> Sucursales { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
               // optionsBuilder.UseSqlServer("Server=DESKTOP-13RE0HC\\CHATDANY ; Database=CHATRES; TrustServerCertificate=True; user=sa; password=Sqlserver123; MultipleActiveResultSets=True");
                optionsBuilder.UseSqlServer("Server=SRV-SISDE-DB\\SISDEBD; Database=CHATPR; TrustServerCertificate=True; user=sa; password=Clinicassql123; MultipleActiveResultSets=True");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aparatologium>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.RutaAudio).HasColumnName("ruta_audio");

                entity.Property(e => e.RutaPrecios).HasColumnName("ruta_precios");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_estado");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdEstado)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_estado");

                entity.Property(e => e.IdPais).HasColumnName("id_pais");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Estatus>(entity =>
            {
                entity.HasKey(e => e.IdEstatus)
                    .HasName("Estatus_PK");

                entity.ToTable("Estatus");

                entity.Property(e => e.IdEstatus).HasColumnName("id_estatus");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Faciale>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.RutaAudio).HasColumnName("ruta_audio");

                entity.Property(e => e.RutaPrecios).HasColumnName("ruta_precios");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fam>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Fam");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dias")
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

                entity.Property(e => e.Horario1)
                    .HasMaxLength(250)
                    .HasColumnName("horario")
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

                entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Horarios_Sucursales_FK");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.Property(e => e.Ubicacion)
                    .IsUnicode(false)
                    .HasColumnName("ubicacion");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

                entity.Property(e => e.Nombre)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Paso>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Pregunta1)
                    .IsUnicode(false)
                    .HasColumnName("pregunta");
            });

            modelBuilder.Entity<Promocione>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.ClaveSucursal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clave_sucursal");

                entity.Property(e => e.Desde)
                    .HasColumnType("date")
                    .HasColumnName("desde");

                entity.Property(e => e.Hasta)
                    .HasColumnType("date")
                    .HasColumnName("hasta");

                entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

                entity.Property(e => e.Nombre)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.RutaImagen)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Promociones)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("Promociones_Sucursales_FK");
            });

            modelBuilder.Entity<RazonesCancelar>(entity =>
            {
                entity.HasKey(e => e.IdRazon);

                entity.ToTable("Razones_Cancelar");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");
            });

            modelBuilder.Entity<Sucursale>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

                entity.Property(e => e.Latitud).IsUnicode(false);

                entity.Property(e => e.Longitud).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre")
                    .UseCollation("Latin1_General_100_CI_AI_SC_UTF8");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ap_materno");

                entity.Property(e => e.ApPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ap_paterno");

                entity.Property(e => e.Curp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("curp");

                entity.Property(e => e.FNacimiento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("f_nacimiento");

                entity.Property(e => e.Fam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.FechaAtencion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_atencion");

                entity.Property(e => e.FechaFinalizacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_finalizacion");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.HoraAtencion).HasColumnName("hora_atencion");

                entity.Property(e => e.HoraFinalizacion).HasColumnName("hora_finalizacion");

                entity.Property(e => e.IdCallcenter).HasColumnName("id_callcenter");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdEstatus).HasColumnName("id_estatus");

                entity.Property(e => e.IdPaso).HasColumnName("id_paso");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.IdSucursal)
                    .IsUnicode(false)
                    .HasColumnName("id_sucursal");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numero");

                entity.Property(e => e.Pvte).HasColumnName("pvte");

                entity.Property(e => e.Servicio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sexo");

                entity.HasOne(d => d.IdEstatusNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEstatus)
                    .HasConstraintName("Usuarios_Estatus_FK");

                entity.HasOne(d => d.IdPasoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPaso)
                    .HasConstraintName("Usuarios_Pasos_FK");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRazon)
                    .HasConstraintName("FK_Usuarios_Razones_Cancelar");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
