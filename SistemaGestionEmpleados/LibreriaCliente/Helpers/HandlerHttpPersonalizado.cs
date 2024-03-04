using LibreriaBase.DTOs;
using LibreriaCliente.Servicios.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaCliente.Helpers
{
	public class HandlerHttpPersonalizado
		(ObtenerHttpCliente obtenerHttpCliente, ServicioDeAlmacenamientoLocal servicioDeAlmacenamientoLocal, IServicioCuentaUsuario servicioCuentaUsuario) : DelegatingHandler
	{
		// Metodos.

		// Ya viene por defecto de la clase que hereda.
		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			
			bool iniciarSesionUrl = request.RequestUri!.AbsoluteUri.Contains("login");
			bool registrarseUrl= request.RequestUri!.AbsoluteUri.Contains("register");
			bool tokenActualizadoUrl= request.RequestUri!.AbsoluteUri.Contains("token-de-actualizacion");

			if(iniciarSesionUrl || registrarseUrl || tokenActualizadoUrl) return await base.SendAsync(request, cancellationToken);
			
			var resultado = await base.SendAsync(request, cancellationToken);
			if(resultado.StatusCode == HttpStatusCode.Unauthorized)
			{
				// obtiene el token de ServicioDeAlmacenamientoLocal
				var stringToken = await servicioDeAlmacenamientoLocal.ObtenerToken();
				if (stringToken == null) return resultado;

				// verifica si el header contiene el token.
				string token = string.Empty;
				try { token = request.Headers.Authorization!.Parameter!; }
				catch { }

				// deserializa el token.
				var deserializarToken = Serialozations.DeserializeJsonString<SesionUsuario>(stringToken);
				if(deserializarToken is null)  return resultado; 

				// verifica que el token no este vacio.
				if (string.IsNullOrEmpty(token))
				{
					request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializarToken.Token);
					return await base.SendAsync(request, cancellationToken);
				}

				// obtiene el token actualizado.
				var nuevoJwtToken = await ObtenerTokenActualizado(deserializarToken.Token!);
				if (string.IsNullOrEmpty(nuevoJwtToken)) return resultado;

				// reemplaza el viejo token con el nuevo.
				request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", nuevoJwtToken);
				return await base.SendAsync(request, cancellationToken);

			}
			return resultado;
		}


		// Obtener token actualizado.
		private async Task<string> ObtenerTokenActualizado(string tokenActualizado)
		{
			var resultado =  await servicioCuentaUsuario.TokenDeActualizacionAsync(new TokenActualizado() { Token = tokenActualizado });
			string tokenSerializado = Serialozations.SerializeObj(new SesionUsuario() { Token = resultado.Token, TokenDeActualizacion = resultado.TokenActualizado });
			await servicioDeAlmacenamientoLocal.EstablecerToken(tokenSerializado);
			return resultado.Token;
		}
	}
}
