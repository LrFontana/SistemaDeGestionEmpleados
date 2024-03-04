
namespace LibreriaBase.Entidades
{
    public class Departamento : EntidadBase
    {
        // Propiedades.

        // relacion muchas a uno con departamento.
        public DepartamentoGeneral? DepartamentoGeneral { get; set; }

        public int DepartamentoGeneralId { get; set; }

        // relacion una a muchos con Rama.
        public List<Rama>? Ramas { get; set; }
    }
}
