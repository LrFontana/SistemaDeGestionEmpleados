using LibreriaBase.DTOs;
using LibreriaBase.Respuestas;
using LibreriaCliente.Helpers;
using LibreriaCliente.Servicios.Contratos;
using System.Net.Http.Json;

namespace LibreriaCliente.Servicios.Implementaciones
{
    public class ServicioCuentaUsuario(ObtenerHttpCliente obtenerHttpCliente) : IServicioCuentaUsuario
    {
        // Propiedades.
        public const string AutoUrl = "api/autenticacion";

        // Metodos.
        // Crear.
        public async Task<RespuestaGeneral> CrearAsync(Register usuario)
        {
            var httpClient = obtenerHttpCliente.ObtenerHttpClientePublico();
            var resultado = await httpClient.PostAsJsonAsync($"{AutoUrl}/register", usuario);
            if (!resultado.IsSuccessStatusCode) return new RespuestaGeneral(false, "Ocurrio un Error.");

            return await resultado.Content.ReadFromJsonAsync<RespuestaGeneral>()!;
        }

        // Iniciar Sesion.
        public async Task<RespuestaLogin> IniciarSesionAsync(Login usuario)
        {
            var httpClient = obtenerHttpCliente.ObtenerHttpClientePublico();
            var resultado = await httpClient.PostAsJsonAsync($"{AutoUrl}/login", usuario);
            if (!resultado.IsSuccessStatusCode) return new RespuestaLogin(false, "Ocurrio un Error.");

            return await resultado.Content.ReadFromJsonAsync<RespuestaLogin>()!;
        }       


        // Token.
        public async Task<RespuestaLogin> TokenDeActualizacionAsync(TokenActualizado token)
        {
			var httpClient = obtenerHttpCliente.ObtenerHttpClientePublico();
			var resultado = await httpClient.PostAsJsonAsync($"{AutoUrl}/token-de-actualizacion", token);
			if (!resultado.IsSuccessStatusCode) return new RespuestaLogin(false, "Ocurrio un Error.");

			return await resultado.Content.ReadFromJsonAsync<RespuestaLogin>()!;
		}


        public async Task<WeatherForecast[]> ObtenerWeatherForecast()
        {
            var httpClient = obtenerHttpCliente.ObtenerHttpClientePublico();
            var resultado = await httpClient.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast");
            return resultado!;
        }

    }
}
