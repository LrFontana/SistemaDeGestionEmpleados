
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
	public class Medico : OtraEntidadBase
	{
        // Propiedades.
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string DiagnosticoMedico { get; set; } = string.Empty;
        [Required]
        public string RecomendacionesMEdicas { get; set; } = string.Empty;
    }
}
