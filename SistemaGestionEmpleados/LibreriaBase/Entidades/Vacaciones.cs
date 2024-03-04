using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
	public class Vacaciones : OtraEntidadBase
	{
		// Propiedades.
		[Required]
		public DateTime FechaInicio { get; set; }

		public int NumeroDeDias { get; set; }

		[Required]
		public DateTime FechaFin => FechaInicio.AddDays(NumeroDeDias);
		

		// relacion muchas a una con tipo de vacaciones.

		public TipoDeVacaciones? TipoDeVacaciones { get; set; }
		[Required]
		public int TipoDeVacacionesId { get; set; }
	}
}
