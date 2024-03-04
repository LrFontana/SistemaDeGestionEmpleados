
using System.ComponentModel.DataAnnotations;

namespace LibreriaBase.DTOs
{
    public class Register : CuentaBase
    {
        // Propiedades.

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? NombreCompleto { get; set; }


        [DataType(DataType.Password)]
        [Compare(nameof(Contraseña))]
        [Required]
        public string? ConfirmarContraseña { get; set; }
    }
}
