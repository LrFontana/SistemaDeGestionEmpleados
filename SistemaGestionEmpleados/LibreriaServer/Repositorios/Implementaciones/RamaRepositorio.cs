using LibreriaBase.Entidades;
using LibreriaBase.Respuestas;
using LibreriaServer.Datos;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.EntityFrameworkCore;
namespace LibreriaServer.Repositorios.Implementaciones
{
	public class RamaRepositorio(AppDbContext appDbContext) : IRepositorioGenerico<Rama>
	{
		// Metodos.

		// Actualizar.
		public async Task<List<Rama>> ObtenerTodos() => await appDbContext.Rama.ToListAsync();

		// Obtener por id.
		public async Task<Rama> ObtenerPorId(int id) => await appDbContext.Rama.FindAsync(id);

		// Insertar.
		public async Task<RespuestaGeneral> Insertar(Rama item)
		{
			// verifica el nombre con el metodo  "VerificarNombre", si no lo encuenta devuelve false, y sino lo agrega, guarda los cambio y devuelve true(exitoso).

			if (!await VerificarNombre(item.Nombre!)) return new RespuestaGeneral(false, "La Rama Ya Existe!.");
			appDbContext.Rama.Add(item);
			await Commit();
			return Exitoso();
		}

		// Actualizar.
		public async Task<RespuestaGeneral> Actualizar(Rama item)
		{
			var query = await appDbContext.Rama.FindAsync(item);
			if (query is null) return NoEncontrado();
			query.Nombre = item.Nombre;
			await Commit();
			return Exitoso();
		}

		// Eliminar por id.
		public async Task<RespuestaGeneral> EliminarPorId(int id)
		{
			// busca y verifica el id en la base de datos, sino lo encontro devuelve "no encontrado"
			var query = await appDbContext.Rama.FindAsync(id);
			if (query is null) return NoEncontrado(); ;

			// si lo encontro, lo elimina, guarda los cambios y devuelve exitoso.
			appDbContext.Rama.Remove(query);
			await Commit();
			return Exitoso();
		}

		// No encontrado.
		private static RespuestaGeneral NoEncontrado() => new(false, "Rama No Encontrada.");

		// Exitoso.
		private static RespuestaGeneral Exitoso() => new(true, "Proceso Completado.");

		// Commit.
		private async Task Commit() => await appDbContext.SaveChangesAsync();

		// Verificar Nombre
		private async Task<bool> VerificarNombre(string nombre)
		{
			var item = await appDbContext.Rama.FirstOrDefaultAsync(x => x.Nombre!.ToLower().Equals(nombre.ToLower()));
			return item is null;
		}
	}
}
