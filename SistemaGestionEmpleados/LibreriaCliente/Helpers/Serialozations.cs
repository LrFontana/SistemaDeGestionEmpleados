
using System.Text.Json;

namespace LibreriaCliente.Helpers
{
    public static class Serialozations
    {
        // Propiedades.
        public static string SerializeObj<T>(T modeloObj) => JsonSerializer.Serialize(modeloObj);
        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);
        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
