
using LibreriaBase.Entidades;
using LibreriaBase.Respuestas;
using LibreriaServer.Datos;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace LibreriaServer.Repositorios.Implementaciones
{
	public class DepartamentoRepositorio(AppDbContext appDbContext) : IRepositorioGenerico<Departamento>
	{
		// Metodos.

		// Actualizar.
		public async Task<List<Departamento>> ObtenerTodos() => await appDbContext.Departamento.ToListAsync();

		// Obtener por id.
		public async Task<Departamento> ObtenerPorId(int id) => await appDbContext.Departamento.FindAsync(id);

		// Insertar.
		public async Task<RespuestaGeneral> Insertar(Departamento item)
		{
			// verifica el nombre con el metodo  "VerificarNombre", si no lo encuenta devuelve false, y sino lo agrega, guarda los cambio y devuelve true(exitoso).

			if (!await VerificarNombre(item.Nombre!)) return new RespuestaGeneral(false, "El Departamento Ya Existe.");
			appDbContext.Departamento.Add(item);
			await Commit();
			return Exitoso();
		}

		// Actualizar.
		public async Task<RespuestaGeneral> Actualizar(Departamento item)
		{
			var query = await appDbContext.Departamento.FindAsync(item);
			if (query is null) return NoEncontrado();
			query.Nombre = item.Nombre;
			await Commit();
			return Exitoso();
		}

		// Eliminar por id.
		public async Task<RespuestaGeneral> EliminarPorId(int id)
		{
			// busca y verifica el id en la base de datos, sino lo encontro devuelve "no encontrado"
			var query = await appDbContext.Departamento.FindAsync(id);
			if (query is null) return NoEncontrado(); ;

			// si lo encontro, lo elimina, guarda los cambios y devuelve exitoso.
			appDbContext.Departamento.Remove(query);
			await Commit();
			return Exitoso();
		}

		// No encontrado.
		private static RespuestaGeneral NoEncontrado() => new(false, "Departamento No Encontrado.");

		// Exitoso.
		private static RespuestaGeneral Exitoso() => new(true, "Proceso Completado.");

		// Commit.
		private async Task Commit() => await appDbContext.SaveChangesAsync();

		// Verificar Nombre
		private async Task<bool> VerificarNombre(string nombre)
		{
			var item = await appDbContext.Departamento.FirstOrDefaultAsync(x => x.Nombre!.ToLower().Equals(nombre.ToLower()));
			return item is null;
		}
	}
}
