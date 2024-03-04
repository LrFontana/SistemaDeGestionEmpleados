namespace LibreriaBase.Entidades
{
	public class TipoDeVacaciones : EntidadBase
	{
        // Propiedades.

        // relacion muchas a una con vacacion
        public List<Vacaciones>? Vacaciones { get; set; }
    }
}
