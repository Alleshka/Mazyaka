using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maze.Common.Types
{
    public class EntityIdJsonConverter<T> : JsonConverter<T> where T : struct
    {
        private static readonly Func<int, T> _from;

        static EntityIdJsonConverter()
        {
            var method = typeof(T).GetMethod("From", new[] { typeof(int) });
            var param = Expression.Parameter(typeof(int));
            _from = Expression.Lambda<Func<int, T>>(
                Expression.Call(method!, param), param).Compile();
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
                return _from(reader.GetInt32());

            return default;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var prop = typeof(T).GetProperty("Value")!;
            writer.WriteNumberValue((int)prop.GetValue(value)!);
        }
    }

    public class EntityIdJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IEntityId).IsAssignableFrom(typeToConvert);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(EntityIdJsonConverter<>).MakeGenericType(typeToConvert))!;
        }
    }
}
