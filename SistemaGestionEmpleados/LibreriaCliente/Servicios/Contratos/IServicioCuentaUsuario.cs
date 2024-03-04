using LibreriaBase.DTOs;
using LibreriaBase.Respuestas;

namespace LibreriaCliente.Servicios.Contratos
{
    public interface IServicioCuentaUsuario
    {
        // Metodos.
        Task<RespuestaGeneral> CrearAsync( Register usuario);
        Task<RespuestaLogin> IniciarSesionAsync(Login usuario);
        Task<RespuestaLogin> TokenDeActualizacionAsync( TokenActualizado usuario);
        Task<WeatherForecast[]> ObtenerWeatherForecast();

    }
}
