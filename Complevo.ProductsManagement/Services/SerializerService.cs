using Complevo.ProductsManagement.Common;
using System.Text.Json;

namespace Complevo.ProductsManagement.Services
{
    public class SerializerService : ISerializerService
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public SerializerService()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
        public T Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input, _serializerOptions);
        }
    }
}
