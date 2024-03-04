using LibreriaServer.Repositorios.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ControlGenerico<T>(IRepositorioGenerico<T> repositorioGenerico) : Controller where T : class
	{
		[HttpGet("todos")]
		public async Task<IActionResult> ObtenerTodos() => Ok(await repositorioGenerico.ObtenerTodos());

		[HttpDelete("eliminar/{id}")]
		public async Task<IActionResult> Eliminar(int id)
		{
			// verifica que el id no sea a menor o igual a 0, de lo contrario lo elimina y devuelve "ok".
			if (id <= 0) return BadRequest("Peticion invalida, intentelo de nuevo.");
			return Ok(await repositorioGenerico.EliminarPorId(id));
		}

		[HttpGet("obtenerPorId/{id}")]
		public async Task<IActionResult> ObtenerPorId(int id)
		{
			// verifica que el id no sea a menor a 0, de lo contrario lo elimina y devuelve "ok".
			if (id <= 0) return BadRequest("Peticion invalida, intentelo de nuevo.");
			return Ok(await repositorioGenerico.ObtenerPorId(id));
		}

		[HttpPost("agregar")]
		public async Task<IActionResult> Insertar(T modelo)
		{
			// verifica que el modelo no sea nulo, de lo contrario lo elimina y devuelve "ok".
			if (modelo is null) return BadRequest("Peticion invalida, intentelo de nuevo.");
			return Ok(await repositorioGenerico.Insertar(modelo));
		}

		[HttpPut("actualizar")]
		public async Task<IActionResult> Actualizar(T modelo)
		{
			// verifica que el modelo no sea nulo, de lo contrario lo elimina y devuelve "ok".
			if (modelo is null) return BadRequest("Peticion invalida, intentelo de nuevo.");
			return Ok(await repositorioGenerico.Actualizar(modelo));
		}
	}
}
