
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.Entidades
{
    public class Empleado : EntidadBase
    {
        // Propiedades.        
        [Required]
        public string IdEmpleado { get; set; } = string.Empty ;
        [Required]
        public string NumeroArchivo { get; set; } = string.Empty;
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required]
        public string NombreTrabajo { get; set; } = string.Empty;
        [Required]
        public string Direccion { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
		public string Telefono { get; set; } = string.Empty;
        [Required]
		public string Foto { get; set; } = string.Empty;        
		public string? Otro { get; set; } = string.Empty;



		// Ralacion Db:       

		//  relacion varias a uno con rama.
		public Rama? Rama { get; set; }
        public int RamaId { get; set; }

        //cuidad.
        public Pueblo? Pueblo { get; set; }
        public int PuebloId { get; set; }
    }
}
