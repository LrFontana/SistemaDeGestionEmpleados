
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
	public class HorasExtras : OtraEntidadBase
	{
		// Propiedades.
		[Required]
		public DateTime FechaInicio { get; set; }
		[Required]
		public DateTime FechaFin { get; set; }

		public int NumeroDeDias => (FechaFin - FechaInicio).Days;

		// relacion muchas a una con vacaciones.

		public TipoHorasExtras? TipoHorasExtras { get; set; }
		[Required]
		public int TipoHorasExtrasId { get; set; }
	}
}
