
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.DTOs
{
	public class CuentaBase
	{
		// Propiedades.
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[Required]
		public string? Email { get; set; }

		[DataType(DataType.Password)]
		[Required]
		public string? Contraseña { get; set; }
	}
}
