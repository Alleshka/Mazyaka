using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maze.Common.Types
{
    public class EntityIdJsonConverter<T> : JsonConverter<T> where T : struct
    {
        private static readonly Func<string, T> _from;
        private static readonly Func<T, string> _toString;

        static EntityIdJsonConverter()
        {
            // Find From(string) method
            var fromMethod = typeof(T).GetMethod("From", new[] { typeof(string) });
            if (fromMethod == null)
                throw new InvalidOperationException($"{typeof(T).Name} must have a static From(string) method");

            var param = Expression.Parameter(typeof(string));
            _from = Expression.Lambda<Func<string, T>>(
                Expression.Call(fromMethod, param), param).Compile();

            // Use ToString() for serialization
            var instance = Expression.Parameter(typeof(T));
            var toStringMethod = typeof(T).GetMethod("ToString", Type.EmptyTypes)!;
            _toString = Expression.Lambda<Func<T, string>>(
                Expression.Call(instance, toStringMethod), instance).Compile();
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return default;
            return _from(value);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(_toString(value));
        }
    }

    public class EntityIdJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(IEntityId).IsAssignableFrom(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeToConvert.GetNestedType("JsonConverter")
                ?? throw new InvalidOperationException(
                    $"{typeToConvert.Name} must have a nested JsonConverter class. Did you use the entityid snippet?");

            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }
}
