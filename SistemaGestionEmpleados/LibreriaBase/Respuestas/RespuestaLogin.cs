
namespace LibreriaBase.Respuestas
{
    public record RespuestaLogin(bool Alerta, string Mensaje = null!,  string Token = null!, string TokenActualizado = null!);
}
