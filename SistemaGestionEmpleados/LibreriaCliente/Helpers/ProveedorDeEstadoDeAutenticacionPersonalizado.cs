using LibreriaBase.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibreriaCliente.Helpers
{
    public class ProveedorDeEstadoDeAutenticacionPersonalizado(ServicioDeAlmacenamientoLocal servicioDeAlmacenamientoLocal) : AuthenticationStateProvider
    {
        // Propiedades.
        private readonly ClaimsPrincipal anonimo = new(new ClaimsIdentity()); // notificacion anonimo.

        // Metodos. 

        // Obtien el estado de autenticacion (metodo que ya viene en la herencia).
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Obtiene y verifica el token.
            var stringToken = await servicioDeAlmacenamientoLocal.ObtenerToken();
            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(anonimo));

            // si el token fue encontrado, lo deserealiza.
            var deserealizeToken = Serialozations.DeserializeJsonString<SesionUsuario>(stringToken);
            if (deserealizeToken == null) return await Task.FromResult(new AuthenticationState(anonimo));

            // decifra el token, para obtener el reclamo.
            var obtenerNotificacionUsuario = DecifrarToken(deserealizeToken.Token!);
            if (obtenerNotificacionUsuario == null) return await Task.FromResult(new AuthenticationState(anonimo));

            var reclamoPrincipal = EstablecerNotificacionPrincipal(obtenerNotificacionUsuario);
            return await Task.FromResult(new AuthenticationState(reclamoPrincipal));
        }

        // Actualizacion del estado de autenticacion
        public async Task ActualizarEstadoDeAutenticacion(SesionUsuario sesionUsuario)
        {
            var notificacionPrincipal = new ClaimsPrincipal();
            if (sesionUsuario.Token != null || sesionUsuario.TokenDeActualizacion != null)
            {
                var seserializarSesion = Serialozations.SerializeObj(sesionUsuario);
                await servicioDeAlmacenamientoLocal.EstablecerToken(seserializarSesion);
                var obtenerNotificacionUsuario = DecifrarToken(sesionUsuario.Token!);
                notificacionPrincipal = EstablecerNotificacionPrincipal(obtenerNotificacionUsuario);
            }
            else
            {
                await servicioDeAlmacenamientoLocal.EliminarToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(notificacionPrincipal)));
        }

        // Notificacion principal.
        public static ClaimsPrincipal EstablecerNotificacionPrincipal(NotificacionesPersonalizadaUsuario claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity( 
                new List<Claim>
                {
                    new (ClaimTypes.NameIdentifier, claims.Id!),
                    new (ClaimTypes.Name, claims.Nombre!),
                    new (ClaimTypes.Email, claims.Email!),
                    new (ClaimTypes.Role, claims.Rol!),                    
                }, "JwtAuth"));
        }

        // Decifrar token.
        private static NotificacionesPersonalizadaUsuario DecifrarToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new NotificacionesPersonalizadaUsuario();

            // obtiene el token lo decripta y devuelto todos los datos del usuario.
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var usuarioId = token.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);
            var nombre = token.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email);
            var rol = token.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role);
            return new NotificacionesPersonalizadaUsuario(usuarioId!.Value!, nombre!.Value, email!.Value, rol!.Value);

        }
    }
}
