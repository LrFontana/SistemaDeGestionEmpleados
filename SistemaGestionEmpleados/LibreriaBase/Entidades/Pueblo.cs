
namespace LibreriaBase.Entidades
{
	public class Pueblo : EntidadBase
	{
        // Propiedades.       

        // relacion muchas a una con ciudad.
        public Ciudad? Ciudad { get; set; }
        public int CiudadId { get; set; }

		// ralacion una a muchas con empleado.
		public List<Empleado>? Empleados { get; set; }
	}
}
