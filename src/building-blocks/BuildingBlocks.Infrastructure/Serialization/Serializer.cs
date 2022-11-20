using System.Text.Json;

namespace BuildingBlocks.Infrastructure.Serialization
{
    public class Serializer : ISerializer
    {

        public T? Deserialize<T>(string json)
        {
            if(string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            T? result = JsonSerializer.Deserialize<T>(json, options);
            return result;
        }

        public string Serialize<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            return JsonSerializer.Serialize(obj);
        }
    }
}
