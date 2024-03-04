using LibreriaBase.Entidades;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ControlDepartamentoGeneral(IRepositorioGenerico<DepartamentoGeneral> repositorioGenerico) : ControlGenerico<DepartamentoGeneral>(repositorioGenerico)
	{
	}
}
