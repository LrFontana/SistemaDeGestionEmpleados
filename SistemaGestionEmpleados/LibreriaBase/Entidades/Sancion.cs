using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
	public class Sancion : OtraEntidadBase
	{
        // Propiedades.
        [Required]
        public DateTime Dia { get; set; }
        [Required]
        public string Castigo { get; set; } = string.Empty;
        [Required]
        public DateTime CastigoFecha { get; set; }

        // relacion una a muchas con Tipo de vacaciones.
        public TipoDeSancion? TipoDeSancion { get; set; }
    }
}
