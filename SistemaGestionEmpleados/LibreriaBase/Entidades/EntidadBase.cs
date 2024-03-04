
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibreriaBase.Entidades
{
    public class EntidadBase
    {
        // Propiedades.
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;		
	}
}
