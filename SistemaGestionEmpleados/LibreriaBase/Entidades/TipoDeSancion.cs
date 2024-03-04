
namespace LibreriaBase.Entidades
{
	public class TipoDeSancion : EntidadBase
	{
        // Propiedades. 

        // relacion varias a una con vacaciones.
        public List<Sancion>? Sanciones { get; set; }
    }
}
