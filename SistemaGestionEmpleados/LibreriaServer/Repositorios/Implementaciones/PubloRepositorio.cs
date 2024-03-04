using LibreriaBase.Entidades;
using LibreriaBase.Respuestas;
using LibreriaServer.Datos;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace LibreriaServer.Repositorios.Implementaciones
{
	public class PubloRepositorio(AppDbContext appDbContext) : IRepositorioGenerico<Pueblo>
	{
		// Metodos.

		// Actualizar.
		public async Task<List<Pueblo>> ObtenerTodos() => await appDbContext.Pueblo.ToListAsync();

		// Obtener por id.
		public async Task<Pueblo> ObtenerPorId(int id) => await appDbContext.Pueblo.FindAsync(id);

		// Insertar.
		public async Task<RespuestaGeneral> Insertar(Pueblo item)
		{
			// verifica el nombre con el metodo  "VerificarNombre", si no lo encuenta devuelve false, y sino lo agrega, guarda los cambio y devuelve true(exitoso).

			if (!await VerificarNombre(item.Nombre!)) return new RespuestaGeneral(false, "El Pueblo Ya Existe!.");
			appDbContext.Pueblo.Add(item);
			await Commit();
			return Exitoso();
		}

		// Actualizar.
		public async Task<RespuestaGeneral> Actualizar(Pueblo item)
		{
			var query = await appDbContext.Pueblo.FindAsync(item);
			if (query is null) return NoEncontrado();
			query.Nombre = item.Nombre;
			await Commit();
			return Exitoso();
		}

		// Eliminar por id.
		public async Task<RespuestaGeneral> EliminarPorId(int id)
		{
			// busca y verifica el id en la base de datos, sino lo encontro devuelve "no encontrado"
			var query = await appDbContext.Pueblo.FindAsync(id);
			if (query is null) return NoEncontrado(); ;

			// si lo encontro, lo elimina, guarda los cambios y devuelve exitoso.
			appDbContext.Pueblo.Remove(query);
			await Commit();
			return Exitoso();
		}

		// No encontrado.
		private static RespuestaGeneral NoEncontrado() => new(false, "Pueblo No Encontrado.");

		// Exitoso.
		private static RespuestaGeneral Exitoso() => new(true, "Proceso Completado.");

		// Commit.
		private async Task Commit() => await appDbContext.SaveChangesAsync();

		// Verificar Nombre
		private async Task<bool> VerificarNombre(string nombre)
		{
			var item = await appDbContext.Pueblo.FirstOrDefaultAsync(x => x.Nombre!.ToLower().Equals(nombre.ToLower()));
			return item is null;
		}
	}
}
