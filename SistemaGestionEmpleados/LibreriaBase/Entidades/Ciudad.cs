
namespace LibreriaBase.Entidades
{
	public class Ciudad : EntidadBase
	{
        // Propiedades.

        // relacion muchas a uno con pais.
        public Pais? Pais { get; set; }
        public int PaisId { get; set; }

        // relacion ona a muchas con pueblo.
        public List<Pueblo>? Pueblos { get; set; }
    }
}
