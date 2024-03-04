
using LibreriaBase.Respuestas;

namespace LibreriaServer.Repositorios.Contratos
{
	public interface IRepositorioGenerico<T>
	{
		// Metodos.

		Task<List<T>> ObtenerTodos();
		Task<T> ObtenerPorId(int id);
		Task<RespuestaGeneral> Insertar(T item); // "T" hace referencia a todas las entidades que puede aceptar por parametro.
		Task<RespuestaGeneral> Actualizar(T item);
		Task<RespuestaGeneral> EliminarPorId(int id);
	}
}
