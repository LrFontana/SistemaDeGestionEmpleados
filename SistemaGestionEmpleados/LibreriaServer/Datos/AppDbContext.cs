using LibreriaBase.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaServer.Datos
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // Propiedades.         
        public DbSet<Empleado> Empleado { get; set; }

		// Departamento General / Departamento / Rama .
		public DbSet<DepartamentoGeneral> DepartamentoGeneral { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Rama> Rama { get; set; }

        // Pais / Ciudad / Pueblo        
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
		public DbSet<Pueblo> Pueblo { get; set; }

		// Autenticacion / Roles Usuario / Sistema de Roles 
		public DbSet<AplicacionUsuario> AplicacionUsuario { get; set; }
        public DbSet<SistemaDeRol> SistemaDeRol { get; set; }
        public DbSet<RolUsuario> RolUsuario { get; set; }
        public DbSet<InfoTokenActualizado> InfoTokenActualizado { get; set; }

        // Otros / Vacaciones/ Sanciones / Medicos / Horas Extra
        public DbSet<Vacaciones> Vacaiones { get; set; }
        public DbSet<TipoDeVacaciones> TipoDeVacaciones { get; set; }
        public DbSet<HorasExtras> HorasExtras { get; set; }
        public DbSet<TipoHorasExtras> TipoHorasExtras { get; set; }
        public DbSet<Sancion> Sanciones { get; set; }
        public DbSet<TipoDeSancion> TipoDeSanciones { get; set; }
        public DbSet<Medico> Medicos { get; set; }
    }
}
