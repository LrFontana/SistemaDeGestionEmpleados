using LibreriaBase.DTOs;
using LibreriaBase.Respuestas;

namespace LibreriaServer.Repositorios.Contratos
{
    public interface ICuentaUsuario
    {
        // Metodos.
        Task<RespuestaGeneral> CrearAsync(Register usuario);
        Task<RespuestaLogin> IniciarSesionAsync(Login usuario);
        Task<RespuestaLogin> TokenDeActualizacionAsync(TokenActualizado token);
    }
}
