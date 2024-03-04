using Blazored.LocalStorage;

namespace LibreriaCliente.Helpers
{
    public class ServicioDeAlmacenamientoLocal(ILocalStorageService almacenamientoLocal)
    {
        // Propiedades.
        private const string LLaveDeAlmacenamiento = "token-de-autenticacion";
        public async Task<string> ObtenerToken() => await almacenamientoLocal.GetItemAsStringAsync(LLaveDeAlmacenamiento);
        public async Task EstablecerToken(string item) => await almacenamientoLocal.SetItemAsStringAsync(LLaveDeAlmacenamiento, item);
        public async Task EliminarToken() => await almacenamientoLocal.RemoveItemAsync(LLaveDeAlmacenamiento);
    }
}
