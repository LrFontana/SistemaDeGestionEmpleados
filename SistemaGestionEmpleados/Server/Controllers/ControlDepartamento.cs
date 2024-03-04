using LibreriaBase.Entidades;
using LibreriaServer.Repositorios.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ControlDepartamento(IRepositorioGenerico<Departamento> repositorioGenerico) : ControlGenerico<Departamento>(repositorioGenerico)
	{
	}
}
