using LibreriaBase.DTOs;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AutentificacionControl(ICuentaUsuario iCuentaUsuario) : ControllerBase
    {
        
        [HttpPost("register")]

        public async Task<IActionResult> CrearAsync(Register usuario)
        {
            if (usuario == null) return BadRequest("El Modelo esta vacio.");
            var resultado = await iCuentaUsuario.CrearAsync(usuario);
            return Ok(resultado);
        }

        [HttpPost("login")]
        public async Task<IActionResult> IniciarSesionAsync(Login usuario)
        {
            if(usuario == null) return BadRequest("El Modelo esta Vacio.");
            var respuesta = await iCuentaUsuario.IniciarSesionAsync(usuario);
            return Ok(respuesta);
        }

        [HttpPost("token-de-actualizacion")]
        public async Task<IActionResult> TokenDeActualizacionAsync(TokenActualizado token)
        {
            if (token == null) return BadRequest("El Modelo esta vacio.");
            var resultado = await iCuentaUsuario.TokenDeActualizacionAsync(token);
            return Ok(resultado);
        }
    }
}
