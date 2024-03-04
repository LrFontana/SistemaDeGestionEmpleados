
namespace LibreriaBase.Entidades
{
	public class Pais :EntidadBase
	{
        // Propiedades.

        // relacion una a muchas con Cuidad.
        public List<Ciudad>? Ciudad { get; set; }
    }
}
