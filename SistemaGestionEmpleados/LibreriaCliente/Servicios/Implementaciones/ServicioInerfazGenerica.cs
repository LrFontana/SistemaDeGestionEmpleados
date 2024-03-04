
using LibreriaBase.Respuestas;
using LibreriaCliente.Helpers;
using LibreriaCliente.Servicios.Contratos;
using System.Net.Http.Json;
using System.Reflection;

namespace LibreriaCliente.Servicios.Implementaciones
{
	public class ServicioInerfazGenerica<T>(ObtenerHttpCliente obtenerHttpCliente) : IServicioInterfazGenerica<T>
	{
		// Metodos.

		// Obetenr todos.
		public async Task<List<T>> ObtenerTodos(string baseUrl)
		{
			var httpClient = await obtenerHttpCliente.ObtenerHttpClientPrivado();
			var resultado = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/todos");
			return resultado!;
		}

		// Obtener por id.
		public async Task<T> ObtenerPorId(int id,string baseUrl)
		{
			var httpClient = await obtenerHttpCliente.ObtenerHttpClientPrivado();
			var resultado = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/obtenerPorId/{id}");
			return resultado!;
		}

		// Agregar.
		public async Task<RespuestaGeneral> Insertar(T modelo, string baseUrl)
		{
			var httpClient = await obtenerHttpCliente.ObtenerHttpClientPrivado();
			var respuesta = await httpClient.PostAsJsonAsync($"{baseUrl}/agregar", modelo);
			var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaGeneral>();
			return resultado!;
		}

		// Actualizar.
		public async Task<RespuestaGeneral> Actualizar(T modelo, string baseUrl)
		{
			var httpClient = await obtenerHttpCliente.ObtenerHttpClientPrivado();
			var respuesta = await httpClient.PutAsJsonAsync($"{baseUrl}/actualizar", modelo);
			var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaGeneral>();
			return resultado!;
		}

		//Eliminar por id.
		public async Task<RespuestaGeneral> EliminarPorId(int id, string baseUrl)
		{
			var httpClient = await obtenerHttpCliente.ObtenerHttpClientPrivado();
			var respuesta = await httpClient.DeleteAsync($"{baseUrl}/eliminar/{id}");
			var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaGeneral>();
			return resultado!;
		}
	}
}
