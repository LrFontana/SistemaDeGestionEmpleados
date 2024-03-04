using LibreriaBase.DTOs;

namespace LibreriaCliente.Helpers
{
    public class ObtenerHttpCliente(IHttpClientFactory httpClientFactory, ServicioDeAlmacenamientoLocal servicioDeAlmacenamientoLocal)
    {
        // Propiedades.
        private const string Titulo = "Autorizacion";

        // Metodos.
        //Obtener
        public async Task<HttpClient> ObtenerHttpClientPrivado()
        {
            // Crea el cliente y obtiene el token.
            var cliente = httpClientFactory.CreateClient("SystemApiClient");
            var textoTotekn = await servicioDeAlmacenamientoLocal.ObtenerToken();
            if(string.IsNullOrEmpty(textoTotekn)) return cliente;


            var deserializeToken = Serialozations.DeserializeJsonString<SesionUsuario>(textoTotekn);
            if(deserializeToken == null ) return cliente;

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);
            return cliente;
        }

        public HttpClient ObtenerHttpClientePublico()
        {

            var cliente = httpClientFactory.CreateClient("SystemApiClient");
            cliente.DefaultRequestHeaders.Remove(Titulo);
            return cliente;
        }
    }
}
