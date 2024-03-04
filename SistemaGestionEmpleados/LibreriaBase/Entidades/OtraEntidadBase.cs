
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
	public class OtraEntidadBase
	{
        // Propiedades.
        public int Id { get; set; }
        [Required]
        public string IdEmpleado { get; set; } = string.Empty;
        [Required]
        public string NumeroARchivo { get; set; } = string.Empty;
        public string? Otro { get; set; }
    }
}
