using Maze.Common.Types;
using System.Text.Json;

namespace Maze.Common
{
    public static class JsonOptions
    {
        public static readonly JsonSerializerOptions Default = Create();

        public static JsonSerializerOptions Create()
        {
            var options = new JsonSerializerOptions();
            Configure(options);
            return options;
        }

        public static void Configure(JsonSerializerOptions options)
        {
            options.PropertyNamingPolicy = null;
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            options.IncludeFields = true;

            options.Converters.Add(new EntityIdJsonConverterFactory());
        }
    }
}
