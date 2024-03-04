
using LibreriaBase.Respuestas;

namespace LibreriaCliente.Servicios.Contratos
{
	public interface IServicioInterfazGenerica<T>
	{
		// Metodos.
		Task<List<T>> ObtenerTodos(string baseUrl);
		Task<T> ObtenerPorId(int id, string baseUrl);
		Task<RespuestaGeneral> Insertar(T modelo, string baseUrl);
		Task<RespuestaGeneral> Actualizar(T modelo, string baseUrl);
		Task<RespuestaGeneral> EliminarPorId(int id, string baseUrl);
	}
}
