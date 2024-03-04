using LibreriaBase.DTOs;
using LibreriaBase.Entidades;
using LibreriaBase.Respuestas;
using LibreriaServer.Datos;
using LibreriaServer.Helpers;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibreriaServer.Repositorios.Implementaciones
{
    public class CuentaUsuarioRepositorio(IOptions<SeccionJwt> config, AppDbContext appDbContext) : ICuentaUsuario
    {
        // Metodos.

        //Crear.
        public async Task<RespuestaGeneral> CrearAsync(Register usuario)
        {
            // verifica si el usuario en null.
            if (usuario is null) return new RespuestaGeneral(false, "El Modelo esta Vacio.");

            //busca al usuario llamando al metodo "BuscarUsuarioPorMail".
            var verificarUsuario = await BuscarUsuarioPorMail(usuario.Email!);
            if (verificarUsuario != null)
            {
                return new RespuestaGeneral(false, "El Usuario ya esta registrado.");
            }

            // guarda al usuario.
            var aplicacionUsuario = await Agregar(new AplicacionUsuario()
            {
                NombreCompelto = usuario.NombreCompleto,
                Email = usuario.Email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña),
            });

            // verifica, crea y asigna el rol.
            var verificarRolAdministrador = await appDbContext.SistemaDeRol.FirstOrDefaultAsync(u => u.Nombre!.Equals(PropiedadesConstantes.Administrador));
            if (verificarRolAdministrador is null)
            {
                // Crea el rol de administrador y lo guarda en la abse de datos.
                var crearRolAdministrador = await Agregar(new SistemaDeRol() { Nombre = PropiedadesConstantes.Administrador});
                await Agregar(new RolUsuario() { IdRol = crearRolAdministrador.Id, IdUsuario = aplicacionUsuario.Id });
                return new RespuestaGeneral(true, "Cuenta Creada Exitosamente.");
            }

            // verifica y agrega el rol al usuario
            var verificarRolUsuario = await appDbContext.SistemaDeRol.FirstOrDefaultAsync(u => u.Nombre!.Equals(PropiedadesConstantes.Usuario));
            SistemaDeRol respuesta = new();
            if (verificarRolUsuario is null)
            {
                respuesta = await Agregar(new SistemaDeRol() { Nombre = PropiedadesConstantes.Usuario});
                await Agregar(new RolUsuario() { IdRol = respuesta.Id, IdUsuario = aplicacionUsuario.Id });
            }
            else
            {
                await Agregar(new RolUsuario() { IdRol = verificarRolUsuario.Id, IdUsuario = aplicacionUsuario.Id });
            }
            return new RespuestaGeneral(true, "Cuenta Creada Exitosamente.");
        }

        // Iniciar Sesion.
        public async Task<RespuestaLogin> IniciarSesionAsync(Login usuario)
        {
            if (usuario is null) return new RespuestaLogin(false, "El Modelo esta Vacio.");

            // Obtiene y verifica el mail.
            var aplicacionUsuario = await BuscarUsuarioPorMail(usuario.Email!);
            if (aplicacionUsuario is null) return new RespuestaLogin(false, "Usuario No Encontrado.");

            // Verifica la contraseña.
            if (!BCrypt.Net.BCrypt.Verify(usuario.Contraseña, aplicacionUsuario.Contraseña))
                return new RespuestaLogin(false, "Email o Contraseña Incorrectos.");

            // Obtiene y verifica el rol si es valido.
            var obtenerRolUsuario = await BuscarRolUsuario(aplicacionUsuario.Id);
            if (obtenerRolUsuario is null) return new RespuestaLogin(false, "Usuario Rol No Encontrado");

            // Obtiene y verifica el Nombre.
            var obtenerNombreRol = await BuscarNombreRol(obtenerRolUsuario.IdRol);
            if (obtenerNombreRol is null) return new RespuestaLogin(false, "Usuario Rol No Encontrado");

            // Gerena el Token.
            string jwtToken = GenerarToken(aplicacionUsuario, obtenerNombreRol!.Nombre!);
            string actualizarToken = GenerarTokenDeActualizacion();

            // Guarda el token en la base de datos.
            var buscarUsuario = await appDbContext.InfoTokenActualizado.FirstOrDefaultAsync(u => u.UsuarioId == aplicacionUsuario.Id);
            if (buscarUsuario is not null)
            {
                buscarUsuario!.Token = actualizarToken;
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                await Agregar(new InfoTokenActualizado() { Token = actualizarToken, UsuarioId = aplicacionUsuario.Id });
            }
            return new RespuestaLogin(true, "Inicio de Sesion Exitoso!", jwtToken, actualizarToken);
        }

        // Genera el token.
        private string GenerarToken(AplicacionUsuario usuario, string rol)
        {
            var llaveDeSeguridad = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credenciales = new SigningCredentials(llaveDeSeguridad, SecurityAlgorithms.HmacSha256);
            var notificacionUsuario = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompelto!),
                new Claim(ClaimTypes.Email, usuario.Email!),
                new Claim(ClaimTypes.Role, rol!)
            };
            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: notificacionUsuario,
                expires: DateTime.Now.AddSeconds(2),
                signingCredentials: credenciales
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Buscar rol usuario.
        private async Task<RolUsuario> BuscarRolUsuario(int idUsuario) => await appDbContext.RolUsuario.FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

        // Busacar 
        private async Task<SistemaDeRol>  BuscarNombreRol(int idRol) => await appDbContext.SistemaDeRol.FirstOrDefaultAsync(u => u.Id == idRol);

        // Generar token de actualizacion.
        private static string GenerarTokenDeActualizacion() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        // Buscar usuario por mail.
        private async Task<AplicacionUsuario> BuscarUsuarioPorMail(string mail) =>
            await appDbContext.AplicacionUsuario.FirstOrDefaultAsync(u => u.Email!.ToLower()!.Equals(mail!.ToLower()));


        // Agregar.
        private async Task<T> Agregar<T>(T modelo)
        {
            var resultado = appDbContext.Add(modelo!);
            await appDbContext.SaveChangesAsync();
            return (T)resultado.Entity;
        }

        // Token de actualizacion.
        public async Task<RespuestaLogin> TokenDeActualizacionAsync(TokenActualizado token)
        {
            if (token is null) return new RespuestaLogin(false, "El Modelo esta Vacio.");
            
            // Obtiene y verifica el token.
            var buscarToken = await appDbContext.InfoTokenActualizado.FirstOrDefaultAsync(u => u.Token!.Equals(token.Token));
            if (buscarToken is null) return new RespuestaLogin(false, "Se Requiere Token de Actualizacion.");

            // Si fue encontrado obtiene y verifica los detalles del usuario.
            var usuario = await appDbContext.AplicacionUsuario.FirstOrDefaultAsync(u => u.Id == buscarToken.UsuarioId);
            if (usuario is null) return new RespuestaLogin(false, "El Token de Actualizacion No Se Puedo Generar Por Que El Usuario No Fue Encontrado");


            var rolUsuario = await BuscarRolUsuario(usuario.Id);
            var rolNombre = await BuscarNombreRol(rolUsuario.IdRol);
            string jwtToken = GenerarToken(usuario, rolNombre.Nombre!);
            string tokenDeActualizacion = GenerarTokenDeActualizacion();

            // Obtiene y verifica si la tabla token de actualizacion contiene a ese usuario especifico.
            var actualizarTokenDeActualizacion = await appDbContext.InfoTokenActualizado.FirstOrDefaultAsync(u => u.UsuarioId == usuario.Id);
            if (actualizarTokenDeActualizacion is null) return new RespuestaLogin(false, "El Token de Actualizacion No Se Puedo Generar Por Que El Usuario Iniciar Sesion.");

            // Actualiza el token.
            actualizarTokenDeActualizacion.Token = tokenDeActualizacion;
            await appDbContext.SaveChangesAsync();
            return new RespuestaLogin(true, "Token Actualizado Exitosamente!", jwtToken, tokenDeActualizacion);
        }
	}
}
