
namespace LibreriaBase.Entidades
{
    public class Rama : EntidadBase
    {
        // Propiedades.

        // relacion muchos a uno con departamento
        public Departamento? Departamento { get; set; }

        public int DepartamentoId { get; set; }

        // relaciones una a muchos con empleados.
        public List<Empleado>? Empleados { get; set; }
    }
}
